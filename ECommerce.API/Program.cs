
using ECommerce.Infra;
using ECommerce.Infra.Abstracts;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace ECommerce.API
{
    public class Program
    {
        public async static Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                var Key = Encoding.UTF8.GetBytes(builder.Configuration.GetSection("Security:JWT-Key").Value);
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = builder.Configuration.GetSection("Security:JWT-Issuer").Value,
                    ValidAudience = builder.Configuration.GetSection("Security:JWT-Audience").Value,
                    IssuerSigningKey = new SymmetricSecurityKey(Key)
                };
            });

            //application dependencies
            string con = builder?.Configuration?.GetConnectionString("Default");
            var DI = new DependencyResolver(builder.Services);
            DI.Execute(con);
            builder.Services.AddSingleton<IConfiguration>(builder.Configuration);
            var app = builder.Build();
            //create db if not exists
            await DI.EnsureDataBasePresence(builder.Services);
            //get current logger service and initialize it
            var logger = app.Services.GetService<ICustomLogger>();
            logger.Initialize(con);

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseExceptionHandler(err =>
            {
                err.Run(async ctx =>
                {
                    var contextFeature = ctx.Features.Get<IExceptionHandlerFeature>();
                    if (contextFeature != null)
                    {
                        logger.Error(contextFeature.Error);
                        await ctx.Response.WriteAsJsonAsync(contextFeature.Error.Message);
                    }
                });
            });

            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
