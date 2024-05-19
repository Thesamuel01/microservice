using ProductAPI.Configs;
using ProductAPI.Data;
using ProductAPI.Data.DataBase;
using ProductAPI.Services.Products;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddSingleton<IAppSettingsHelper, AppSettingsHelper>();
builder.Services.AddSingleton<IConnectionFactory, ProductDatabaseConnectionFactory>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddAutoMapper(typeof(ProductService));

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
