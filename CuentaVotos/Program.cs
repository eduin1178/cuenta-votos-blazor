using CuentaVotos.Api;
using CuentaVotos.Application;
using CuentaVotos.Core.Account;
using CuentaVotos.Core.Users;
using CuentaVotos.Data.LiteDb;
using CuentaVotos.Repository;
using CuentaVotos.Services;
using CurrieTechnologies.Razor.SweetAlert2;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.IdentityModel.Tokens;
using Sotsera.Blazor.Toaster.Core.Models;
using System.Security.Claims;
using System.Text;

namespace CuentaVotos
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddRazorPages();
            builder.Services.AddServerSideBlazor();
            builder.Services.AddHttpContextAccessor();
            builder.Services.AddControllers();


            var allowedOrigins = builder.Configuration.GetSection("Origins").Get<string[]>();

            builder.Services.AddCors(options => options.AddPolicy("CorsPolicy", builder =>
            {
                builder.WithOrigins(allowedOrigins)
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowAnyOrigin();
            }));



            var key = Encoding.ASCII.GetBytes(builder.Configuration.GetSection("Jwt:Key").Value);
            builder.Services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
            {

                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = builder.Configuration.GetValue<string>("Jwt:Issuer"),
                    ValidAudience = builder.Configuration.GetValue<string>("Jwt:Audience"),
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = false,
                    NameClaimType = ClaimTypes.NameIdentifier
                };
            });


            builder.Services.AddScoped<HttpContextAccessor>();
            builder.Services.AddScoped<AuthenticationStateProvider, AppAuthenticationProvider>();

            builder.Services.AddSingleton(x => new LiteDbContext(@"C:\Data\MyData.litedb"));

            builder.Services.AddTransient<IAccountRepository, AccountRespository>();
            builder.Services.AddTransient<UserRegisterUseCase>();
            builder.Services.AddTransient<UserLoginUseCase>();

            builder.Services.AddTransient<IUserRespository, UserRepository>();
            builder.Services.AddTransient<UsersManager>();


            builder.Services.AddScoped<ApiClient>();

            builder.Services.AddSweetAlert2();
            builder.Services.AddToaster(config =>
            {
                config.PositionClass = Defaults.Classes.Position.BottomCenter;
                config.PreventDuplicates = true;
                config.NewestOnTop = true;
                config.MaxDisplayedToasts = 5;
                config.ProgressBarClass = Defaults.Classes.ProgressBarClass;
                config.CloseIconClass = Defaults.Classes.CloseIconClass;
                config.ShowCloseIcon = true;

                config.VisibleStateDuration = 10000;
                config.ShowProgressBar = true;

                config.ShowTransitionDuration = 100;
                config.HideTransitionDuration = 100;
            });


            var app = builder.Build();

            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();
            app.MapBlazorHub();
            app.MapFallbackToPage("/_Host");


            app.Run();
        }
    }
}