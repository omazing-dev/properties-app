using PropertyApp.Application.Interfaces;
using PropertyApp.Application.Services;
using PropertyApp.Infrastructure.Repositories;
using PropertyApp.Web.Configurations;
using static PropertyApp.Infraestructure.Seed.DataSeeder;

var builder = WebApplication.CreateBuilder(args);



// Add services to the container.

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend",
        policy =>
        {
            policy
                .WithOrigins("http://localhost:5173") 
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
});

builder.Services.AddMongoDb(builder.Configuration);

builder.Services.AddTransient<DatabaseSeeder>();

builder.Services.AddScoped<IPropertyRepository, PropertyRepository>();
builder.Services.AddScoped<IPropertyService, PropertyService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseCors("AllowFrontend");

using (var scope = app.Services.CreateScope())
{
    var seeder = scope.ServiceProvider.GetRequiredService<DatabaseSeeder>();
    seeder.Seed();
}

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
