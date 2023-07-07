using System;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using Stark.Model;


namespace Stark.Helper
{
    public static class StarkHelper
    {
        public static string GenerateJwtToken(AuthenticationModel.SignUPModel date, string secretKey)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(secretKey);
            Array.Resize(ref key, 16);
         
            var _claims = new Claim[]
            {
                new Claim("userCode", date.userCode),
                new Claim("lastName", date.lastName),
                new Claim("firstName", date.firstName),
                new Claim("middleName", date.middleName),
                new Claim("email", date.email),
                new Claim("gcashNo", date.gcashNo),
                new Claim("mobileNo", date.mobileNo),
                new Claim("dateOfBirthEpoch",
                new DateTimeOffset(DateTime.ParseExact(date.dateOfBirthEpoch, "MM/dd/yyyy", null, System.Globalization.DateTimeStyles.None)).ToUnixTimeSeconds().ToString()),
                new Claim("password", date.password)
            };
            
            var _identity = new ClaimsIdentity(_claims);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = _identity,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
            };
            var token = tokenHandler.CreateJwtSecurityToken(tokenDescriptor);
            token.Payload.Remove(JwtRegisteredClaimNames.Nbf);
            token.Payload.Remove(JwtRegisteredClaimNames.Exp);
            token.Payload.Remove(JwtRegisteredClaimNames.Iat);
            return tokenHandler.WriteToken(token);
        }
    }
}
