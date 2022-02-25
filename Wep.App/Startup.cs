using Authorization.Impl;
using Authorization.Impl.Settings;
using Authorization.Interfaces;
using DataAccess.Implementation;
using DataAccess.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using UseCases.User.Commands.CreateUser;
using Wep.App.Middlewares;

namespace Wep.App
{
    public class Startup
    {
        private readonly IConfiguration _cfg;
        public Startup(IConfiguration configuration)
        {
            _cfg = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<IDbContext, AppDbContext>(x =>
            {
                x.UseSqlServer(_cfg.GetConnectionString("Default"));
            });

            services.AddScoped<IJwtTokenProvider, JwtTokenProvider>();
            services.AddScoped<ICurrentUserProvider, CurrentUserProvider>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();


            var jwtSettings = _cfg.GetSection(nameof(JwtSecuritySettings)).Get<JwtSecuritySettings>();
            services.AddSingleton(jwtSettings);
            services.AddControllers();
            services.AddMediatR(typeof(CreateUserRequest).Assembly);
            services.AddSwaggerGen(x =>
            {
                x.SwaggerDoc("v1", new OpenApiInfo() { Title = "API", Version = "v1" });
                x.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "Bearer auth",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey
                });
                x.AddSecurityRequirement(new OpenApiSecurityRequirement {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    new string[] { }
                }});
            });

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(x =>
                {
                    x.RequireHttpsMetadata = false;
                    x.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidIssuer = jwtSettings.Issuer,

                        ValidateAudience = true,
                        ValidAudience = jwtSettings.Audience,

                        ValidateLifetime = true,
                        IssuerSigningKey = jwtSettings.GetSymmetricKey(),
                        ValidateIssuerSigningKey = true
                    };
                });
           
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMiddleware<ExceptionHandler>();
            app.UseRouting();
            app.UseCors(x =>
            {
                x.AllowAnyHeader();
                x.AllowAnyOrigin();
            });
            app.UseSwagger(x => x.SerializeAsV2 = true);
            app.UseSwaggerUI(x => x.SwaggerEndpoint("/swagger/v1/swagger.json", "API"));


            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
