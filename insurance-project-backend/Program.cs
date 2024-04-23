using Microsoft.Extensions.DependencyInjection;
using Refit;
using System;
using insurance_project_backend.Services.FMCSA;
using insurance_project_backend.Models.FMCSA;

var builder = WebApplication.CreateBuilder(args);

var usdotConfig = builder.Configuration.GetSection("USDOT").Get<UsdotConfig>();
builder.Services.AddSingleton(usdotConfig);

builder.Services.AddHttpClient<IUsdotFmcsaCarrierService, UsdotFmcsaCarrierService>();


// Add services to the container.
builder.Services.AddControllers();
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
