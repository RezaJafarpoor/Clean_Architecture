using Restaurants.API.Extensions;
using Restaurants.API.Middlewares;
using Restaurants.Application.Extension;
using Restaurants.Domain.Entities;
using Restaurants.Infrastructure.Extensions;
using Restaurants.Infrastructure.Seeds;
using Serilog;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplication();
builder.AddPresentation();


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
app.MapGroup("api/identity")
    .WithTags("Identity").MapIdentityApi<User>();
app.UseAuthorization();
app.MapControllers();


app.Run();


