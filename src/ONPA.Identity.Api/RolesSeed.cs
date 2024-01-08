namespace ONPA.Identity.Api;

public class RolesSeed(ILogger<UsersSeed> logger, RoleManager<ApplicationRole> roleManager)
    : IDbSeeder<ApplicationDbContext>
{
    public async Task SeedAsync(ApplicationDbContext context)
    {
        var systemAdmin = await roleManager.FindByNameAsync("SystemAdmin");
        if(systemAdmin == null)
        {
            systemAdmin = new ApplicationRole
            {
                Name = "SystemAdmin",
                DisplayName = "Super Administrator"
            };

            var result = roleManager.CreateAsync(systemAdmin).Result;

            if (!result.Succeeded)
            {
                throw new Exception(result.Errors.First().Description);
            }

            if (logger.IsEnabled(LogLevel.Debug))
            {
                logger.LogDebug("systemAdmin created");
            }
        }
        else
        {
            if (logger.IsEnabled(LogLevel.Debug))
            {
                logger.LogDebug("systemAdmin already exists");
            }
        }
        
        var organizationAdmin = await roleManager.FindByNameAsync("OrganizationAdmin");
        if (organizationAdmin == null)
        {
            organizationAdmin = new ApplicationRole
            {
                Name = "OrganizationAdmin",
                DisplayName = "Administrator organizacji"
            };
            
            var result = roleManager.CreateAsync(organizationAdmin).Result;
            
            if (!result.Succeeded)
            {
                throw new Exception(result.Errors.First().Description);
            }

            if (logger.IsEnabled(LogLevel.Debug))
            {
                logger.LogDebug("organizationAdmin created");
            }
        }
        else
        {
            if (logger.IsEnabled(LogLevel.Debug))
            {
                logger.LogDebug("organizationAdmin already exists");
            }
        }
        
        var member = await roleManager.FindByNameAsync("Member");
        if (member == null)
        {
            member = new ApplicationRole
            {
                Name = "Member",
                DisplayName = "Członek"
            };
            
            var result = roleManager.CreateAsync(member).Result;
            
            if (!result.Succeeded)
            {
                throw new Exception(result.Errors.First().Description);
            }

            if (logger.IsEnabled(LogLevel.Debug))
            {
                logger.LogDebug("member created");
            }
        }
        else
        {
            if (logger.IsEnabled(LogLevel.Debug))
            {
                logger.LogDebug("member already exists");
            }
        }

    }
}