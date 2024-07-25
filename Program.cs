using Microsoft.EntityFrameworkCore;
using TestDtoInApi.DataContext;
using TestDtoInApi.Configurations.MapperConfig;
using TestDtoInApi.Repository.BaseRepository.Class;
using TestDtoInApi.Repository.BaseRepository.Interface;
using TestDtoInApi.Repository.CategoryRepository.Class;
using TestDtoInApi.Repository.CategoryRepository.Interface;
using TestDtoInApi.Repository.ProductRepository.Class;
using TestDtoInApi.Repository.ProductRepository.Interface;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<MyDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("MyConnectionString"));
});

// Register Repositories in Middleware
builder.Services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();

// Register Mapping in Middleware
builder.Services.AddAutoMapper(typeof(AutoMapperConfig));

builder.Services.AddControllers();
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
