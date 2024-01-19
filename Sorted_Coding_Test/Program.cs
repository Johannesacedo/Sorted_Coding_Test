using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Sorted_Coding_Test.Interface.Services;
using Sorted_Coding_Test.Model;
using Sorted_Coding_Test.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c => 
  {
      c.SwaggerDoc("v1", new OpenApiInfo
      {
          Title = "Rainfall API",
          Version = "1.0",
          Contact = new OpenApiContact
          {
              Name = "Sorted",
              Url = new Uri("https://www.sorted.com"),
          },
          Description = "An API which provides rainfall reading data",
      });
  });


builder.Services.AddScoped<IRainfallService,RainfallService>();
builder.Services.AddHttpClient<IRainfallService, RainfallService>(client =>
{
    client.BaseAddress = new Uri("http://environment.data.gov.uk/flood-monitoring/");
    client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
});
builder.Services.AddLogging(builder =>
{
    builder.AddConsole(); 
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Rainfall API v1");
        c.RoutePrefix = "swagger";
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
