# Microservices Demo using c#

Example of a microservices application using best practices:
* Api gateway re-routing, aggregates, handler delegation and secured entrypoints  (using Ocelot)
* Service Discovery (using Consul)
* Quality of Service (using Polly)
* Asynchronous communication (using Rabbitmq)

## Instructions

Clone the repository and run composer: 

docker-compose up --build

The api gateway will be available at http://localhost:8000.

Import the postman collection to test the api.
