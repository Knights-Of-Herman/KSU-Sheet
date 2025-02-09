using KnightsOfHerman.Frontend.Common.BlazorViews.Services;
using KnightsOfHerman.Frontend.Common.Model.Communication.Hubs;
using KnightsOfHerman.Frontend.Common.Model.Sanctum;
using KnightsOfHerman.Frontend.Common.Model.Sanctum.Character.Implementation;
using KnightsOfHerman.Frontend.Common.Model.Sanctum.Interfaces;
using KnightsOfHerman.Frontend.Common.Model.User;
using KnightsOfHerman.Frontend.WebApp.Components;
using KnightsOfHerman.Frontend.WebApp.Services;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using MudBlazor.Services;
using ProfanityGuard.Core;

namespace KnightsOfHerman.Frontend.WebApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            //Initialize Profanity Guard
            //ProfanityChecker.Initialize();

            // Add services to the container.
            builder.Services.AddRazorComponents()
                .AddInteractiveServerComponents();

            builder.Services.AddMudServices();

            builder.Services.AddScoped<ProtectedSessionStorage>();

            builder.Services.AddScoped<AuthenticationStateProvider, UserStateProvider>();

            builder.Services.AddScoped<IUserController, UserController>();

            builder.Services.AddScoped<ICharacterListAccess, CharacterListAccess>();

            builder.Services.AddTransient<CharacterViewModelBuilder>();


            builder.Services.AddAuthentication("jwt")
                .AddCookie("koh_jwt", options =>
                {
                    options.Cookie.HttpOnly = true;
                    options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
                    options.ExpireTimeSpan = TimeSpan.FromMinutes(60);
                    options.LoginPath = "/account/login"; //not setup yet
                    options.LogoutPath = "/account/logout"; //not setup yet
                    options.AccessDeniedPath = "/accessdenied"; //not setup yet
                    options.SlidingExpiration = true;
                    //Eventuall add refresh token stuff here
                });

            builder.Services.AddCascadingAuthenticationState();
            builder.Services.AddOptions();
            builder.Services.AddAuthorizationCore();

            var serverURL = String.Empty;

            if (builder.Environment.IsDevelopment())
            {
                //Local Dev Settings
                builder.Configuration.AddEnvironmentVariables().AddJsonFile("appsettings.Development.json");

                serverURL = builder.Configuration.GetValue<string>("ServerURL_LOCAL");
                builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(serverURL) });
                builder.Services.AddScoped(sp => new HubFactory(serverURL));
            }
            else
            {
                //Production Settings
                serverURL = builder.Configuration.GetValue<string>("ServerURL");
                builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(serverURL) });
                builder.Services.AddScoped(sp => new HubFactory(serverURL));
            }

            var app = builder.Build();

            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseRouting();
            app.UseHttpsRedirection();
            app.UseHsts();
            app.UseStaticFiles();
            app.UseAntiforgery();

            app.MapRazorComponents<App>()
                .AddInteractiveServerRenderMode();

            app.Run();
        }
    }
}
