using Eagle.Application.Interfaces;
using Eagle.Application.Mapper;
using Eagle.Application.Services;
using Eagle.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

var dbConnectionString = builder.Configuration.GetConnectionString("DBConnectionString");
builder.Services.AddDbContext<EagleContext>(options => options.UseSqlServer(dbConnectionString)); builder.Services.AddSwaggerGen();

builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.Services.AddTransient<IEvent, EventService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
