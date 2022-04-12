using AutoMapper;
using Banners.DAL.Context;
using Banners.DAL.Repositories;
using Banners.DAL.Repositories.Implementations;
using Banners.Service.Services;
using Banners.Service.Services.Implementations;
using Banners.Service.Services.Interfaces;
using Banners.Shared.Configurations;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

var SpecificOrigins = "_specificOrigins";

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: SpecificOrigins,
                      policy =>
                      {
                          policy.WithOrigins("https://localhost:7169")
                          .AllowAnyMethod()
                          .AllowAnyHeader(); 
                      });
});

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString));

var mapperConfig = new MapperConfiguration(mc =>
{
    mc.AddProfile(new MappingProfile());
});
IMapper mapper = mapperConfig.CreateMapper();
builder.Services.AddSingleton(mapper);

builder.Services.AddScoped(typeof(IEntityRepository<>), typeof(EntityRepository<>));
builder.Services.AddScoped(typeof(IEntityService<>), typeof(EntityService<>));
builder.Services.AddScoped<IBannerStatRepository, BannerStatRepository>();
builder.Services.AddScoped<IBannerStatService, BannerStatService>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(SpecificOrigins);

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();