using System.ComponentModel.DataAnnotations;

namespace ExampleBlazorApp.Client.Models.MessageBoard
{
    public class GetMessageRequest
    {
        [Required]
        public int ID { get; set; }
    }
}
