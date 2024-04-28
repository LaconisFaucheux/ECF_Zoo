
using API_Arcadia.Interfaces;
using API_Arcadia.Models.Data;
using API_Arcadia.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Serilog;

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

            //Business Services
            builder.Services.AddScoped<IAnimalService, AnimalService>();
            builder.Services.AddScoped<IHabitatService, HabitatService>();
            builder.Services.AddScoped<ISpeciesService, SpeciesService>();
            builder.Services.AddScoped<IVetVisitService, VetVisitService>();

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
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                 {
                     //url d'accès au serveur d'identités
                     options.Authority = builder.Configuration["ArcadiaAuthServerUrl"];
                     options.TokenValidationParameters.ValidateAudience = false;

                     //Tolérance sur la validité du jeton (à modifier/supprimer (par défaut c'est 5 min) à la mise en prod pour que l'API accepte des tokens dépassés de quelques min)
                     options.TokenValidationParameters.ClockSkew = TimeSpan.Zero;
                 });

            //ajoute le service d'autorisation
            builder.Services.AddAuthorization(options => 
            {
                //Spécifie que TOUT utilsateur doitêtre authentifié par défaut
                options.FallbackPolicy = new AuthorizationPolicyBuilder()
                    .RequireAuthenticatedUser()
                    .Build();
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
