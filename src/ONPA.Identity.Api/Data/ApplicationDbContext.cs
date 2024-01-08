namespace ONPA.Identity.Api.Data;

/// <remarks>
/// Add migrations using the following command inside the 'Identity.API' project directory:
///
/// dotnet ef migrations add [migration-name]
/// </remarks>
public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public ApplicationDbContext()
    {
        
    }
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.HasDefaultSchema("identity");   
        base.OnModelCreating(builder);
    }
}
