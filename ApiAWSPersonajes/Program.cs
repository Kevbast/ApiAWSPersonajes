using ApiAWSPersonajes.Data;
using ApiAWSPersonajes.Repositories;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

//HABILITAMOS LOS CORS
builder.Services.AddCors(p => p.AddPolicy("corsenabled", options =>
{
    options.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();  
}));

// Add services to the container.

builder.Services.AddTransient<RepositoryTelevision>();
string connectionString = builder.Configuration.GetConnectionString("AwsMysql");
builder.Services.AddDbContext<TelevisionContext>(options => options.UseMySQL(connectionString));

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
}
    app.MapOpenApi();

app.MapScalarApiReference();

app.MapGet("/", context =>
{
    context.Response.Redirect("/scalar");
    return Task.CompletedTask;
});

//USAMOS LOS CORS
app.UseCors("corsenabled");


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
