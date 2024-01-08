namespace ONPA.Identity.Api;

public class RoleAssignmentSeed(ILogger<UsersSeed> logger, UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager) : IDbSeeder<ApplicationDbContext>
{
    public async Task SeedAsync(ApplicationDbContext context)
    {
        var user = await userManager.FindByNameAsync("root");
        var role = await roleManager.FindByNameAsync("SystemAdmin");
        if (user != null && role != null && !await userManager.IsInRoleAsync(user, role.Name))
        {
            var result = await userManager.AddToRoleAsync(user, role.Name);
            if (!result.Succeeded)
            {
                throw new Exception(result.Errors.First().Description);
            }

            if (logger.IsEnabled(LogLevel.Debug))
            {
                logger.LogDebug("root assigned to SystemAdmin");
            }
        }
        else
        {
            if (logger.IsEnabled(LogLevel.Debug))
            {
                logger.LogDebug("root not exists or already assigned to SystemAdmin");
            }
        }
    }
}