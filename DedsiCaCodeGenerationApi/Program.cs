using DedsiCaCodeGenerationApi.Apis;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference(options =>
    {
        options.ForceThemeMode = ThemeMode.Light;
    });
}

app.MapCodeGenerationApi();

app.Run();