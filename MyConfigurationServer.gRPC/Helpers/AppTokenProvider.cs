using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MyClassroom.Contracts;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace MyConfigurationServer.gRPC.Helpers
{
    public class AppTokenProvider : ITokenProvider
    {
        private readonly APISettings _config;

        public AppTokenProvider(IOptionsSnapshot<APISettings> optionsSnapshot)
        {
            _config = optionsSnapshot.Get(APISettings.gRPCConfiguration);
        }

        public string GetToken()
        {

            var tokenOptions = new JwtSecurityToken(
                issuer: _config.ValidIssuer,
                audience: _config.ValidAudience,
                //claims: claims,
                expires: DateTime.Now.AddDays(30),
                signingCredentials: GetSignCredentials()
                );

           return new JwtSecurityTokenHandler().WriteToken(tokenOptions);
        }

        private SigningCredentials GetSignCredentials()
        {
            var secret = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.SecretKey));
            return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
        }
    }
}
