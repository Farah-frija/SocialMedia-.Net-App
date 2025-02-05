
using Core.Application.Extentions;
using Core.Application.Interfaces.Posts;
using Core.Application.Interfaces.Stories;
using Core.Application.Interfaces.UserProfile;

using Core.Application.Mapper.ChatRoomMapper;
using Core.Application.Mapper.FollowMapper;
using Core.Application.Mapper.UserMapper;
using Core.Application.Services;
using Core.Application.Services.Posts;
using Core.Application.Services.Stories;
using Core.Application.Services.UserProfile;

using Core.Domain.RepositoryInterfaces.Posts;
using Core.Domain.RepositoryInterfaces.Stories;
using Core.Domain.RepositoryInterfaces.UserProfile;

using Infrastructure.Extentions;
using Infrastructure.Identity.Configurations;
using Infrastructure.Identity.Mappings;
using Infrastructure.Repositories;
using Infrastructure.Repositories.Posts;
using Infrastructure.Repositories.Stories;
using Infrastructure.Repositories.UserProfile;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using Microsoft.AspNetCore.Http;




var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddAppDbContext(builder.Configuration);

builder.Services.AddInfrastructureIdentityServices();
builder.Services.AddAutoMapper(
    typeof(IdentityMapping).Assembly,
    typeof(UserProfile).Assembly,
    typeof(FollowProfile).Assembly,
    typeof(ChatRoomProfile).Assembly
    ,typeof(MessageMappingProfile).Assembly
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
builder.Services.AddAuthorization();


builder.Services.AddScoped<IPostService, PostService>();
builder.Services.AddScoped<IPostRepository, PostRepository>();

builder.Services.AddScoped<ICommentService, CommentService>();
builder.Services.AddScoped<ICommentRepository, CommentRepository>();

builder.Services.AddScoped<IFileStorageService>(provider =>
    new LocalFileStorageService(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot")));

builder.Services.AddScoped<IFileStorageServiceStories, FileStorageService>();


builder.Services.AddScoped<ILikeRepository, LikeRepository>();
builder.Services.AddScoped<ILikeService, LikeService>();

builder.Services.AddScoped<IStoryRepository, StoryRepository>();
builder.Services.AddScoped<IStoryService, StoryService>();

builder.Services.AddScoped<IStoryCommentRepository, StoryCommentRepository>();
builder.Services.AddScoped<IStoryCommentService, StoryCommentService>();

builder.Services.AddScoped<IStoryLikeRepository, StoryLikeRepository>();
builder.Services.AddScoped<IStoryLikeService, StoryLikeService>();


builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddScoped<IProfilePictureStorageService, ProfilePictureStorageService>();

builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureRepoInjections();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "Your API", Version = "v1" });

    // Add security definition for JWT
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme."
    });

    // Add security requirement for JWT
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "Your API v1");

        // Add the "Authorize" button to Swagger UI
        options.ConfigObject.AdditionalItems["oauth2RedirectUrl"] = "/swagger/oauth2-redirect.html";
        options.ConfigObject.AdditionalItems["persistAuthorization"] = "true";
    });
}

app.UseHttpsRedirection();

app.UseAuthentication(); // Enable authentication

app.UseAuthorization();

app.MapControllers();

app.Run();
