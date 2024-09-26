using AutoMapper;
using CMS.API;
using CMS.Data.ContextModels;
using CMS.Data.FormModels;
using CMS.DESKTOPUI.CustomAuthentication;
using CMS.DESKTOPUI.Data;
using CMS.Repository.Interface;
using CMS.Repository.Repository;
using CMS.Repository;
using CMS.Services;
using CMS.Services.Interface;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Radzen;

var builder = WebApplication.CreateBuilder(args);

var startup = new Startup(builder.Configuration);
startup.ConfigureServices(builder.Services);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddSingleton<WeatherForecastService>();
builder.Services.AddScoped<DialogService>();
builder.Services.AddScoped<ProtectedSessionStorage>();
builder.Services.AddSession();
builder.Services.AddRadzenComponents();
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
builder.Services.AddScoped<NotificationService>();

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

app.UseAuthentication();
app.UseAuthorization();

app.UseSession();
app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
