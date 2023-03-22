using RRS.Core;
using RRS.Data;
using RRS.Data.Implementation;
using RRS.Data.Interface;
using RRS.Service;
using RRS.Service.Implementation;
using RRS.Service.Interface;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Register DB Context
builder.Services.AddSingleton<RRSDBContext>();

//Register Service
builder.Services.AddSingleton<IUserService, UserService>();
builder.Services.AddSingleton<IRestaurantsService, RestaurantService>();

//Register Repository
builder.Services.AddSingleton<IRestaurantRepository ,RestaurantRepository>();
builder.Services.AddSingleton<IUserRepository, UserRepository>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
