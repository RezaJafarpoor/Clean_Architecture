using Restaurants.API.Middlewares;
using Restaurants.Application.Extension;
using Restaurants.Infrastructure.Extensions;
using Restaurants.Infrastructure.Seeds;
using Serilog;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddSwaggerGen();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplication();
builder.Host.UseSerilog((context, configuration) =>
{
    configuration.ReadFrom.Configuration(context.Configuration);
});

builder.Services.AddScoped<ErrorHandlingMiddleware>();
builder.Services.AddScoped<RequestTimeMiddleware>();


var app = builder.Build();
var scoped = app.Services.CreateScope();
var seeder = scoped.ServiceProvider.GetRequiredService<IRestaurantSeeder>();
await seeder.Seed();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<RequestTimeMiddleware>();
app.UseMiddleware<ErrorHandlingMiddleware>();
app.UseSerilogRequestLogging();


app.UseHttpsRedirection();

app.MapControllers();


app.Run();


