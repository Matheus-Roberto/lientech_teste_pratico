using Microsoft.EntityFrameworkCore;
using System;
using test_lientech.Data;
using test_lientech.Service;
using test_lientech.Tests;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddTransient<IMovieRepository, MovieRepository>();
builder.Services.AddTransient<IRoomRepository, RoomRepository>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(c =>
{
    c.AddPolicy("AllowOrigin", options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
});

var connectionString = builder.Configuration.GetConnectionString("Mysql");
builder.Services.AddDbContext<ApiDbContext>(options => options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));



var app = builder.Build();



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

app.UseHttpsRedirection();

//app.MapPost("/movie", MovieEndPoints.AddMovie);
//app.MapPost("/room", RoomEndPoints.AddRoom);

app.UseAuthorization();

app.MapControllers();

app.Run();
