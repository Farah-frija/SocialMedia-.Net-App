
using Core.Application.Extentions;
using Core.Application.Mapper.FollowMapper;
using Core.Application.Mapper.UserMapper;
using Infrastructure.Extentions;
using Infrastructure.Identity.Configurations;
using Infrastructure.Identity.Mappings;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;




var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddAppDbContext(builder.Configuration);

builder.Services.AddInfrastructureIdentityServices();
builder.Services.AddAutoMapper(
    typeof(IdentityMapping).Assembly,
    typeof(UserProfile).Assembly,
    typeof(FollowProfile).Assembly
);
var jwtSection = builder.Configuration.GetSection("JWTBearerTokenSettings");
builder.Services.Configure<JWTBearerTokenSetting>(jwtSection); // Ajoute les paramètres JWT au conteneur d'injection de dépendances

var jwtBearerTokenSettings = jwtSection.Get<JWTBearerTokenSetting>(); // Récupère les paramètres JWT sous forme d'objet
var key = Encoding.ASCII.GetBytes(jwtBearerTokenSettings.SecretKey); // Crée une clé symétrique en bytes pour la signature du JWT

// Étape 2 : Ajouter l'authentification via JWT
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme; // Définit le schéma par défaut d'authentification comme étant celui de JWT
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme; // Définit le schéma pour les challenges d'authentification comme étant celui de JWT
})
.AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false; // Indique que l'application peut accepter des tokens même s'ils sont envoyés via HTTP (modifie à true pour la production, toujours utiliser HTTPS)
    options.SaveToken = true; // Sauvegarde le token dans la requête après son authentification

    // Paramètres de validation du token JWT
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = true, // Valide l'émetteur du token
        ValidIssuer = jwtBearerTokenSettings.Issuer, // Spécifie l'émetteur attendu du token
        ValidateAudience = true, // Valide le destinataire du token (audience)
        ValidAudience = jwtBearerTokenSettings.Audience, // Spécifie l'audience attendue
        ValidateIssuerSigningKey = true, // Valide la clé de signature de l'émetteur
        IssuerSigningKey = new SymmetricSecurityKey(key), // Utilise la clé symétrique pour valider la signature du token
        ValidateLifetime = true, // Valide la durée de vie du token (expiration)
        ClockSkew = TimeSpan.Zero // Spécifie la tolérance de la différence d'heure entre l'horloge du serveur et celle du client
    };
});
builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureRepoInjections();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
