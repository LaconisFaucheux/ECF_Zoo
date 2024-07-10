
using API_Arcadia.Interfaces;
using API_Arcadia.Models.Data;
using API_Arcadia.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using System.Text;

namespace API_Arcadia
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            string? connect1 = builder.Configuration.GetConnectionString("ArcadiaConnect");

            // Add services to the container.
            builder.Services.AddDbContext<ContextArcadia>(
                opt => opt.UseSqlServer(connect1));

            builder.Services.AddDbContext<AuthDbContext>(
                opt => opt.UseSqlServer(connect1));

            //Business Services
            builder.Services.AddScoped<IAnimalService, AnimalService>();
            builder.Services.AddScoped<IHabitatService, HabitatService>();
            builder.Services.AddScoped<ISpeciesService, SpeciesService>();
            builder.Services.AddScoped<IVetVisitService, VetVisitService>();

            //Identity Config
            builder.Services.AddIdentityCore<IdentityUser>()
                .AddRoles<IdentityRole>()
                .AddTokenProvider<DataProtectorTokenProvider<IdentityUser>>("Arcadia")
                .AddEntityFrameworkStores<AuthDbContext>()
                .AddDefaultTokenProviders();
            builder.Services.Configure<IdentityOptions>(opt =>
            {
                opt.Password.RequireDigit = false;
                opt.Password.RequireLowercase = false;
                opt.Password.RequireUppercase = false;
                opt.Password.RequireNonAlphanumeric = false;
                opt.Password.RequiredLength = 6;
                opt.Password.RequiredUniqueChars = 1;
            });

            //Authentication Config
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(opt =>
                {
                    opt.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                    {
                        AuthenticationType = "Jwt",
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = builder.Configuration["Jwt:Issuer"],
                        ValidAudience = builder.Configuration["Jwt:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
                    };
                });


            //Serilog
            var logger = new LoggerConfiguration()
                .ReadFrom.Configuration(builder.Configuration)
                .Enrich.FromLogContext()
                .CreateLogger();
            builder.Logging.ClearProviders();
            builder.Logging.AddSerilog(logger);

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();


            //Ajoute le service d'authentication par jeton JWT
            //builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            //    .AddJwtBearer(options =>
            //     {
            //         //url d'accès au serveur d'identités
            //         options.Authority = builder.Configuration["ArcadiaAuthServerUrl"];
            //         options.TokenValidationParameters.ValidateAudience = false;

            //         //Tolérance sur la validité du jeton (à modifier/supprimer (par défaut c'est 5 min) à la mise en prod pour que l'API accepte des tokens dépassés de quelques min)
            //         //options.TokenValidationParameters.ClockSkew = TimeSpan.Zero;
            //     });

            //ajoute le service d'autorisation
            //builder.Services.AddAuthorization(options => 
            //{
            //    //Spécifie que TOUT utilsateur doitêtre authentifié par défaut
            //    options.FallbackPolicy = new AuthorizationPolicyBuilder()
            //        .RequireAuthenticatedUser()
            //        //.RequireAssertion(context => true)
            //        .Build();

            //    #region AUTHORIZATION POLICIES

            //    options.AddPolicy("ModifHoraires", p => p.RequireClaim("Fonction", "Administrateur"));

            //    options.AddPolicy("CreateAnimal", p => p.RequireClaim("Fonction", "Administrateur"));
            //    options.AddPolicy("UpdateAnimal", p => p.RequireClaim("Fonction", "Administrateur"));
            //    options.AddPolicy("DeleteAnimal", p => p.RequireClaim("Fonction", "Administrateur"));

            //    options.AddPolicy("CreateDiet", p => p.RequireClaim("Fonction", "Administrateur"));
            //    options.AddPolicy("UpdateDiet", p => p.RequireClaim("Fonction", "Administrateur"));
            //    options.AddPolicy("DeleteDiet", p => p.RequireClaim("Fonction", "Administrateur"));

            //    options.AddPolicy("CreateHabitat", p => p.RequireClaim("Fonction", "Administrateur"));
            //    options.AddPolicy("UpdateHabitat", p => p.RequireClaim("Fonction", "Administrateur"));
            //    options.AddPolicy("DeleteHabitat", p => p.RequireClaim("Fonction", "Administrateur"));

            //    options.AddPolicy("CreateHealth", p => p.RequireClaim("Fonction", "Administrateur"));
            //    options.AddPolicy("UpdateHealth", p => p.RequireClaim("Fonction", "Administrateur"));
            //    options.AddPolicy("DeleteHealth", p => p.RequireClaim("Fonction", "Administrateur"));

            //    options.AddPolicy("ReadUnfilteredReviews", p => p.RequireClaim("Fonction", "Administrateur", "Employe"));
            //    options.AddPolicy("UpdateReview", p => p.RequireClaim("Fonction", "Administrateur", "Employe"));
            //    options.AddPolicy("DeleteReview", p => p.RequireClaim("Fonction", "Administrateur", "Employe"));

            //    options.AddPolicy("CreateSpecies", p => p.RequireClaim("Fonction", "Administrateur"));
            //    options.AddPolicy("UpdateSpecies", p => p.RequireClaim("Fonction", "Administrateur"));
            //    options.AddPolicy("DeleteSpecies", p => p.RequireClaim("Fonction", "Administrateur"));

            //    options.AddPolicy("CreateVetVisit", p => p.RequireClaim("Fonction", "Veterinaire"));
            //    options.AddPolicy("ReadVetVisit", p => p.RequireClaim("Fonction", "Administrateur", "Veterinaire"));
            //    options.AddPolicy("UpdateVetVisit", p => p.RequireClaim("Fonction", "Veterinaire"));
            //    options.AddPolicy("DeleteVetVisit", p => p.RequireClaim("Fonction", "Veterinaire"));

            //    options.AddPolicy("CreateZooService", p => p.RequireClaim("Fonction", "Administrateur"));
            //    options.AddPolicy("UpdateZooService", p => p.RequireClaim("Fonction", "Administrateur"));
            //    options.AddPolicy("DeleteZooService", p => p.RequireClaim("Fonction", "Administrateur"));

            //    #endregion
            //});


            //A MODIFIER AVANT MISE EN PROD
            builder.Services.AddCors(policy =>
            {
                policy.AddPolicy("CorsPolicy",
                    builder => builder
                        .AllowAnyMethod()
                        .AllowCredentials()
                        .SetIsOriginAllowed((host) => true)
                        .AllowAnyHeader());
            });

           

            var app = builder.Build();

            app.UseCors("CorsPolicy");

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
