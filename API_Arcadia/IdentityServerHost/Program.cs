using Duende.IdentityServer.Models;
using IdentityServerHost.Controllers;
using IdentityServerHost.Data;
using IdentityServerHost.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace IdentityServerHost
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString));

            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            builder.Services.AddDefaultIdentity<ArcadiaUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<ApplicationDbContext>();

            builder.Services.AddControllers();

            builder.Services.AddRazorPages();

            // Ajoute et configure le service IdentityServer
            builder.Services.AddIdentityServer(options =>
                  options.Authentication.CoordinateClientLifetimesWithUserSession = true)

                // Cr�e des identit�s
                .AddInMemoryIdentityResources(new IdentityResource[] {
                    new IdentityResources.OpenId(),
                    new IdentityResources.Profile(),
                })

                // Crée une étendue d'API "entreprise" et lui associe la revendication Fonction
                .AddInMemoryApiScopes(new ApiScope[] {
                    new ApiScope("arcadmin", new[] { "Fonction" })
                })

                // Configure une appli cliente
                .AddInMemoryClients(new Client[] {
                    new Client
                    {
                       ClientId = "Client1",
                       ClientSecrets = { new Secret("Secret1".Sha256()) },
                       AllowedGrantTypes = GrantTypes.Code,

                       // Urls auxquelles envoyer les jetons
                       RedirectUris = { "https://localhost:7189/signin-oidc" },
                       // Urls de redirection apr�s d�connexion
                       PostLogoutRedirectUris = { "https://localhost:7189/signout-callback-oidc" },
                       // Url pour envoyer une demande de d�connexion au serveur d'identit�
                       FrontChannelLogoutUri = "https://localhost:7189/signout-oidc",

                       // Etendues d'API autoris�es
                       AllowedScopes = { "openid", "profile", "arcadmin" },

                       // Autorise le client � utiliser un jeton d'actualisation
                       AllowOfflineAccess = true,
                       // Configuration pour inclure les revendications de rôle dans le token
                       AlwaysIncludeUserClaimsInIdToken = true
                    }
                })
                // Indique d'utiliser ASP.Net Core Identity pour la gestion des profils et revendications
                .AddAspNetIdentity<ArcadiaUser>();

            // Ajoute la journalisation au niveau debug des �v�nements �mis par Duende
            builder.Services.AddLogging(options =>
            {
                options.AddFilter("Duende", LogLevel.Debug);
            });

            builder.Services.AddAuthorization(options =>
            {
                //Spécifie que TOUT utilsateur doitêtre authentifié par défaut
                options.FallbackPolicy = new AuthorizationPolicyBuilder()
                    .RequireAuthenticatedUser()
                    //.RequireAssertion(context => true)
                    .Build();

                options.AddPolicy("CreateUser", p => p.RequireClaim("Fonction", "Administrateur"));
                options.AddPolicy("ReadUser", p => p.RequireClaim("Fonction", "Administrateur"));
                options.AddPolicy("UpdateUser", p => p.RequireClaim("Fonction", "Administrateur"));
                options.AddPolicy("DeleteUser", p => p.RequireClaim("Fonction", "Administrateur"));
            });

            // Ajout de la configuration CORS
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("MyPolicy", builder =>
                {
                    builder.WithOrigins("https://localhost:7189")
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials();
                });
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            // Application de la politique CORS
            app.UseCors("MyPolicy");

            //ajoute le middleware d'authentification IdentityServer dane le pipeline

            app.UseIdentityServer();

            app.UseAuthorization();

            app.MapRazorPages();
            app.MapControllers();

            app.Run();

        }
    }
}
