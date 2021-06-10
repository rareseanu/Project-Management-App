using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using ProjectManagementApp.Filters;
using ProjectManagementApp.Helpers;
using ProjectManagementApp.Middlewares;
using ProjectManagementApp.Models;
using ProjectManagementApp.Models.Database;
using ProjectManagementApp.Models.Database.Entities;
using ProjectManagementApp.Repositories;
using ProjectManagementApp.Services;
using Serilog;
using Serilog.Events;
using System;
using System.Text;

namespace ProjectManagementApp
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
            services.AddHttpContextAccessor();

            services.AddDbContext<ProjectManagementDbContext>(o => o.UseSqlServer(
                Configuration.GetConnectionString("ProjectManagementDb")));
            services.AddIdentity<UserEntity, RoleEntity>(options =>
            {
                options.SignIn.RequireConfirmedAccount = false;
                options.SignIn.RequireConfirmedEmail = false;
                options.SignIn.RequireConfirmedPhoneNumber = false;

                options.Password.RequiredLength = 8;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
            })
                .AddEntityFrameworkStores<ProjectManagementDbContext>()
                .AddDefaultTokenProviders();

            services.AddScoped<ItemListEntityRepository>();
            services.AddScoped<ItemListEntityService>();
            services.AddScoped<BoardEntityRepository>();
            services.AddScoped<BoardEntityService>();
            services.AddScoped<BoardUserEntityRepository>();
            services.AddScoped<ItemLabelEntityRepository>();
            services.AddScoped<CheckListEntityService>();
            services.AddScoped<CheckListEntityRepository>();
            services.AddScoped<LabelEntityRepository>();
            services.AddScoped<LabelEntityService>();
            services.AddScoped<CommentaryEntityRepository>();
            services.AddScoped<CommentaryEntityService>();
            services.AddScoped<CheckItemEntityRepository>();
            services.AddScoped<CheckItemEntityService>();
            services.AddScoped<ItemEntityRepository>();
            services.AddScoped<ItemEntityService>();
            services.AddScoped<UserRepository>();
            services.AddScoped<UserService>();
            services.AddControllers();

            services.AddAuthorization()
                .AddAuthentication(o =>
                {
                    o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    o.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                    o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(o =>
                {
                    o.RequireHttpsMetadata = false;
                    o.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidIssuer = "https://projectmanagement.ro",
                        ValidAudience = "https://projectmanagement.ro",
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                            @"hfwehdfuhf0jf-23jd9-83u9830ffjn4jffkerfj4j32f9043jfjifjrefjre")),
                        ClockSkew = TimeSpan.Zero
                    };
                });

            services.AddHostedService<SeedDatabaseHostedService>();
            services.AddHostedService<DisableInactiveUsersHostedService>();

            services.AddSingleton(new MapperConfiguration(p => p.AddProfile(new MappingProfile())).CreateMapper());
            services.AddControllers().AddNewtonsoftJson(x =>
                x.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore);

            services.AddScoped<EmailService>();
            services.AddSingleton(Configuration.GetSection("EmailConfiguration").Get<EmailConfiguration>());

            services.AddMvcCore(o => o.Filters.Add(new TokenAuthorizationFilter()));
            //services.AddMvcCore(o => o.Filters.Add(new LoggedInTodayFilter()));

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Project Management App", Version = "v1" });
            });

            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
                .Enrich.WithProperty("Application", "ProjectManagementApp")
                .Enrich.FromLogContext()
                .WriteTo.Seq("http://localhost:44334")
                .CreateLogger();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMiddleware<ExceptionMiddleware>();

            app.UseSwagger();

            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Project Management App"));

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
