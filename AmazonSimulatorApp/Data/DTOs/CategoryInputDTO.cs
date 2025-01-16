using System.ComponentModel.DataAnnotations;

namespace AmazonSimulatorApp.Data.DTOs
{
    public class CategoryInputDTO
    {
        [Required(ErrorMessage = "Category name is required")]
        public string CategoryName { get; set; }
    }
}
