using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using RecipeApp.Infrastructure.Data;
using RecipeApp.Api.Services;

var builder = WebApplication.CreateBuilder(args);

// ======================
// DATABASE
// ======================
builder.Services.AddDbContext<RecipeDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")
    )
);

// ======================
// SERVICES
// ======================
builder.Services.AddScoped<RecipeService>();

// ======================
// CONTROLLERS + JSON
// ======================
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler =
            ReferenceHandler.IgnoreCycles;
    });

// ======================
// CORS
// ======================
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

// ======================
// SWAGGER
// ======================
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// ======================
// PIPELINE
// ======================
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("AllowAll");

app.UseAuthorization();

app.MapControllers();

app.Run();