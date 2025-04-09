using Eagle.Application.Interfaces;
using Eagle.Application.Mapper;
using Eagle.Application.Services;
using Eagle.Domain.Middlewares;
using Eagle.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var dbConnectionString = builder.Configuration.GetConnectionString("DBConnectionString");
builder.Services.AddDbContext<EagleContext>(options => options.UseSqlServer(dbConnectionString));

builder.Services.AddSwaggerGen();

builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.Services.AddTransient<IEvent, EventService>();
builder.Services.AddScoped<ITenantProvider, TenantProviderService>();


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseSwagger();
app.UseSwaggerUI();
app.UseMiddleware<TenantMiddleware>();

app.UseHttpsRedirection();

app.UseCors("AllowAll");

app.UseAuthorization();

app.MapControllers();

app.Run();
