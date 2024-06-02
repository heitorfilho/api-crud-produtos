using DevIO.Api.Configuration;
using DevIO.Data.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.Swagger;
using Microsoft.OpenApi.Models;
using DevIO.Api.Extensions;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

builder.Services.AddDbContext<MyDbContext>(options =>
{
    options.UseSqlServer(connectionString);
});

builder.Services.AddAutoMapper(typeof(Program));

builder.Services.ResolveDependencies();
builder.Services.WebApiConfig();

// LoggerConfig.AddLoggingConfig(builder.Services, builder.Configuration);

IdentityConfig.AddIdentityConfiguration(builder.Services, builder.Configuration);

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "My Api",
        Version = "v1"
    });
    //var xmlFile = "DocumentingSwagger.xml";
    //var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    //c.IncludeXmlComments(xmlPath);
});

// default -> se responder
// verifica o db
//builder.Services.AddHealthChecks()
//    .AddSqlServer(connectionString);

//builder.Services.AddHealthChecksUI(options =>
//{
//    options.UIPath = "/api/hc-ui";
//});

var app = builder.Build();



// antes do MVC
app.UseAuthentication();
app.UseAuthorization();

// app.UseElmahIo();

app.UseMiddleware<ExceptionMiddleware>();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseStaticFiles();
app.MapControllers();

app.UseSwaggerUI(opt =>
{
    opt.SwaggerEndpoint("/swagger/v1/swagger.json", "TodoAPI V1");
});

// app.UseHealthChecks("/hc");
app.UseCors("Development");
app.UseSwagger();

app.Run();
