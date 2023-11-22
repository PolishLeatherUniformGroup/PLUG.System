using System.ComponentModel.DataAnnotations;

namespace PLUG.System.Identity.Api.Models.AccountViewModels
{
    public record ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; init; }
    }
}
