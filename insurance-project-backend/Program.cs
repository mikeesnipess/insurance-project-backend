using DinkToPdf.Contracts;
using DinkToPdf;
using insurance_project_backend.Models.FMCSA;
using insurance_project_backend.Services.Company;
using insurance_project_backend.Services.DocuSign;
using insurance_project_backend.Services.Drivers;
using insurance_project_backend.Services.FMCSA;
using Microsoft.OpenApi.Models;
using PdfSharp.Charting;
using insurance_project_backend.Templates;

var builder = WebApplication.CreateBuilder(args);

var usdotConfig = builder.Configuration.GetSection("USDOT").Get<UsdotConfig>();

builder.Services.AddSingleton(usdotConfig);
builder.Services.AddHttpClient<IUsdotFmcsaCarrierService, UsdotFmcsaCarrierService>();
builder.Services.AddScoped<IDriverDetailsService,DriverDetailsService>();
builder.Services.AddScoped<ICompanyDetailsService,CompanyDetailsService>();
builder.Services.AddSingleton<DocuSignClientService>();
builder.Services.AddSingleton(typeof(IConverter), new SynchronizedConverter(new PdfTools()));
builder.Services.AddTransient<PdfService>();
builder.Services.AddTransient<CreateOccupationInsuranceRecipient>();
builder.Services.AddTransient<CreateOccupationInsuranceDocument>();

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.PropertyNamingPolicy = null;
});

// Add AutoMapper
builder.Services.AddAutoMapper(typeof(MappingProfile));

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme.",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement()
    {
      {
        new OpenApiSecurityScheme
        {
          Reference = new OpenApiReference
          {
            Type = ReferenceType.SecurityScheme,
            Id = "Bearer"
          },
          Scheme = "oauth2",
          Name = "Bearer",
          In = ParameterLocation.Header,
        },
        new List<string>()
      }
    });
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
        builder => builder.WithOrigins("http://localhost:4200")
                          .AllowAnyHeader()
                          .AllowAnyMethod());
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Use CORS before any other middlewares
app.UseCors("AllowSpecificOrigin");

app.UseAuthorization();

app.MapControllers();

app.Run();
