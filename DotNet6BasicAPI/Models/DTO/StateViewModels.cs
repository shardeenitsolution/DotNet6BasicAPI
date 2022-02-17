using System.ComponentModel.DataAnnotations;

namespace DotNet6BasicAPI.Models.DTO
{
    public class StateViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
    }

    public class AddStateViewModel
    {
        [Required]
        public string Name { get; set; } = null!;
    }

    public class EditStateViewModel
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = null!;
    }
}
