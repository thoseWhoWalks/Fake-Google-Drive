using FGD.Api.Model;
using FGD.Bussines.Model; 
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FGD.Bussines.Service
{
    public interface IAuthJWTService
    {
        SymmetricSecurityKey GetSymmetricSecurityKey();

        Task<ResponseModel<AuthTokenResponseModel>> GetTokenResponseAsync(LoginModelApi loginApiModel);

    }

}
