using MassTransit;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using RoomService.Application.Consumers;
using RoomService.Application.Contracts;
using RoomService.Infrastructure.Context;
using RoomService.Infrastructure.Repositories;

namespace RoomService.Infrastructure.IOC.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddApiDoc(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                var path = Path.Combine(Directory.GetCurrentDirectory(), "readme.md");
                var description = $"Room Service API";

                if (File.Exists(path))
                    description = File.ReadAllText(path);

                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Room Service API",
                    Version = "v1",
                    Description = description
                });

                // Add JWT Authentication to Swagger
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = "Bearer"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                },
                new string[] {}
            }
        });

                c.ResolveConflictingActions(x => x.First());
            });

            return services;
        }

        public static IServiceCollection AddCustomIntegrationTransport(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMassTransit(x =>
            {
                x.AddEntityFrameworkOutbox<AuctionDbContext>(o =>
                {
                    o.QueryDelay = TimeSpan.FromSeconds(10);

                    o.UseSqlServer();
                    o.UseBusOutbox();
                });

                x.AddConsumersFromNamespaceContaining<AuctionCreatedFaultConsumer>();

                x.SetEndpointNameFormatter(new KebabCaseEndpointNameFormatter("auction", false));

                x.UsingRabbitMq((context, cfg) =>
                {
                    cfg.UseRetry(r =>
                    {
                        r.Handle<RabbitMqConnectionException>();
                        r.Interval(5, TimeSpan.FromSeconds(10));
                    });
                    cfg.Host(configuration["RabbitMq:Host"], "/", host =>
                    {
                        host.Username(configuration.GetValue("RabbitMq:Username", "guest"));
                        host.Password(configuration.GetValue("RabbitMq:Password", "guest"));
                    });

                    cfg.ConfigureEndpoints(context);
                });
            });

            return services;
        }

        public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AuctionDbContext>(opt =>
            {
                opt.UseSqlServer(configuration.GetConnectionString("AuctionConnection"));
            });

            return services;
        }

        public static IServiceCollection AddCustomAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
              .AddJwtBearer(options =>

              {
                  options.Authority = configuration["IdentityServiceUrl"];
                  options.RequireHttpsMetadata = false;
                  options.TokenValidationParameters.ValidateAudience = false;
                  options.TokenValidationParameters.NameClaimType = "username";
              });

            return services;
        }

        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            return services
                .AddScoped<IAuctionRepository, AuctionRepository>();

            
        }
    }
}