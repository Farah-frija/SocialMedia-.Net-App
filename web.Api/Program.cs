
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
builder.Services.Configure<JWTBearerTokenSetting>(jwtSection); // Ajoute les param�tres JWT au conteneur d'injection de d�pendances

var jwtBearerTokenSettings = jwtSection.Get<JWTBearerTokenSetting>(); // R�cup�re les param�tres JWT sous forme d'objet
var key = Encoding.ASCII.GetBytes(jwtBearerTokenSettings.SecretKey); // Cr�e une cl� sym�trique en bytes pour la signature du JWT

// �tape 2 : Ajouter l'authentification via JWT
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme; // D�finit le sch�ma par d�faut d'authentification comme �tant celui de JWT
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme; // D�finit le sch�ma pour les challenges d'authentification comme �tant celui de JWT
})
.AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false; // Indique que l'application peut accepter des tokens m�me s'ils sont envoy�s via HTTP (modifie � true pour la production, toujours utiliser HTTPS)
    options.SaveToken = true; // Sauvegarde le token dans la requ�te apr�s son authentification

    // Param�tres de validation du token JWT
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = true, // Valide l'�metteur du token
        ValidIssuer = jwtBearerTokenSettings.Issuer, // Sp�cifie l'�metteur attendu du token
        ValidateAudience = true, // Valide le destinataire du token (audience)
        ValidAudience = jwtBearerTokenSettings.Audience, // Sp�cifie l'audience attendue
        ValidateIssuerSigningKey = true, // Valide la cl� de signature de l'�metteur
        IssuerSigningKey = new SymmetricSecurityKey(key), // Utilise la cl� sym�trique pour valider la signature du token
        ValidateLifetime = true, // Valide la dur�e de vie du token (expiration)
        ClockSkew = TimeSpan.Zero // Sp�cifie la tol�rance de la diff�rence d'heure entre l'horloge du serveur et celle du client
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
