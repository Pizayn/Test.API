using Microsoft.EntityFrameworkCore;
using Vienna.Test.API.Data;
using Vienna.Test.API.Extensions;
using Vienna.Test.API.Repositories;
using Vienna.Test.API.Services;
using Vienna.Test.API.Services.Implementation;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<MonarchContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("MonarchConnectionString")));
builder.Services.AddGitHubService(new Uri(builder.Configuration["GitHubSettings:BaseApiUrl"]));
builder.Services.AddScoped(typeof(IRepository<>), typeof(RepositoryBase<>));
builder.Services.AddScoped<IMonarchRepository, MonarchRepository>();


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

app.Run();
