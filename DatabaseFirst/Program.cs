using DatabaseFirst.DAL;
using DatabaseFirst.Middleware;
using DatabaseFirst.Service;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<S25646Context>(opt =>
{
    opt.UseSqlServer("Data Source=db-mssql;Initial Catalog=s25646;Integrated Security=True;TrustServerCertificate=True");
    opt.LogTo(Console.WriteLine);
});

builder.Services.AddControllers();
builder.Services.AddScoped<IClientsService, ClientsService>();
builder.Services.AddScoped<ITripsService, TripsService>();
builder.Services.AddScoped<ErrorHandlingMiddleware>();


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
