using System.ComponentModel.DataAnnotations;

namespace ExampleBlazorApp.Client.Models.Account
{
    public class CheckPasswordResetRequest
    {
        [Required]
        public string Email { get; set; }
        
        public Guid Guid { get; set; }
    }
}
