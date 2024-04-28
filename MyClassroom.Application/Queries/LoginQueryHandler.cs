using MyClassroom.Application.Common;
using MyClassroom.Contracts;
using MyClassroom.Domain.AggregatesModel.UserAggregate;
using MediatR;
using Microsoft.Extensions.Options;
using System.IdentityModel.Tokens.Jwt;
using MyClassroom.Infrastructure.Services;
using MyConfigurationServer.gRPC.Clients;
using Microsoft.Extensions.Logging;

namespace MyClassroom.Application.Queries
{
    public class LoginQueryHandler(
        IOptions<APISettings> options,
        IConfigurationClient client,
        IAuthenticationService authenticationService,
        IUserRepository userRepository) : IRequestHandler<LoginQuery, BaseResponse<LoginResponse>>
    {
        private readonly IAuthenticationService _authenticationService = authenticationService ?? throw new ArgumentNullException(nameof(authenticationService));
        private readonly APISettings _apiSettings = options.Value ?? throw new ArgumentNullException(nameof(options));
        private readonly IUserRepository _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        private readonly IConfigurationClient _client = client ?? throw new ArgumentNullException(nameof(client));


        public async Task<BaseResponse<LoginResponse>> Handle(LoginQuery request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.PasswordSignInAsync(request.UserName, request.Password);

            if (user != null)
            {
                var signInCredentials = _authenticationService.GetSignCredentials();
                var claims = await _authenticationService.GetClaimsAsync(user);

                var tokenOptions = new JwtSecurityToken(
                    issuer: _apiSettings.ValidIssuer,
                    audience: _apiSettings.ValidAudience,
                    claims: claims,
                    expires: DateTime.Now.AddDays(30),
                    signingCredentials: signInCredentials);

                var token = new JwtSecurityTokenHandler().WriteToken(tokenOptions);

                var userResponse = new LoginResponse
                {
                    Email = user.Email ?? string.Empty,
                    Id = user.Id,
                    UserName = user.UserName ?? string.Empty,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    IsAuthSuccess = true,
                    Token = token
                };

                return new BaseResponse<LoginResponse>(userResponse);
            }
            else
            {
                return new BaseResponse<LoginResponse>(APIProblemFactory.InvalidAuthentication());
            }
        }
    }
}
