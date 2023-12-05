using System.ComponentModel.DataAnnotations;

namespace ONPA.Identity.Api.Models.AccountViewModels
{
    public record ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; init; }
    }
}
