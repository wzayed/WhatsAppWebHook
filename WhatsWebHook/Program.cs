using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore;
using System;
using WhatsWebHook.Data;
using WhatsWebHook.Functions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var serverVersion = new MySqlServerVersion(new Version(5, 0, 47));
/*builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(
    builder.Configuration.GetConnectionString("DefaultConnection"))); */

builder.Services.AddDbContext<AppDbContext>(options => options.UseMySql(
    builder.Configuration.GetConnectionString("MySqlConn"),serverVersion));

builder.Services.AddSingleton<RestSharp_fns>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

//app.UseRouting();


app.UseAuthorization();

app.MapControllers();

app.MapFallbackToFile("index.html"); 

app.Run();
