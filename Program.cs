using food_order_dotnet.Data;
using food_order_dotnet.DTO.Response;
using food_order_dotnet.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");
builder.Services.AddControllers(options =>
{
    options.Filters.Add(typeof(ValidationResponse));
});
builder.Services.AddDbContext<AppDbContext>(options => 
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<UserService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();

app.Run();
