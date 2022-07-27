var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
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


app.Use(async (context, next) =>
{
    context.Response.Headers.Add(new KeyValuePair<string, Microsoft.Extensions.Primitives.StringValues>("nadam-custom", "Hlika"));
    await next();
});
app.UseStaticFiles();

//app.Run(async context =>
//{
//    await context.Response.WriteAsync("Hello Dear Readers!");
//});

app.UseRouting();
app.Run();
