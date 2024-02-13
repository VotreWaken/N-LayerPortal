using Microsoft.Extensions.Configuration;
using MusicPortal.BLL.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
string? connection = configuration.GetConnectionString("DefaultConnection");
builder.Services.AddMusicPortalContext(connection);


builder.Services.AddUnitOfWorkService();
builder.Services.AddBLLServices();

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
