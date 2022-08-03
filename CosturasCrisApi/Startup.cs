using CosturasCrisApi.Data;
using CosturasCrisApi.Services;
using CosturasCrisApi.Services.Contracts;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CosturasCrisApi
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

            services.AddControllers();
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidAudience = Configuration["Jwt:Audience"],
                    ValidIssuer = Configuration["Jwt:Issuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"]))
                };
            });
            services.AddCors(options => options.AddDefaultPolicy(
                builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()));
            //services.AddSwaggerGen(c =>
            //{
            //    c.SwaggerDoc("v1", new OpenApiInfo { Title = "CosturasCrisApi", Version = "v1" });
            //    c.AddSecurityDefinition("Bearer",
            //    new OpenApiSecurityScheme
            //    {
            //        In = ParameterLocation.Header,
            //        Description = "Please enter into field the word 'Bearer' following by space and JWT",
            //        Name = "Authorization",
            //        Type = SecuritySchemeType.ApiKey
            //    });
            //    c.AddSecurityRequirement(new OpenApiSecurityRequirement {
            //       {
            //         new OpenApiSecurityScheme
            //         {
            //           Reference = new OpenApiReference
            //           {
            //             Type = ReferenceType.SecurityScheme,
            //             Id = "Bearer"
            //           }
            //          },
            //         new string[] { }
            //       }
            //    });
            //});
            services.AddScoped<IAssociateService, AssociateService>();
            services.AddScoped<ICustomersService, CustomersService>();
            services.AddScoped<IProductServiceService, ProductServiceService>();
            services.AddScoped<IServiceCustomerService, ServiceCustomerService>();
            services.AddDbContext<CosturasCrisContext>(o =>
            o.UseSqlServer(Configuration.GetConnectionString("Con")));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            var path = Directory.GetCurrentDirectory();
            loggerFactory.AddFile($"{path}\\Logs\\Log.txt");
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                //app.UseSwagger();
                //app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "CosturasCrisApi v1"));
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();
            app.UseDefaultFiles();

            app.UseRouting();

            app.UseCors();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
