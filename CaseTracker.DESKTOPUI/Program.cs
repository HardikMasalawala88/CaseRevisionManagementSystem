using AutoMapper;
using CaseTracker.API;
using CaseTracker.Data.ContextModels;
using CaseTracker.Data.FormModels;
using CaseTracker.DESKTOPUI.CustomAuthentication;
using CaseTracker.DESKTOPUI.Data;
using CaseTracker.Repository.Interface;
using CaseTracker.Repository.Repository;
using CaseTracker.Repository;
using CaseTracker.Services;
using CaseTracker.Services.Interface;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using MudBlazor.Services;
using MudBlazor;
using Radzen;
using Blazored.Toast;

var builder = WebApplication.CreateBuilder(args);

var startup = new Startup(builder.Configuration);
startup.ConfigureServices(builder.Services);

//builder.Services.Configure<StripeSettings>(builder.Configuration.GetSection("PaymentSettings"));

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddSingleton<WeatherForecastService>();
builder.Services.AddSingleton<TitleService>();
builder.Services.AddScoped<MudBlazor.DialogService>();
builder.Services.AddScoped<Radzen.DialogService>();
builder.Services.AddScoped<ProtectedSessionStorage>();
builder.Services.AddSession();
builder.Services.AddBlazoredToast();
//builder.Services.AddRadzenComponents();
builder.Services.AddMudServices(config =>
{
    config.SnackbarConfiguration.PositionClass = MudBlazor.Defaults.Classes.Position.TopRight;

    config.SnackbarConfiguration.PreventDuplicates = false;
    config.SnackbarConfiguration.NewestOnTop = false;
    config.SnackbarConfiguration.ShowCloseIcon = true;
    config.SnackbarConfiguration.VisibleStateDuration = 10000;
    config.SnackbarConfiguration.HideTransitionDuration = 500;
    config.SnackbarConfiguration.ShowTransitionDuration = 500;
    config.SnackbarConfiguration.SnackbarVariant = MudBlazor.Variant.Filled;
});

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
builder.Services.AddTransient<ISubscriptionService, SubscriptionService>();
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthStateProvider>();
builder.Services.AddScoped<NotificationService>();
builder.Services.AddHttpClient();

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
