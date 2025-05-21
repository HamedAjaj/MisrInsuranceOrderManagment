using Microsoft.EntityFrameworkCore;
using MisrInsuranceOrderManagment;
using MisrInsuranceOrderManagment.Infrastructure;
using MisrInsuranceOrderManagment.Infrastructure.UnitOfwork;
using MisrInsuranceOrderManagment.Middlewares;
using MisrInsuranceOrderManagment.Repository.GenericRepository;
using MisrInsuranceOrderManagment.Services;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddScoped(typeof(IGenereicRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped(typeof(IUnitOfwork), typeof(UnitOfWork)); 
builder.Services.AddScoped(typeof(IOrderService),typeof(OrderService));
builder.Services.AddApplicationServices();

builder.Services.AddControllers();


builder.Services.AddDbContext<OrderManagementContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

// Apply migrations automatically
    using (var scope = app.Services.CreateScope())
    {
        var dbContext = scope.ServiceProvider.GetRequiredService<OrderManagementContext>();
        dbContext.Database.Migrate();
    }

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseMiddleware<ExceptionMiddleware>();
app.MapControllers();

app.Run();
