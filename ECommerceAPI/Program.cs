using InfrastructureLibrary.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using DomainLibrary.IRepositories;
using InfrastructureLibrary.Data;
using AppLibrary.IService;
using AppLibrary.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
#region DI-s

builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<ICategoryService, CategoryService>();

builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IProductService, ProductService>();

#endregion

#region Context
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"),
        b => b.MigrationsAssembly("InfrastructureLibrary")));
#endregion

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddScoped<AppDbContextInitializer>();

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

// Initialize the database (running AppDbContextInitializer)
using (var scope = app.Services.CreateScope())
{
    var dbInitializer = scope.ServiceProvider.GetRequiredService<AppDbContextInitializer>();
    await dbInitializer.InitializeAsync();
}

app.Run();