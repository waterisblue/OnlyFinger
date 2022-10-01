using FurionTest.Utils;

var builder = WebApplication.CreateBuilder(args).Inject();

// Add services to the container.

builder.Services.AddControllers().AddInject();

builder.Services.AddSqlsugarSetup(builder.Configuration);

builder.Services.AddCors(c =>
{
    c.AddPolicy("Cors", policy =>
    {
        policy
        .AllowAnyHeader()
        .AllowAnyOrigin()
        .AllowAnyMethod();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseCors("Cors");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseInject(string.Empty);

app.Run();
