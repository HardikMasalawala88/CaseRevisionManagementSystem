using CMS.FluentUI.Components;
using CMS.Services.Interface;
using CMS.Services;
using Microsoft.FluentUI.AspNetCore.Components;
using CMS.API;
using AutoMapper;
using CMS.Repository.Interface;
using CMS.Repository.Repository;
using CMS.Repository;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using CMS.Data.FormModels;
using CMS.Data.ContextModels;
using CMS.FluentUI.CustomAuthentication;

namespace CMS.FluentUI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var startup = new Startup(builder.Configuration);

            startup.ConfigureServices(builder.Services);

            // Add services to the container.
            builder.Services.AddRazorComponents().AddInteractiveServerComponents();
            builder.Services.AddFluentUIComponents();
            builder.Services.AddScoped<DialogService>();
            builder.Services.AddScoped<ProtectedSessionStorage>();
            builder.Services.AddSession();
            builder.Services.AddAuthorizationCore();
            builder.Services.AddAuthenticationCore();
            builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            builder.Services.AddTransient<IUserService, UserService>();
            builder.Services.AddTransient<ILawyerService, LawyerService>();
            builder.Services.AddTransient<IClientService, ClientService>();
            builder.Services.AddTransient<ICaseService, CaseService>();
            builder.Services.AddTransient<ICaseDocumentRepository, CaseDocumentRepository>();
            builder.Services.AddTransient<IAccountService, AccountService>();
            builder.Services.AddTransient<IUserRepository, UserRepository>();
            builder.Services.AddTransient<ILawyerRepository, LawyerRepository>();
            builder.Services.AddTransient<IClientRepository, ClientRepository>();
            builder.Services.AddTransient<ICaseRepository, CaseRepository>();
            builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthStateProvider>();

            //Initialize the mapper
            var config = new MapperConfiguration(cfg =>
                    cfg.CreateMap<RegisterFM, User>()
                );

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
            app.UseAntiforgery();

            app.MapRazorComponents<App>()
                .AddInteractiveServerRenderMode();

            app.Run();
        }
    }
}
