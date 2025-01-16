using System.ComponentModel.DataAnnotations;

namespace AmazonSimulatorApp.Data.DTOs
{
    public class UserInputDTO
    {
        [Required]
        public string Name { get; set; }

        [Required]
        [RegularExpression(@"^(admin)$", ErrorMessage = "Role must be  'admin' .")]

        public string Role { get; set; }

        [Required]
        public string Phone { get; set; }

    }
}
