using Common.Domain;
using Common.Exceptions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Common.Infrastructure
{
    public class RabbitMQEventBus : IEventBus
    {
        private const int RabbitDefaultPort = 5672;
        private readonly IList<IDomainEvent> _pendingEvents = new List<IDomainEvent>();
        private readonly IDictionary<string, IList<Type>> _subscribers = new Dictionary<string, IList<Type>>();
        private readonly IDictionary<string, Type> _eventMap = new Dictionary<string, Type>();
        private readonly string _rabbitHostname;
        private readonly int _rabbitPort;
        private readonly ILogger<RabbitMQEventBus> _logger;
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public RabbitMQEventBus(ILogger<RabbitMQEventBus> logger, IConfiguration configuration, IServiceScopeFactory serviceScopeFactory)
        {
            _logger = logger;
            _serviceScopeFactory = serviceScopeFactory;
            _rabbitHostname = configuration["App:RabbitHostName"];
            _rabbitPort = int.TryParse(configuration["App:RabbitPort"], out int portNumber) ? portNumber : RabbitDefaultPort;
        }

        #region IEventBus
        public void DiscardEvents()
            => _pendingEvents.Clear();

        public void Record(DomainEvent domainEvent) 
            => _pendingEvents.Add(domainEvent ?? throw new InvalidEventDomainException("Invalid event"));

        public Task PublishAsync()
        {
            return Task.Run(() =>
            {
                if (_pendingEvents.Count == 0)
                    return;

                var factory = new ConnectionFactory() { HostName = _rabbitHostname, Port = _rabbitPort };

                using (var connection = factory.CreateConnection())
                {
                    using var channel = connection.CreateModel();

                    foreach (var evt in _pendingEvents)
                    {
                        var eventName = GetEventName(evt.GetType());
                        channel.QueueDeclare(eventName, false, false, false, null);
                        //var message = System.Text.Json.JsonSerializer.Serialize(evt); => missing child properties
                        var message = Newtonsoft.Json.JsonConvert.SerializeObject(evt);
                        var body = Encoding.UTF8.GetBytes(message);
                        _logger.LogInformation("Publishing event: " + message);
                        channel.BasicPublish(string.Empty, eventName, null, body);
                    }
                }

                _pendingEvents.Clear();
            });
        }

        public void Subscribe<E, H>()
            where E: IDomainEvent
            where H: IDomainEventSubscriber<E>
        {
            var eventName = GetEventName(typeof(E));
            var subs = _subscribers.ContainsKey(eventName) ? _subscribers[eventName] : new List<Type>();

            if (subs.Contains(typeof(H)))
                return;

            subs.Add(typeof(H));
            _subscribers[eventName] = subs;

            if (!_eventMap.ContainsKey(eventName))
                _eventMap.Add(eventName, typeof(E));

            var factory = new ConnectionFactory() { HostName = _rabbitHostname, Port = _rabbitPort, DispatchConsumersAsync = true };

            using (var connection = factory.CreateConnection())
            {
                using var channel = connection.CreateModel();
                
                channel.QueueDeclare(eventName, false, false, false, null);
                var consumer = new AsyncEventingBasicConsumer(channel);
                consumer.Received += ConsumerProcessMessage;

                channel.BasicConsume(eventName, true, consumer);
            }

        }

        #endregion

        private string GetEventName(Type evt)
            => GetEventName(evt.GetType().Name);

        private string GetEventName(string eventName)
            => $"Demo-{eventName}-v1";

        private async Task ConsumerProcessMessage(object sender, BasicDeliverEventArgs e)
        {
            var eventName = GetEventName(e.RoutingKey);

            if (!_subscribers.ContainsKey(eventName))
                return;
            
            var subscriptions = _subscribers[eventName];
            var body = Encoding.UTF8.GetString(e.Body.ToArray());
            //var message = System.Text.Json.JsonSerializer.Deserialize(body, _eventMap[eventName]);
            var message = Newtonsoft.Json.JsonConvert.DeserializeObject(body, _eventMap[eventName]);

            try
            {
                using (var scope = _serviceScopeFactory.CreateScope())
                {
                    foreach (var sb in subscriptions)
                    {
                        var subscriber = scope.ServiceProvider.GetService(sb);
                        var method = sb.GetMethod("Dispatch");
                        await (Task)method.Invoke(subscriber, new object[] { message });
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error processig message");
            }
        }
    }
}
