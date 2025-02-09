using KnightsOfHerman.Backend.Common.Config;
using KnightsOfHerman.Backend.Common.JWT;
using KnightsOfHerman.Backend.Common.User.Abstract;
using KnightsOfHerman.Backend.Common.User.Implementation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.EntityFrameworkCore;
using KnightsOfHerman.Backend.Database.Azure;
using KnightsOfHerman.Backend.Common.Database.Interfaces.User;
using KnightsOfHerman.Backend.Database.Azure.User;
using KnightsOfHerman.Backend.Common.Database.Interfaces.Sanctum.Character;
using KnightsOfHerman.Backend.Database.Azure.Character;
using KnightsOfHerman.Backend.Common.Sanctum.Character.Interfaces;
using KnightsOfHerman.Backend.Server.Memory;
using KnightsOfHerman.Backend.Common.Sanctum.Character.Services;
using KnightsOfHerman.Backend.Server.Services;
using KnightsOfHerman.Backend.Server.SignalRHubs;
using ProfanityGuard.Core;

namespace KnightsOfHerman.Backend.Server
{
    public class Program
    {
        //Main EntryPoint
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            ProfanityChecker.Initialize();

            ConfigureServices(builder);

            var app = builder.Build();

            ConfigureApp(app);

            app.Run();
        }
        internal static void ConfigureServices(WebApplicationBuilder builder)
        {
            var services = builder.Services;
            //Setup Database Connection
            var connection = String.Empty;
            if (builder.Environment.IsDevelopment())
            {

                //Local Settings
                builder.Configuration.AddEnvironmentVariables().AddJsonFile("appsettings.Development.json");
                connection = builder.Configuration.GetConnectionString("AZURE_SQL_CONNECTIONSTRING_LOCAL_DEV");

                //Debug stuff

                // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
                services.AddEndpointsApiExplorer();
                services.AddSwaggerGen();
            }
            else
            {
                //Live Settings
                connection = builder.Configuration.GetConnectionString("AZURE_SQL_CONNECTIONSTRING");
            }
            services.AddDbContext<AzureDBContext>(options => options.UseSqlServer(connection));

            services.AddSignalR();

            //Used for storing large datasets in memory
            services.AddMemoryCache();

            //Configre CORS Configuration
            services.AddCors(options =>
            {
                options.AddDefaultPolicy(builder =>
                {
                    builder.AllowAnyOrigin()
                           .AllowAnyMethod()
                           .AllowAnyHeader();
                });
            });

            //Add API Controllers
            services.AddControllers();

            //Add Config Services
            var authConfig = new JWTConfig(); //Constructed here for use in JWT authentication setup
            services.AddSingleton(authConfig);
            services.AddScoped<JWTService>();

            services.AddSingleton<ICharacterLockService, CharacterLockService>();
            services.AddScoped<ICharacterNotificationService, CharacterNotificationService>();

            //Add Database Controllers
            services.AddScoped<IUserDB, UserDB>();
            services.AddScoped<ICharacterDB, CharacterDB>();
            services.AddScoped<ICharacterCache, CharacterCache>();
            services.AddScoped<ICharacterDBService, CharacterDBService>();

            //Add API Controllers
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ICharacterService, CharacterService>();


            //Add Memory Caches
            services.AddScoped<ICharacterCache, CharacterCache>();
            services.AddHostedService<CharacterCache>();

            //Setup JWT Bearer
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authConfig.JWTSecret)),
                        ValidateIssuer = false, // Set to true and define valid issuer if needed
                        ValidateAudience = false, // Set to true and define valid audience if needed
                        ClockSkew = TimeSpan.Zero // Optional: reduce or eliminate clock skew allowance
                    };
                });
            services.AddAuthorization();

        }

        internal static void ConfigureApp(WebApplication app)
        {

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.MapControllers();

            app.UseRouting();

            app.UseCors();

            //Setup JWT Authorization and Authentication
            app.UseAuthentication();
            app.UseAuthorization();

            //Map SignalR Hubs
            //app.MapHub<TestHub>("/testhub");
            app.MapHub<CharacterHub>("/characterhub");
            
            //Map Http APIs
            app.MapControllers();
        }
    }
}
