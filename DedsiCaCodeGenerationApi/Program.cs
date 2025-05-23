using DedsiCaCodeGenerationApi.Apis;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

// ¿ª·Å¿çÓòÇëÇó
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder
            .AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference(options =>
    {
        options.ForceThemeMode = ThemeMode.Light;
    });
}

app.UseCors();
app.MapCodeGenerationApi();

app.Run();