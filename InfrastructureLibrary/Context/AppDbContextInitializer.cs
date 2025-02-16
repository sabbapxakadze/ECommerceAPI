using InfrastructureLibrary.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;

public class AppDbContextInitializer
{
    private readonly AppDbContext _context;
    private readonly UserManager<IdentityUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly ILogger<AppDbContextInitializer> _logger;

    public AppDbContextInitializer(AppDbContext context, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, ILogger<AppDbContextInitializer> logger)
    {
        _context = context;
        _userManager = userManager;
        _roleManager = roleManager;
        _logger = logger;
    }

    public async Task InitializeAsync()
    {
        // Ensure the database is created
        await _context.Database.MigrateAsync();

        // Seed roles
        await SeedRolesAsync();

        // Seed admin user if needed
        await SeedAdminUserAsync();
    }

    private async Task SeedRolesAsync()
    {
        // Create "Admin" role if it doesn't exist
        if (!await _roleManager.RoleExistsAsync("Admin"))
        {
            await _roleManager.CreateAsync(new IdentityRole("Admin"));
        }

        // Create "User" role if it doesn't exist
        if (!await _roleManager.RoleExistsAsync("User"))
        {
            await _roleManager.CreateAsync(new IdentityRole("User"));
        }
    }

    private async Task SeedAdminUserAsync()
    {
        // Check if admin user already exists
        var adminUser = await _userManager.FindByEmailAsync("admin@gmail.com");

        if (adminUser == null)
        {
            // Create a new admin user
            adminUser = new IdentityUser
            {
                UserName = "admin@gmail.com",
                Email = "admin@gmail.com"
            };

            // Create the user and set password
            var result = await _userManager.CreateAsync(adminUser, "Admin123!");

            if (result.Succeeded)
            {
                // Assign the admin role
                await _userManager.AddToRoleAsync(adminUser, "Admin");
            }
            else
            {
                // Log an error if user creation fails
                _logger.LogError("Failed to create admin user");
            }
        }
    }
}