using Duende.IdentityServer.Models;
using IdentityServerHost.Data;
using IdentityServerHost.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

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

            builder.Services.AddRazorPages();

            // Ajoute et configure le service IdentityServer
            builder.Services.AddIdentityServer(options =>
                  options.Authentication.CoordinateClientLifetimesWithUserSession = true)

                // Cr�e des identit�s
                .AddInMemoryIdentityResources(new IdentityResource[] {
         new IdentityResources.OpenId(),
         new IdentityResources.Profile(),
                })

                // Configure une appli cliente
                .AddInMemoryClients(new Client[] {
         new Client
         {
            ClientId = "Client1",
            ClientSecrets = { new Secret("Secret1".Sha256()) },
            AllowedGrantTypes = GrantTypes.Code,

            // Urls auxquelles envoyer les jetons
            RedirectUris = { "https://localhost:6001/signin-oidc" },
            // Urls de redirection apr�s d�connexion
            PostLogoutRedirectUris = { "https://localhost:6001/signout-callback-oidc" },
            // Url pour envoyer une demande de d�connexion au serveur d'identit�
            FrontChannelLogoutUri = "https://localhost:6001/signout-oidc",

            // Etendues d'API autoris�es
            AllowedScopes = { "openid", "profile" },

            // Autorise le client � utiliser un jeton d'actualisation
            AllowOfflineAccess = true
         }
                })
                // Indique d'utiliser ASP.Net Core Identity pour la gestion des profils et revendications
                .AddAspNetIdentity<ArcadiaUser>();

            // Ajoute la journalisation au niveau debug des �v�nements �mis par Duende
            builder.Services.AddLogging(options =>
            {
                options.AddFilter("Duende", LogLevel.Debug);
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

            //ajoute le middleware d'authentification IdentityServer dane le pipeline
            app.UseIdentityServer();

            app.UseAuthorization();

            app.MapRazorPages();

            app.Run();
        }
    }
}
