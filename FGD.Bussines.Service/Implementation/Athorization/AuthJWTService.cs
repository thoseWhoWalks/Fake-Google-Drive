using FGD.Api.Model;
using FGD.Bussines.Model;
using FGD.Data.Service;
using FGD.Encryption.Helpers;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace FGD.Bussines.Service
{
    public class AuthJWTService : IAuthJWTService
    {
        IOptions<AuthJWTModel> authJWToptions;

        IAccountRepository<AccountModelBussines<int>, int> accountService;

        public AuthJWTService(IOptions<AuthJWTModel> authJWToptions,
                                IAccountRepository<AccountModelBussines<int>, int> accountService)
        {
            this.authJWToptions = authJWToptions;
            this.accountService = accountService;
        }


        public async Task<ResponseModel<AuthTokenResponseModel>> GetTokenResponseAsync(LoginModelApi loginApiModel)
        {

            var accountApiModel = await accountService.GetByEmailAsync(loginApiModel.Email);

            var respModel = new ResponseModel<AuthTokenResponseModel>();

            if (accountApiModel == null|| !await accountService.VerifyUserAsync(loginApiModel))
            {
                respModel.AddError(new Error($"Email or password is incorrect..."));
                return respModel;
            }
 
            var claims = GenerateClaims(accountApiModel);

            var jwt = new JwtSecurityToken(
                    issuer: this.authJWToptions.Value.Issuer,
                    audience: this.authJWToptions.Value.Audience,
                    notBefore: DateTime.UtcNow,
                    claims: claims.Claims,
                    expires: DateTime.UtcNow.Add(TimeSpan.FromDays(this.authJWToptions.Value.Lifetime)),
                    signingCredentials: new SigningCredentials(this.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));

            var tokenRespModel = new AuthTokenResponseModel()
            {
                Token = new JwtSecurityTokenHandler().WriteToken(jwt),
                UserId = Convert.ToInt32(claims.Name)
            };

            respModel.Item = tokenRespModel;

            return respModel;

        }

        private ClaimsIdentity GenerateClaims(AccountModelBussines<int> accountModelBussines)
        {

            var claims = new List<Claim>
                {
                    new Claim(ClaimsIdentity.DefaultNameClaimType, accountModelBussines.Id.ToString()),
                    new Claim(ClaimsIdentity.DefaultRoleClaimType, accountModelBussines.Role)
                };

            return new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
                ClaimsIdentity.DefaultRoleClaimType);

        }

        public SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return SymetricTokenEncryptionHelper.GetSymmetricSecurityKey(this.authJWToptions.Value.Key);
        }
    
    }
}
