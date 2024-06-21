using Azure.Storage.Blobs;
using Inficare.Application.Common.Behaviours;
using Inficare.Application.Common.Interfaces;
using Inficare.Infrastructure;
using Inficare.Infrastructure.Common.Options;
using Inficare.Infrastructure.Persistence;
using Inficare.Infrastructure.Persistence.Identity;
using Inficare.Infrastructure.Services;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using SendGrid.Extensions.DependencyInjection;
using SendGrid.Helpers.Reliability;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Inficare.Infrastructure
{
    public static class DependencyInjection
    {
        private const string ACCOUNT_MFA_SCHEME_NAME = "MfaBearer";
        private const string APPLICATION_AUTH_SCHEME_NAME = "AuthBearer";

        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddOptions();
            services.Configure<JwtOptions>(configuration.GetSection(JwtOptions.SECTION_NAME));
            services.Configure<TwilioOptions>(configuration.GetSection(TwilioOptions.SECTION_NAME));
            services.Configure<EmailOptions>(configuration.GetSection(EmailOptions.SECTION_NAME));

            services.AddDatabase(configuration);
            services.AddSecurity(configuration);
            services.AddCorsPolicies(configuration);
            services.AddSendGrid(configuration);

            services.AddScoped<IEventDispatcherService, EventDispatcherService>();
            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(EventDispatcherBehavior<,>));
            services.AddScoped<IIdentityService, IdentityService>();
            services.AddScoped<ITwilioService, TwilioService>();
            services.AddScoped<IExchangeRate, ExchangeRate>();

        }

        private static void AddCorsPolicies(this IServiceCollection services,
                                            IConfiguration configuration)
        {
            services.AddCors(options =>
            {
                var section = configuration.GetSection(CorsOptions.SECTION_NAME);
                var corsConfig = new CorsOptions();
                section.Bind(corsConfig);

                options.AddDefaultPolicy(builder =>
                {
                    builder.WithOrigins(corsConfig.AllowedOrigins)
                           .AllowAnyMethod()
                           .AllowAnyHeader();
                });
            });
        }

        private static void AddDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<InficareDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("Inficare"));
            });
            services.AddScoped<IInficareDbContext>(provider => provider.GetService<InficareDbContext>());
        }

        private static void AddSecurity(this IServiceCollection services, IConfiguration configuration)
        {
            // For Identity
            services.AddIdentity<User, Role>(options =>
                {
                    // TODO : default value is false
                    options.SignIn.RequireConfirmedAccount = false;
                    options.User.RequireUniqueEmail = true;
                    options.Lockout.AllowedForNewUsers = true;
                    options.Lockout.MaxFailedAccessAttempts = 5;
                    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);
                })
                .AddEntityFrameworkStores<InficareDbContext>()
                .AddDefaultTokenProviders();

            // Adding Authentication
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            // Adding JWt Bearer - Mfa
            .AddJwtBearer(ACCOUNT_MFA_SCHEME_NAME, options =>
            {
                options.SaveToken = true;
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidAudience = configuration["JWT:Audience"],
                    ValidIssuer = configuration["JWT:Issuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Key"])),
                    ClockSkew = TimeSpan.Zero
                };
            })
            // Adding Jwt Bearer - Default
            .AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidAudience = configuration["JWT:Audience"],
                    ValidIssuer = configuration["JWT:Issuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Key"])),
                    ClockSkew = TimeSpan.Zero
                };
            });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("Mfa Endpoint", policy =>
                {
                    policy.AddAuthenticationSchemes(ACCOUNT_MFA_SCHEME_NAME);
                    policy.RequireAuthenticatedUser();
                    policy.RequireAssertion(context =>
                    {
                        var mfaClaim = context.User.Claims.FirstOrDefault(fd => fd.Type == ClaimTypes.AuthenticationMethod)?.Value;
                        return !string.IsNullOrEmpty(mfaClaim) && mfaClaim == "mfa";
                    });
                });
            });
        }

        private static void AddSendGrid(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSendGrid(options =>
            {
                options.ApiKey = configuration["Email:Key"];
                options.ReliabilitySettings = new ReliabilitySettings(1, TimeSpan.FromSeconds(1), TimeSpan.FromSeconds(10), TimeSpan.FromSeconds(3));
            });
            services.AddScoped<IEmailService, EmailService>();
        }
    }
}
