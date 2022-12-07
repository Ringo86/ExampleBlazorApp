using System.ComponentModel.DataAnnotations;

namespace ExampleBlazorApp.Client.Models.Account
{
    public class RegisterNewAccount
    {
        [Required]
        public string Email { get; set; }

        [Required]
        [MinLength(8, ErrorMessage = "The Password field must be a minimum of 8 characters")]
        public string Password { get; set; }

        [Required]
        [CompareProperty("Password")]
        public string ConfirmPassword { get; set; }

        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
    }
}
