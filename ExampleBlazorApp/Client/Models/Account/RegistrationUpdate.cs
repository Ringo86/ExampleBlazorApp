using System.ComponentModel.DataAnnotations;

namespace ExampleBlazorApp.Client.Models.Account
{
    public class RegistrationUpdate
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string OldPassword { get; set; }

        [Required]
        [MinLength(8, ErrorMessage = "The New Password field must be a minimum of 8 characters")]
        public string NewPassword { get; set; }

        [Required]
        [CompareProperty("NewPassword")]
        public string ConfirmNewPassword { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }
    }
}
