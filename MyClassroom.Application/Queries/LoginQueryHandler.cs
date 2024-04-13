using MyClassroom.Application.Common;
using MyClassroom.Contracts;
using MyClassroom.Domain.AggregatesModel.UserAggregate;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System.IdentityModel.Tokens.Jwt;
using MyClassroom.Infrastructure.Services;

namespace MyClassroom.Application.Queries
{
    public class LoginQueryHandler : IRequestHandler<LoginQuery, BaseResponse<LoginResponse>>
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly APISettings _apiSettings;
        private readonly IUserRepository _userRepository;

        public LoginQueryHandler(
            IOptions<APISettings> options,
            IAuthenticationService authenticationService,
            IUserRepository userRepository)
        {
            _authenticationService = authenticationService ?? throw new ArgumentNullException(nameof(authenticationService));
            _apiSettings = options.Value ?? throw new ArgumentNullException(nameof(options));
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        }

        public async Task<BaseResponse<LoginResponse>> Handle(LoginQuery request, CancellationToken cancellationToken)
        {
            var result = await _userRepository.PasswordSignInAsync(request.UserName, request.Password);

            if (result)
            {
                var user = await _userRepository.GetByUserNameAsync(request.UserName);

                if (user == null)
                {
                    return new BaseResponse<LoginResponse>(APIProblemFactory.InvalidAuthentication());
                }

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
