using APPLICATION.Features.Auth.Models;
using APPLICATION.Features.Auth.Models.Extensions;
using APPLICATION.Features.Auth.Repository;
using APPLICATION.Features.Auth.Service;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace APPLICATION.Features.Auth.UseCase
{
    public class LoginHandler : IRequestHandler<LoginRequest, AuthResponse>
    {
        private readonly IAuthRepository _authRepository;
        private readonly ITokenService _tokenService;

        public LoginHandler(IAuthRepository authRepository, ITokenService tokenService)
        {
            _authRepository = authRepository;
            _tokenService = tokenService;
        }

        public async Task<AuthResponse> Handle(LoginRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var account = await _authRepository.SelectAccountAsync(request.Mapper(), cancellationToken);

                var token = await _tokenService.GenerateToken(account);

                return new AuthResponse
                {
                    User = account.Username,
                    Token = token
                };
            }
            catch (System.Exception)
            {
                return null;
            }
        }
    }
}