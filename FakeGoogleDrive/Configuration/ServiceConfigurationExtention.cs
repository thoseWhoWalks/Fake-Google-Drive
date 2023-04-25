using FGD.Api.Model;
using FGD.Bussines.Model;
using FGD.Bussines.Service;
using FGD.Bussines.Service.Helper;
using FGD.Data;
using FGD.Data.Service;
using FGD.Encryption.Helpers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Threading.Tasks;

namespace FGD.Api.Configuration
{
    public static class ServiceConfigurationExtention
    {
        private static IConfiguration _Configuration;

        public static IConfiguration Configuration { get => null; set => _Configuration = value; }

        public static void ConfigureAuthentication(this IServiceCollection services)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
               .AddJwtBearer(options =>
               {
                   options.RequireHttpsMetadata = false;
                   options.TokenValidationParameters = new TokenValidationParameters
                   {
                       ValidateIssuer = true,
                       ValidIssuer = _Configuration["AuthJWT:ISSUER"],

                       ValidateAudience = true,
                       ValidAudience = _Configuration["AuthJWT:AUDIENCE"],

                       ValidateLifetime = true,

                       ClockSkew = TimeSpan.Zero,


                       IssuerSigningKey = SymetricTokenEncryptionHelper.GetSymmetricSecurityKey(_Configuration["AuthJWT:KEY"]),
                       ValidateIssuerSigningKey = true,
                   };

               });
        }

        public static void RegisterCutomServices(this IServiceCollection services)
        {
            #region Data Access Logic
            RegisterDataAccesServices(services);
            #endregion

            #region Business logic
            RegisterBussinesServices(services);
            #endregion



            #region Helpers
            RegisterHelpers(services);
            #endregion

        }

        public static void ConfigureCors(this IServiceCollection services)
        {
            services.AddCors();
        }

        public static void ConfigureModelValidation(this IServiceCollection services)
        {
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = (context) =>
                {
                    var errors = context.ModelState.Values.SelectMany(x => x.Errors.Select(p => p.ErrorMessage)).ToList();
                    var result = new
                    {
                        Code = "00009",
                        Message = "Validation errors",
                        Errors = errors
                    };
                    return new BadRequestObjectResult(result);
                };
            });
        }

        public static void RegisterDatabaseContext(this IServiceCollection services)
        {
            services.AddDbContext<FakeGoogleDriveContext>(dbContextOptions =>
            {
                dbContextOptions.UseSqlServer(
                _Configuration.GetConnectionString("MSSqlConnectionDocker")
                );
            },ServiceLifetime.Transient);
        }

        public static void ConfigureResponseOptimisation(this IServiceCollection services)
        {
            services.AddResponseCompression(options =>
            {
                options.EnableForHttps = true;
            });
            services.Configure<GzipCompressionProviderOptions>(options =>
            {
                options.Level = CompressionLevel.Optimal;
            });
        }

        public static void SetUpOptions(this IServiceCollection services)
        {
            services.AddOptions();

            services.Configure<AuthJWTModel>(_Configuration.GetSection("AuthJWT"));
        }

        private static void RegisterHelpers(IServiceCollection services)
        {
            services.AddTransient<IRedisCachingService, RedisCachingService>();
            services.AddTransient<HostingEnvironmentHelper>();
            services.AddTransient<ImageSharpHelper>();
            services.AddTransient<ThumbnailHelper>();
        }

        private static void RegisterBussinesServices(IServiceCollection services)
        {
            services.AddTransient<IAuthJWTService, AuthJWTService>();

            services.AddTransient<IAccountService<AccountModelApi<int>, int>, AccountService>();

            services.AddScoped<IAccountSubscriptionService<AccountSubscriptionModelApi<int>, int>,
                AccountSubscriptionService>();

            services.AddTransient<ISubscriptionService<SubscriptionModelApi<int>, int>,
                SubscriptionService>();

            services.AddTransient<IRootFolderService<RootFolderModelBussines<int>, int>,
                RootFolderService>();

            services.AddTransient<INotificationService<NotificationModelApi<int>, int>,
                NotificationService>();

            services.AddTransient<IStoredFolderService<StoredFolderModelApi<int>, int>,
                StoredFolderService>();

            services.AddTransient<IStoredFileService<StoredFileModelApi<int>, int>, StoredFileService>();

            services.AddTransient<ISharedFileService<int, SharedFileModelApi<int>, StoredFileModelApi<int>>,
                SharedFileService>();

            services.AddTransient<ISharedFolderService<int, SharedFolderModelApi<int>, StoredFolderModelApi<int>>,
                SharedFolderService>();

            services.AddTransient<IBinService<int, StoredFileModelApi<int>, StoredFolderModelApi<int>>, BinService>();

            services.AddTransient<ISignalRCommunicationService, SignalRCommunicationService>();

            services.AddTransient<ISubscriptionCapacityService, SubscriptionCapacityService>();
        }

        private static void RegisterDataAccesServices(IServiceCollection services)
        {

            #region Account

            services.AddTransient<IAccountRepository<AccountModelBussines<int>, int>,
                                    AccountRepository>();

            services.AddTransient<IAccountCryptoRepository<AccountCryptoModelBussines<int>, int>, AccountCryptoRepository>();

            #endregion

            services.AddTransient<ISubscriptionRepository<SubscriptionModelApi<int>, int>,
                    SubscriptionRepository>();

            services.AddTransient<IAccountSubscriptionRepository<AccountSubscriptionModelBussines<int>, int>,
                    AccountSubscriptionRepository>();

            services.AddTransient<IRootFolderRepository<RootFolderModelBussines<int>, int>,
                    RootFolderRepository>();

            services.AddScoped<IStoredFolderRepository<StoredFolderModelBussines<int>, int>,
                   StoredFolderRepository>();

            services.AddTransient<IStoredFileRepository<StoredFileModelBussines<int>, int>,
                   StoredFileRepository>();


            #region Notififcation 
            services.AddTransient<INotificationRepository<NotificationModelBussines<int>, int>,
                    NotificationRepository>();
            #endregion

            #region Shared Items
            services.AddTransient<ISharedFileRepository<SharedFileModelBussines<int>, int>,
                SharedFileRepository>();

            services.AddTransient<ISharedFolderRepository<SharedFolderModelBussines<int>, int>,
                SharedFolderRepository>();
            #endregion Shared Items
        }
    }
}
