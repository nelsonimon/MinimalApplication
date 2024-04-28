using Microsoft.AspNetCore.Hosting;
using Microsoft.OpenApi.Models;
using MinimalApplication.Application.Api;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(
     c =>
     {
         c.EnableAnnotations();
         c.SwaggerDoc("v1", new OpenApiInfo
         {
             Title = "Minimal Application Web API",             
             Description = "Modelo base de api.<br/>" +
             "Todos os retornos serão entregues no padrão:<br/>" +
             "<b>{<br/>  \"statusCode\": (int),<br/>  \"success\": (bool),<br/>  \"data\": (retorno do endpoint),<br/>  \"errors\": [(string)]<br/>}<b/>",
             //Contact = new OpenApiContact() { Name = "Nelson Imon", Email = "nelsonimon@gmail.com" },
             
         });
     });


//set startup
var startup = new Startup(builder.Configuration);
startup.ConfigureService(builder.Services);

var app = builder.Build();
startup.Configure(app, app.Environment);

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
