using Restaurants.Application.Extension;
using Restaurants.Infrastructure.Extensions;
using Restaurants.Infrastructure.Seeds;
using Serilog;
using Serilog.Events;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplication();
builder.Host.UseSerilog((context, configuration) =>
{
    configuration.ReadFrom.Configuration(context.Configuration);
});

var app = builder.Build();
app.UseSerilogRequestLogging();
var scoped = app.Services.CreateScope();
var seeder = scoped.ServiceProvider.GetRequiredService<IRestaurantSeeder>();
await seeder.Seed();

app.UseHttpsRedirection();

app.MapControllers();


app.Run();


