using System.ComponentModel.DataAnnotations;

namespace Quillser.DTOs
{
    public record UpdateItemDTO
    {
        [Required]
        public string Name { get; init; }

        [Required]
        [Range(0, 1000)]
        public decimal Price { get; init; }
    }
}