using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using ConsultaJa.Models;
using ConsultaJa.Backend;

namespace ConsultaJa.Services
{
    public class ContaService
    {
        public ContaModel Authenticate(ContaModel conta, string email, string type)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(Settings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, email),
                    new Claim("Store", type),
                    new Claim("Id", conta.Id)
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            conta.Token = tokenHandler.WriteToken(token);

            conta.Password = null;

            return conta;
        }
    }
}

