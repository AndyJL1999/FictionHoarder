using AutoMapper;
using FictionAPI.Data;
using FictionAPI.DTOs;
using FictionDataAccessLibrary.Data;
using FictionDataAccessLibrary.DbAccess;
using FictionDataAccessLibrary.DTOs;
using FictionDataAccessLibrary.Models;
using Microsoft.AspNetCore.Identity;
using System.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(options =>
{

});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<ISqlDataAccess, SqlDataAccess>();
builder.Services.AddSingleton<IStoryData, StoryData>();
builder.Services.AddSingleton<IAuthData, AuthData>();

var config = new MapperConfiguration(myConfig =>
{
    myConfig.CreateMap<User, LoginDto>();
    myConfig.CreateMap<User, RegisterDto>();
});
var mapper = config.CreateMapper();

builder.Services.AddSingleton(mapper);
builder.Services.AddScoped<IAuthRepository, AuthRepository>();
builder.Services.AddScoped<IStoryRepository, StoryRepository>();

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
