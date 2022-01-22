using System.IO;
using System.Text.Json.Nodes;


var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      builder =>
                      {
                          builder.WithOrigins("https://localhost:4200/",
                                              "http://localhost:4200/")
                                              .WithMethods("PUT", "DELETE", "GET");
                      });
});



var app = builder.Build();


app.UseCors();

var jsonString = File.ReadAllText("products.json");
var allProducts = JsonNode.Parse(jsonString);

app.UseRouting();
app.UseCors(options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());  
app.UseCors(builder => builder.WithOrigins("http://localhost:4200")
                              .AllowAnyMethod()
                              .AllowAnyHeader());
app.UseCors("AllowAll");

app.MapGet("/", () => allProducts["Products"]); 
app.MapGet("/product/0", () => allProducts["Products"][0]);
app.MapGet("/product/1", () => allProducts["Products"][1]);
app.MapGet("/product/2", () => allProducts["Products"][2]);
app.MapGet("/product/3", () => allProducts["Products"][3]);
app.MapGet("/product/4", () => allProducts["Products"][4]);
app.MapGet("/product/5", () => allProducts["Products"][5]);  


app.Run();
