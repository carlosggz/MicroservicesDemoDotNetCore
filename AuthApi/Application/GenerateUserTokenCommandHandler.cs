using AuthApi.Domain;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace AuthApi.Application
{
    public class GenerateUserTokenCommandHandler : IRequestHandler<GenerateUserTokenCommand, string>
    {
        private readonly IUsersRepository _repository;
        private readonly ICryptPassword _cryptPassword;
        private readonly ITokenBuilder _tokenBuilder;

        public GenerateUserTokenCommandHandler(IUsersRepository repository, ICryptPassword cryptPassword, ITokenBuilder tokenBuilder)
        {
            _repository = repository;
            _cryptPassword = cryptPassword;
            _tokenBuilder = tokenBuilder;
        }
        public Task<string> Handle(GenerateUserTokenCommand request, CancellationToken cancellationToken)
        {
            return Task.Run(() =>
            {
                if (string.IsNullOrEmpty(request.UserName) || string.IsNullOrEmpty(request.Password))
                    return null;

                var user = _repository.GetUser(request.UserName);

                return user == null || _cryptPassword.GetCrypted(request.Password) != user.Password
                    ? null
                    : _tokenBuilder.BuildToken(user);
            });
        }
    }
}
