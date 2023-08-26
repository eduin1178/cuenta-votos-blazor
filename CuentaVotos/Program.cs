
using CuentaVotos.Core.Account;
using CuentaVotos.Core.Account.Login;
using CuentaVotos.Data.LiteDb;
using CuentaVotos.Repository;
using CuentaVotos.Services;
using CuentaVotos.Sqlite;
using LiteDB;
using Microsoft.EntityFrameworkCore;

namespace CuentaVotos
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorPages();
            builder.Services.AddServerSideBlazor();

            
            builder.Services.AddSingleton(x=> new LiteDbContext(builder.Configuration.GetValue<string>("ConnectionStrings:AppDbContext")));

            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<LoginModel>();
            builder.Services.AddScoped<UsersList>();
            builder.Services.AddScoped<UserRegisterModel>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseRouting();

            app.MapBlazorHub();
            app.MapFallbackToPage("/_Host");

            app.Run();
        }
    }
}