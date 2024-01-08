namespace ONPA.Identity.Api;

public class UsersSeed(ILogger<UsersSeed> logger, UserManager<ApplicationUser> userManager) : IDbSeeder<ApplicationDbContext>
{
    public async Task SeedAsync(ApplicationDbContext context)
    {
        var root = await userManager.FindByNameAsync("root");

        if (root == null)
        {
            root = new ApplicationUser
            {
                UserName = "root" ,
                Email = "AliceSmith@email.com",
                EmailConfirmed = true,
                CardNumber = "4012888888881881",
           
                Id = Guid.NewGuid().ToString(),
                LastName = "Systemowy",
                Name = "Administrator"
            };

            var result = userManager.CreateAsync(root, "d!Zujocaci1").Result;

            if (!result.Succeeded)
            {
                throw new Exception(result.Errors.First().Description);
            }

            if (logger.IsEnabled(LogLevel.Debug))
            {
                logger.LogDebug("alice created");
            }
        }
        else
        {
            if (logger.IsEnabled(LogLevel.Debug))
            {
                logger.LogDebug("alice already exists");
            }
        }
    }
}