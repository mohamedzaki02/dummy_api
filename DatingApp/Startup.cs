using DatingApp.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Diagnostics;
using DatingApp.Helpers;
using DatingApp.Services;
using DatingApp.Models;
using AutoMapper;
using System;
using Microsoft.OpenApi.Models;

namespace DatingApp
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
            services.AddDbContext<DataContext>(optionsbuilder =>
            {
                optionsbuilder.UseSqlServer(Configuration.GetConnectionString("DataContextDb"));
                optionsbuilder.UseLoggerFactory(LoggerFactory.Create(builder =>
                {
                    builder.AddFilter((category, level) => category == DbLoggerCategory.Database.Command.Name &&
                     level == LogLevel.Information)
                    .AddConsole();
                }));
            });


            services.AddScoped<IAuthRepository, AuthRepository>();
            services.AddScoped(typeof(IDatingRepository<>), typeof(DatingRepository<>));
            services.AddScoped<DataContext>();

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddJwtBearer(options => options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.ASCII.GetBytes(Configuration.GetSection("AppSettings:Token").Value))
                    });


            services.AddControllers(options => options.ReturnHttpNotAcceptable = true)
                    .AddXmlDataContractSerializerFormatters()
                    .AddNewtonsoftJson(opts => opts.SerializerSettings.ReferenceLoopHandling =
                    Newtonsoft.Json.ReferenceLoopHandling.Ignore);

            services.AddSwaggerGen(opts => opts.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "Dating Api",
                Description = "A Lightweight Dummy Api for Dating",
                Version = "v1"
            }));

            services.Configure<CloudinarySettings>(Configuration.GetSection("CloudinarySettings"));

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler(builder =>
                    builder.Run(async ctx =>
                    {
                        ctx.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        var errorObj = ctx.Features.Get<IExceptionHandlerFeature>();
                        if (errorObj != null)
                        {
                            // ctx.Response.Headers.Add("xxx-hasd",new[]{"test value"});
                            ctx.Response.AddApplicationError(errorObj.Error.Message);
                            await ctx.Response.WriteAsync(errorObj.Error.Message);
                        }
                    }));
            }

            // app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger();
            app.UseSwaggerUI(opts => opts.SwaggerEndpoint("/swagger/v1/swagger.json", "Dating Api"));
        }
    }
}
