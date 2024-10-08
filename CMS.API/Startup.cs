using Autofac;
using AutoMapper;
using CMS.API.Extension;
using CMS.API.Utilities;
using CMS.Data.ContextModels;
using CMS.Repository;
using CMS.Repository.Interface;
using CMS.Repository.Repository;
using CMS.Services;
using CMS.Services.Interface;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.IdentityModel.Tokens.Jwt;

namespace CMS.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });

            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);

            services.AddControllers();
            services.AddRazorPages();
            services.AddMvc();
            services.AddSession();
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<ILawyerService, LawyerService>();
            services.AddTransient<IClientService, ClientService>();
            services.AddTransient<ICaseService, CaseService>();
            services.AddTransient<IAccountService, AccountService>();
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<ILawyerRepository, LawyerRepository>();
            services.AddTransient<IClientRepository, ClientRepository>();
            services.AddTransient<ICaseRepository, CaseRepository>();
            services.AddDbContext<ApplicationContext>(item => item.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.IntegrateSwagger();
            services.AddSwaggerGen();
            //For Identity
            services.IdentityImplementation();

            #region Adding Authentication
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            });
            #endregion

            #region JWT bearer
            //.AddJwtBearer(options =>
            //{
            //    options.SaveToken = true;
            //    options.RequireHttpsMetadata = false;
            //    //options.TokenValidationParameters = new TokenValidationParameters()
            //    //{
            //    //    ValidIssuer = Configuration["JWT:JwtIssuer"],
            //    //    ValidAudience = Configuration["JWT:JwtIssuer"],
            //    //    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JWT:JwtKey"])),
            //    //    ClockSkew = TimeSpan.Zero // remove delay of token when expire
            //    //};
            //});
            //services.AuthorizationHandler();
            #endregion
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            // Configure custom container.
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "CMS.API v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseSession();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
