using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LIbrary.DTOs
{
    public partial class StateModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
    }

    public partial class AddStateModel
    {
        [Required]
        public string Name { get; set; } = null!;
    }

    public partial class EditStateModel
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = null!;
    }
}
