using BlazorDownloadFile;
using Blazored.LocalStorage;
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
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Microsoft.IdentityModel.Tokens;
using Sotsera.Blazor.Toaster.Core.Models;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace CuentaVotos
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddRazorPages();
            builder.Services.AddControllers();
            builder.Services.AddServerSideBlazor();
            builder.Services.AddHttpContextAccessor();
            builder.Services.AddScoped<ProtectedLocalStorage>();
            builder.Services.AddSession(x=>
            {
                x.IdleTimeout = new TimeSpan(1,0,0);
                x.Cookie.Name = "cuentavotos";
                x.IOTimeout = new TimeSpan(24, 0, 0);
                x.Cookie.Path = "/";
            });
            builder.Services.AddScoped<TokenProvider>();
            builder.Services.AddScoped<HttpContextAccessor>();
            builder.Services.AddBlazoredLocalStorage(config =>
            {
                config.JsonSerializerOptions.DictionaryKeyPolicy = JsonNamingPolicy.CamelCase;
                config.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
                config.JsonSerializerOptions.IgnoreReadOnlyProperties = true;
                config.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
                config.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
                config.JsonSerializerOptions.ReadCommentHandling = JsonCommentHandling.Skip;
                config.JsonSerializerOptions.WriteIndented = false;
            });

            builder.Services.AddScoped<AuthenticationStateProvider, AppAuthenticationProvider>();

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


            
            var pathDb = $"{builder.Configuration.GetValue<string>("ConnectionStrings:AppDbContext")}";
            try
            {
                Directory.CreateDirectory(Path.GetDirectoryName(pathDb));
            }
            catch (Exception)
            {
            }
            builder.Services.AddSingleton(x => new LiteDbContext(pathDb));

            builder.Services.AddTransient<IAccountRepository, AccountRespository>();
            builder.Services.AddTransient<UserRegisterUseCase>();
            builder.Services.AddTransient<UserLoginUseCase>();

            builder.Services.AddTransient<IUserRespository, UserRepository>();
            builder.Services.AddTransient<UsersManager>();


            builder.Services.AddScoped<ApiClient>();
            builder.Services.AddBlazorDownloadFile(ServiceLifetime.Scoped);

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
            app.UseSession();

            app.MapControllers();
            app.MapBlazorHub();
            app.MapFallbackToPage("/_Host");

            app.Run();

        }
    }
}