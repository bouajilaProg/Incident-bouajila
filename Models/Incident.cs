using System.ComponentModel.DataAnnotations;

namespace tp1.Models
{
    public class Incident
    {
        public int Id { get; set; }

        [Required]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "Title must be between 3 and 30 characters.")]
        public string Title { get; set; } = string.Empty;

        [Required]
        [StringLength(200, MinimumLength = 10, ErrorMessage = "Description must be between 10 and 200 characters.")]
        public string Description { get; set; } = string.Empty;

        [Required]
        public string Severity { get; set; } = string.Empty;

        [Required]
        public string Status { get; set; } = string.Empty;

        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
