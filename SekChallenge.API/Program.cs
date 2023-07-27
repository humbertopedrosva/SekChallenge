using Microsoft.EntityFrameworkCore;
using SekChallenge.API;
using SekChallenge.API.Infra;
using SekChallenge.API.Infra.Repositories;
using SekChallenge.API.Infra.Repositories.Interfaces;
using SekChallenge.API.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHttpClient();
builder.Services.AddDbContext<SekContext>(opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("SekContextConnection")));

builder.Services.AddScoped<IFileService, FileService>();
builder.Services.AddScoped<IScanRepository, ScanRepository>();
builder.Services.AddScoped<ContextMiddleware>();

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

app.UseMiddleware<ContextMiddleware>();

app.Run();
