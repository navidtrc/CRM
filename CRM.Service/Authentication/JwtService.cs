using CRM.Common;
using CRM.Entities.DataModels.Security;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Service.Authentication
{
    public class JwtService : IJwtService
    {
        private readonly SiteSetting _siteSetting;
        private readonly SignInManager<Entities.DataModels.Security.User> signInManager;

        public JwtService(IOptionsSnapshot<SiteSetting> setting, SignInManager<Entities.DataModels.Security.User> signInManager)
        {
            _siteSetting = setting.Value;
            this.signInManager = signInManager;
        }
        public async Task<string> Generate(Entities.DataModels.Security.User user)
        {
            var secretKey = Encoding.UTF8.GetBytes(_siteSetting.JwtSetting.SecretKey);
            var signingCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKey), SecurityAlgorithms.HmacSha256Signature);

            var encryptionkey = Encoding.UTF8.GetBytes(_siteSetting.JwtSetting.Encryptkey); //must be 16 character
            var encryptingCredentials = new EncryptingCredentials(new SymmetricSecurityKey(encryptionkey), SecurityAlgorithms.Aes128KW, SecurityAlgorithms.Aes128CbcHmacSha256);

            var descriptor = new SecurityTokenDescriptor()
            {
                Issuer = _siteSetting.JwtSetting.Issuer,
                Audience = _siteSetting.JwtSetting.Audience,
                IssuedAt = DateTime.Now,
                NotBefore = DateTime.Now.AddMinutes(_siteSetting.JwtSetting.NotBeforeTimeMinutes),
                Expires = DateTime.Now.AddMinutes(_siteSetting.JwtSetting.ExpirationMinutes),
                SigningCredentials = signingCredentials,
                EncryptingCredentials = encryptingCredentials,
                Subject = new ClaimsIdentity(await GetClaims(user))
            };
            // If we dont want to .net claims automaticly convert to jwt claims
            //JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
            //JwtSecurityTokenHandler.DefaultMapInboundClaims = false;
            //JwtSecurityTokenHandler.DefaultOutboundClaimTypeMap.Clear();
            var tokenHandler = new JwtSecurityTokenHandler();
            var securityToken = tokenHandler.CreateToken(descriptor);
            var jwt = tokenHandler.WriteToken(securityToken);            
            return jwt;
        }
        private async Task<IEnumerable<Claim>> GetClaims(Entities.DataModels.Security.User user)
        {
            var result = await signInManager.ClaimsFactory.CreateAsync(user);
            return result.Claims;

            //var securityStampClaimType = new ClaimsIdentityOptions().SecurityStampClaimType;

            //var list = new List<Claim>
            //{
            //    new Claim(ClaimTypes.Name, user.UserName),
            //    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            //    new Claim(securityStampClaimType, user.SecurityStamp.ToString())
            //};

            //var roles = new Role[] { new Role { Name = "Admin" } };
            //foreach (var role in roles)
            //    list.Add(new Claim(ClaimTypes.Role, role.Name));

            //return list;
        }
    }
}
