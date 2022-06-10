using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using ParkyAPI.Repository;
using POC_DotNET_6;
using POC_DotNET_6.Data;
using POC_DotNET_6.Mapper;
using POC_DotNET_6.Repository;
using POC_DotNET_6.Repository.Contract;
using Swashbuckle.AspNetCore.SwaggerGen;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddApiVersioning(options => 
{
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.DefaultApiVersion = new ApiVersion(1, 0);
    options.ReportApiVersions = true;
});
builder.Services.AddVersionedApiExplorer(options => options.GroupNameFormat = "'v'VVV");
builder.Services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
//builder.Services.AddSwaggerGen(options =>
//{
//    options.SwaggerDoc("ParkyOpenAPISpec", new Microsoft.OpenApi.Models.OpenApiInfo()
//    {
//        Title = "Parky API National Parks",
//        Version = "1",
//    });

//    options.SwaggerDoc("ParkyOpenAPISpecTrails", new Microsoft.OpenApi.Models.OpenApiInfo()
//    {
//        Title = "Parky API Trails",
//        Version = "1",
//    });

//    var xmlCommentFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
//    var xmlComment = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, xmlCommentFile);
//    options.IncludeXmlComments(xmlComment);
//});
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<ApplicationDbContext>(x => x.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddAutoMapper(typeof(ParkyMapper));
builder.Services.AddScoped<INationalParkRepository, NationalParkRepository>();
builder.Services.AddScoped<ITrailRepository, TrailRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

builder.Services.AddCors();

var appSettingsSection = builder.Configuration.GetSection("AppSettings");
builder.Services.Configure<AppSettings>(appSettingsSection);

var appSettings = appSettingsSection.Get<AppSettings>();
var key = Encoding.ASCII.GetBytes(appSettings.Secret);

builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(x => {
    x.RequireHttpsMetadata = false;
    x.SaveToken = true;
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});

var app = builder.Build();
var provider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();
// Configure the HTTP request pipeline.

app.UseHttpsRedirection();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options => {
        foreach (var desc in provider.ApiVersionDescriptions)
            options.SwaggerEndpoint($"/swagger/{desc.GroupName}/swagger.json",
                desc.GroupName.ToUpperInvariant());
        options.RoutePrefix = "";
    });

    //app.UseSwaggerUI(options =>
    //{
    //    options.SwaggerEndpoint("swagger/ParkyOpenAPISpec/swagger.json", "Parky API");
    //    // options.SwaggerEndpoint("swagger/ParkyOpenAPISpecTrails/swagger.json", "Parky API Trails");
    //    options.RoutePrefix = "";

    //});
}

app.UseAuthentication();
app.UseAuthorization();

app.UseCors(x => x
  .AllowAnyOrigin()
  .AllowAnyMethod()
  .AllowAnyHeader());

app.MapControllers();

app.Run();
