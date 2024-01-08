namespace ONPA.Identity.Api.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        [Required]
        public string CardNumber { get; set; }
      
        [Required]
        public string Name { get; set; }
        [Required]
        public string LastName { get; set; }
    }
    
    public class ApplicationRole: IdentityRole
    {
        public string DisplayName { get; set; }
    }
}
