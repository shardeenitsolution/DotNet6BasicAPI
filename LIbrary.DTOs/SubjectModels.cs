using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LIbrary.DTOs
{
    public partial class SubjectModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Code { get; set; } = null!;
    }

    public partial class AddSubjectModel
    {
        [Required]
        public string Name { get; set; } = null!;
        [Required]
        public string Code { get; set; } = null!;
    }

    public partial class EditSubjectModel
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = null!;
        [Required]
        public string Code { get; set; } = null!;
    }
}
