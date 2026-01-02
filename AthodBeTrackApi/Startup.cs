using AthodBeTrackApi.Repositories;
using AthodBeTrackApi.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;

namespace AthodBeTrackApi
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

            services.AddControllers()
               .AddJsonOptions(options =>
               {
                   options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
               });
            services.AddHttpClient();
            services.AddDirectoryBrowser();


            #region Swagger Configuration
            services.AddSwaggerGen(swagger =>
            {
                swagger.SwaggerDoc("v1", new OpenApiInfo { Title = "AthodBeTrack", Version = "v1" });

                // To Enable authorization using Swagger (JWT)
                swagger.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Description = "This site uses Bearer token and you have to pass " + "It as Bearer<<space>>Token",
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                });
                swagger.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                          new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = "Bearer"
                                },
                                Scheme="oauth2",
                                Name = "Bearer",
                                In = ParameterLocation.Header
                            },
                            new List<string>()
                    }
                });
            });
            #endregion

            #region Authentication
            var jwtkey = Configuration.GetValue<string>("JwtSettings:Key");
            var keyBytes = Encoding.ASCII.GetBytes(jwtkey);

            TokenValidationParameters tokenValidation = new TokenValidationParameters
            {
                IssuerSigningKey = new SymmetricSecurityKey(keyBytes),
                ValidateLifetime = true,
                ValidateAudience = false,
                ValidateIssuer = false,
                ClockSkew = TimeSpan.Zero

            };
            services.AddSingleton(tokenValidation);
            services.AddAuthentication(authOptions =>
            {
                authOptions.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                authOptions.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(jwtOptions =>
            {
                jwtOptions.TokenValidationParameters = tokenValidation;
            });

            #endregion

            services.AddTransient<IJWTManagerService, JWTManagerService>();
            services.AddAutoMapper(typeof(Startup));

            #region REGISTER REPOSITORY

            services.AddScoped<IGenericRepository, GenericRepository>();
            services.AddScoped<IAccountRepository, AccountRepository>();
            services.AddScoped<IActivityRepository, ActivityRepository>();
            services.AddScoped<ITagRepository, TagRepository>();
            services.AddScoped<IActivityQuestionSetRepository, ActivityQuestionSetRepository>();
            services.AddScoped<IDashboardRepository, DashboardRepository>();
            services.AddScoped<IDropdownRepository, DropdownRepository>();
            services.AddScoped<IReportRepository, ReportRepository>();
            services.AddScoped<IMasterRepository, MasterRepository>();
            services.AddScoped<ILocationRepository, LocationRepository>();
            services.AddScoped<IIndicatorDueRepository, IndicatorDueRepository>();
            #endregion

            #region REGISTER SERVICES

            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IDashboardService, DashboardService>();
            services.AddScoped<IUserLocationService, UserLocationService>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<ITagService, TagService>();
            services.AddScoped<IMasterService, MasterService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IQuestionChoiceService, QuestionChoiceService>();
            services.AddScoped<IQuestionChoiceItemService, QuestionChoiceItemService>();
            services.AddScoped<IQuestionGroupService, QuestionGroupService>();
            services.AddScoped<IQuestionBankService, QuestionBankService>();
            services.AddScoped<IActivityService, ActivityService>();
            services.AddScoped<IActivityQuestionService, ActivityQuestionService>();
            services.AddScoped<IActivityQuestionSetService, ActivityQuestionSetService>();
            services.AddScoped<IEmailConfigService, EmailConfigService>();
            services.AddScoped<ILocationService, LocationService>();
            services.AddScoped<IFileUploadService, FileUploadService>();
            services.AddScoped<IDropdownService, DropdownService>();
            services.AddScoped<IReportService, ReportService>();
            services.AddScoped<IIndicatorDueService, IndicatorDueService>();
            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

            }
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "AthodBeTrack"));
            loggerFactory.AddFile("Logs/AthodBeTrack-{Date}.txt");

            app.UseCors(x => x
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());


            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseDefaultFiles();
            app.UseStaticFiles();

            //app.UseDirectoryBrowser();
            app.UseSwaggerAuthorized();
            app.UseSwagger();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
