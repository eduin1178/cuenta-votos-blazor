using CuentaVotos.Data.LiteDb;
using CuentaVotos.Repository;
using CuentaVotos.Services;
using CuentaVotos.Storage;
using Elecciones.Server.Hubs;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http.Connections;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;

namespace Elecciones
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllersWithViews();
            builder.Services.AddRazorPages();
            builder.Services.AddSignalR();
            builder.Services.AddResponseCompression(opts =>
            {
                opts.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(
                    new[] { "application/octet-stream" });
            });
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
            builder.Services.AddTransient<IUserRespository, UserRepository>();
            builder.Services.AddTransient<IPuestoRepository, PuestoRepository>();
            builder.Services.AddTransient<IMesaRepository, MesaRepository>();

            builder.Services.AddTransient<ICargosRepository, CargosRepository>();
            builder.Services.AddTransient<IPartidosRepository, PartidosRespository>();
            builder.Services.AddTransient<ICandidatosRepository, CandidatosRepository>();
            builder.Services.AddTransient<IResultadosRepository, ResultadosRepository>();
            builder.Services.AddTransient<IProcesoRespository, ProcesoRespository>();
            builder.Services.AddScoped<NotifyResultHub>();

            builder.Services.AddTransient<ICuentaVotosStorage, BunnyStorage>();
            var configBunny = new BunnyStorageConfig();
            builder.Configuration.Bind("BunnyStorageConfig", configBunny);
            builder.Services.AddSingleton(configBunny);

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseWebAssemblyDebugging();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseBlazorFrameworkFiles();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();


            app.MapRazorPages();
            app.MapControllers();
            app.MapFallbackToFile("index.html");
            app.MapHub<NotifyResultHub>("/Resultados/Noify",
                   o => o.Transports = HttpTransportType.WebSockets);

            app.Run();
        }
    }
}