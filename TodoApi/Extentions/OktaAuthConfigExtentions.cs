using System;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.IdentityModel.Tokens;
using Okta.AspNetCore;
using TodoApi.Models;

namespace TodoApi.Extentions
{
    public static class OktaAuthConfigExtentions
    {
        public static IServiceCollection AddOktaServices(this IServiceCollection services, IConfiguration config)
        {

             services.Configure<OktaTokenSettings>(config.GetSection("Okta"));           
            var oktaSettings = config.GetSection("Okta").Get<OktaTokenSettings>();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
              .AddJwtBearer(options =>
              {
                  options.Audience = oktaSettings.Audience;
                  options.Authority = oktaSettings.Issuer;
                  
              });

            services.AddAuthorization(opt =>
            {
                opt.AddPolicy("RequiredAdminRole", policy =>
                    policy.RequireClaim("groups","MeetAdmin"));
                opt.AddPolicy("RequiredUserRole", policy => 
                    policy.RequireClaim("groups", "MeetUser"));

            });
            /***
            services.AddAuthentication(option =>
            {
                     options.Audience = oktaSettings.Audience;
                  options.Authority = oktaSettings.Issuer;
               // option.DefaultAuthenticateScheme = OktaDefaults.ApiAuthenticationScheme;
               // option.DefaultChallengeScheme = OktaDefaults.ApiAuthenticationScheme;
               // option.DefaultSignInScheme = OktaDefaults.ApiAuthenticationScheme;
            }).AddOktaWebApi(new OktaWebApiOptions
            {
                OktaDomain = oktaSettings.Domain,
                AuthorizationServerId = oktaSettings.AuthorizationServerId,
                Audience = oktaSettings.Audience
            });
            ***/

            return services;
        }
    }
}

