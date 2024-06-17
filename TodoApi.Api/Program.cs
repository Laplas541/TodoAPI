using Microsoft.EntityFrameworkCore;
using TodoApi.Infrastructure.Context;
using TodoApi.Infrastructure;
using TodoApi.Application;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddConveyServices();
builder.Services.AddDatabaseContext(builder.Configuration);
builder.Services.AddValidators();
builder.Services.AddAutoMapperProfiles();

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
