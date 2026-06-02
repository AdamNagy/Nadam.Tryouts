using System.Collections.Generic;

using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Mvc;

using Rag.Web;

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

var basePath = "H:\\Documents\\MIV\\app_data\\Adam_Nagy\\Babes\\images\\OnlyAllSites\\Vendula_Bednarova";
var connectionString = "Host=localhost;Port=27018;Database=postgres;Username=postgres;Password=123qweASD";// "postgresql://postgres:123qweASD@localhost:27018/public";
var tableName = "faceembeddings";

//app.MapGet("/vectorize", () =>
//{
//    var portraits = Directory.GetFiles("H:\\Documents\\MIV\\app_data\\Adam_Nagy\\Babes\\images\\_website-elements\\portraits");

//    var recognizer = new FaceRecognition.FaceAiSharpEmbedder();
//    var vectorStore = new VectorDb.Postgres.VectorStoreService(connectionString, tableName);

//    foreach (var file in portraits)
//    {
//        var image = File.ReadAllBytes(file);
//        var fileName = Path.GetFileName(file);
//        var faces = recognizer.GenerateEmbeddings(image);

//        foreach (var item in faces)
//        {
//            vectorStore.StoreVectorAsync(Guid.NewGuid(), item.Embedding, fileName).Wait();
//        }
//    }

//    return Results.Ok("Done");
//});

app.MapGet("/ragging/{fileName}", async ([FromRoute] string fileName) =>
{
    var recognizer = new FaceRecognition.FaceAiSharpEmbedder();
    var image = File.ReadAllBytes($"{basePath}\\{fileName}");
    var faces = recognizer.GenerateEmbeddings(image);

    var vectorStore = new VectorDb.Postgres.VectorStoreService(connectionString, tableName);

    var results = new List<SearchResult>();
    foreach (var item in faces)
    {
        // vectorStore.StoreVectorAsync(Guid.NewGuid(), item.Embedding, fileName).Wait();
        var result = await vectorStore.Search(item.Embedding, 5);
        results.Add(new SearchResult(item.X, item.Y, item.Width, item.Height, result.First()));
    }

    return results;
});

app.MapGet("/search/{fileName}", async ([FromRoute] string fileName) =>
{
    var image = File.ReadAllBytes($"{basePath}\\{fileName}");
    var vectorStore = new VectorDb.Postgres.VectorStoreService(connectionString, tableName);

    var recognizer = new FaceRecognition.FaceAiSharpEmbedder();
    var faces = recognizer.GenerateEmbeddings(image).First();

    var result = await vectorStore.Search(faces.Embedding, 5);

    return result;
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

