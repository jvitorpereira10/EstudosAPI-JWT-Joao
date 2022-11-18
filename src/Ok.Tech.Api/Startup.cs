using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Ok.Tech.Api.Extensions;
using Ok.Tech.Api.Extensions.Security;
using Ok.Tech.Application;
using Ok.Tech.Domain.Applications;
using Ok.Tech.Domain.Notifications;
using Ok.Tech.Domain.Repositories;
using Ok.Tech.Infra.Data.Contexts;
using Ok.Tech.Infra.Data.Repositories;
using System;
using System.Text;

namespace Ok.Tech.Api
{
  public class Startup
  {
    public IConfiguration Configuration { get; set; }

    public Startup(IConfiguration configuration)
    {
      Configuration = configuration;
    }

    // This method gets called by the runtime. Use this method to add services to the container.
    // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
    public void ConfigureServices(IServiceCollection services)
    {
      services.AddDbContext<ApiDbContext>(options =>
      {
        options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
      });

      services.AddAutoMapper(typeof(Startup));

      services.AddScoped<IProductRepository, ProductRepository>();

      services.AddScoped<IPriceListRepository, PriceListRepository>();

      services.AddScoped<IPriceRepository, PriceRepository>();

      services.AddScoped<IUnitOfWork, UnitOfWork>();

      services.AddScoped<IProductApplication, ProductApplication>();

      services.AddScoped<IPriceListApplication, PriceListApplication>();

      services.AddScoped<IPriceApplication, PriceApplication>();

      services.AddScoped<INotifier, Notifier>();

      services.AddScoped<ITokenManager, TokenManager>();

      services.AddSwaggerGen(setup =>
      {
        setup.SwaggerDoc("v1", new OpenApiInfo
        {
          Title = "API JOAO",
          Description = "API OK.TECH - JOAO",
          Contact = new OpenApiContact
          {
            Name = "João Vitor Santos",
            Email = "jvitorpereira10@gmail.com"
          },
          License = new OpenApiLicense
          {
            Name = "MIT",
            Url = new Uri("https://opensource.org/licenses/MIT")
          }
        });

        setup.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
        {
          Description = "Insert the token JWT like this: Bearer {token}",
          Name = "Authorization",
          Scheme = "Bearer",
          BearerFormat = "JWT",
          In = ParameterLocation.Header,
          Type = SecuritySchemeType.ApiKey
        });

        setup.AddSecurityRequirement(new OpenApiSecurityRequirement
          {
                    {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[]{}
        }
          });
      });

      IConfiguration appSettingsSection = Configuration.GetSection("AppSettings");

      services.Configure<AppSettings>(appSettingsSection);

      AppSettings appSettings = appSettingsSection.Get<AppSettings>();

      byte[] key = Encoding.ASCII.GetBytes(appSettings.Secret);

      services.AddAuthentication(authOptions =>
      {
        authOptions.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        authOptions.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
      }).AddJwtBearer(jwtBearerOptions =>
      {
        jwtBearerOptions.RequireHttpsMetadata = false;
        jwtBearerOptions.SaveToken = true;
        jwtBearerOptions.TokenValidationParameters = new TokenValidationParameters
        {
          ValidateIssuerSigningKey = true,
          IssuerSigningKey = new SymmetricSecurityKey(key),
          ValidateIssuer = true,
          ValidIssuer = appSettings.Emitter,
          ValidateAudience = true,
          ValidAudience = appSettings.ValidIn
        };
      });

      services.Configure<ApiBehaviorOptions>(options => { options.SuppressModelStateInvalidFilter = true; });

      services.AddControllers(options => { options.SuppressAsyncSuffixInActionNames = false; });
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }

      app.UseSwagger();

      app.UseSwaggerUI(setup =>
      {
        setup.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
        setup.RoutePrefix = string.Empty;
      });

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