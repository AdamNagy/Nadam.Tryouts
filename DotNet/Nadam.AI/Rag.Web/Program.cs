using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

#region CORS
var corsBuilder = new CorsPolicyBuilder();
corsBuilder.WithOrigins(new[] {
    "*" });
corsBuilder.AllowAnyHeader();
corsBuilder.AllowCredentials();
corsBuilder.AllowAnyMethod();

builder.Services.AddCors(options =>
{
    options.AddPolicy("SiteCorsPolicy", corsBuilder.Build());
});
#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

var basePath = "H:\\Documents\\MIV\\app_data\\Adam_Nagy\\Babes\\multi-lady";

app.MapGet("/ragging/{fileName}", ([FromRoute] string fileName) =>
{
    var recognizer = new FaceRecognition.FaceRecognizer();
    var image = File.ReadAllBytes($"{basePath}\\{fileName}");
    var faces = recognizer.RecognizeFace(image);
    return faces;
});

app.MapGet("/image/{fileName}", ([FromRoute] string fileName) =>
{
    var image = File.ReadAllBytes($"{basePath}\\{fileName}");
    return Results.File(image, contentType: "image/jpeg");
});

app.MapGet("/index", () =>
{
    return Results.File("G:\\git\\Nadam.Tryouts\\DotNet\\Nadam.AI\\FE\\index.html", contentType: "text/html");
});

app.MapGet("/items", () =>
{
    var images = Directory.GetFiles(basePath).Select(f => 
    
        Path.GetFileName(f)
    );
    return images;
});

app.Run();

