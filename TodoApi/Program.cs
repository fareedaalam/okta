using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Okta.AspNetCore;
using TodoApi.Models;
using TodoApi.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "Okta Auth header using the Bearer scheme Example: \"Authorization: Bearer <token>\"",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
        new OpenApiSecurityScheme
        {
           Name="Bearer",
           Type=SecuritySchemeType.ApiKey,
           In=ParameterLocation.Header,
           Reference=new OpenApiReference
           {
               Type=ReferenceType.SecurityScheme,
               Id="Bearer"
           }
        },
    new string[] { }
        }
    });

});


    builder.Services.Configure<OktaTokenSettings>(builder.Configuration.GetSection("Okta"));
    var oktaSettings = builder.Configuration.GetSection("Okta").Get<OktaTokenSettings>();

    builder.Services.AddAuthentication(option =>
    {
        option.DefaultAuthenticateScheme = OktaDefaults.ApiAuthenticationScheme;
        option.DefaultChallengeScheme = OktaDefaults.ApiAuthenticationScheme;
        option.DefaultSignInScheme = OktaDefaults.ApiAuthenticationScheme;
    }).AddOktaWebApi(new OktaWebApiOptions
    {
        OktaDomain = oktaSettings.Domain,
        AuthorizationServerId = oktaSettings.AuthorizationServerId,
        Audience = oktaSettings.Audience
    });


    builder.Services.AddSingleton<ITokenService, TokenService>();

    //builder.Services.AddAuthentication(authOptions =>
    //{
    //    authOptions.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme; ;
    //    authOptions.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    //    authOptions.DefaultSignOutScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    //    //authOptions.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    //    authOptions.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
    //}).AddOpenIdConnect(oicdOption =>
    //{
    //    oicdOption.ClientId = builder.Configuration["Okta:ClientId"];
    //    oicdOption.ClientSecret = builder.Configuration["Okta:ClientSecret"];
    //    //oicdOption.CallbackPath = "authorization-code/callback";
    //    oicdOption.Authority = builder.Configuration["Okta:Issuer"];
    //    oicdOption.ResponseType = "code";
    //    oicdOption.SaveTokens = true;
    //   // oicdOption.Scope.Add("openid");
    //    //oicdOption.Scope.Add("profile");
    //    oicdOption.TokenValidationParameters = new TokenValidationParameters
    //    {
    //        ValidateIssuer = true,
    //        ValidIssuer= builder.Configuration["Okta:Issuer"],
    //        ValidateAudience=false,
    //        ValidateLifetime=true

    //};
    //}).AddCookie();

    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();
    app.UseAuthentication();
    app.UseAuthorization();

    app.MapControllers();

    app.Run();


