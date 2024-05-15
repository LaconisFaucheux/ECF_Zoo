using Duende.Bff;
using Duende.Bff.Yarp;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

builder.Services.AddBff().AddRemoteApis();

builder.Services.AddAuthentication(options =>
    {
        options.DefaultScheme = "cookie";
        options.DefaultChallengeScheme = "oidc";
        options.DefaultSignOutScheme = "oidc";
    })
    .AddCookie("cookie", options =>
    {
        options.Cookie.Name = "__Host-blazor";
        options.Cookie.SameSite = SameSiteMode.Strict;
    })
    .AddOpenIdConnect("oidc", options =>
    {
        //URL d'accès au fournisseur d'identité OIDC
        options.Authority = builder.Configuration["ArcadiaAuthServerUrl"];
        //options.Authority = "https://demo.duendesoftware.com";

        //ID et code secret de l'appli client (ici l'API d'authentification IdentityServerHost)
        options.ClientId = "Client1";
        options.ClientSecret = "Secret1";

        options.ResponseType = "code";
        options.ResponseMode = "query";

        options.Scope.Clear();
        options.Scope.Add("openid");
        options.Scope.Add("profile");
        //options.Scope.Add("api");
        options.Scope.Add("arcadmin");
        options.Scope.Add("offline_access");


        options.MapInboundClaims = false;
        options.GetClaimsFromUserInfoEndpoint = true;

        //enregistre les jetons (auth et actu)
        options.SaveTokens = true;

        //Params necessaires à OIDC pour valider les jetons
        options.TokenValidationParameters = new TokenValidationParameters
        {
            NameClaimType = "name",
            RoleClaimType = "role"
        };
    });

builder.Services.AddUserAccessTokenManagement();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseBff();
app.UseAuthorization();

app.MapBffManagementEndpoints();


app.MapRazorPages();

// Ajoute les points de terminaison correspondants aux contrôleurs de l'API locale (du backend)
// avec la politique d'autorisation par défaut
// et indique qu'il s'agit de points de terminaison locaux du backend
app.MapControllers()
    .RequireAuthorization()
    .AsBffApiEndpoint();

// Mappe les points de terminaison de l'API externe sur l'API locale
app.MapRemoteBffApiEndpoint("/Animals", builder.Configuration["ApiUrl"] + "Animals")
       .RequireAccessToken(TokenType.User);
app.MapRemoteBffApiEndpoint("/Diets", builder.Configuration["ApiUrl"] + "Diets")
       .RequireAccessToken(TokenType.User);
app.MapRemoteBffApiEndpoint("/Habitats", builder.Configuration["ApiUrl"] + "Habitats")
       .RequireAccessToken(TokenType.User);

app.MapFallbackToFile("index.html");

app.Run();
