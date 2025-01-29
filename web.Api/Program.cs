using Core.Application.Interfaces;
using Core.Application.Mapper;
using Core.Application.Services;
using Core.Domain.RepositoryInterfaces;
using Infrastructure.Data;
using Infrastructure.Extentions;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using System;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<ApplicationDbContext>(options =>


    options.UseSqlServer(builder.Configuration.GetConnectionString("WebApplication1Context"))
);
// Register services to the DI container
builder.Services.AddScoped<IStoryRepository, StoryRepository>();
builder.Services.AddScoped<IStoryService, StoryService>();
builder.Services.AddScoped<IFileStorageService, FileStorageService>();
builder.Services.AddScoped<IStoryCommentRepository, StoryCommentRepository>();
builder.Services.AddScoped<IStoryCommentService, StoryCommentService>();
builder.Services.AddScoped<IStoryLikeRepository, StoryLikeRepository>();
builder.Services.AddScoped<IStoryLikeService, StoryLikeService>();

builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.Services.AddInfrastructureIdentityServices();
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
