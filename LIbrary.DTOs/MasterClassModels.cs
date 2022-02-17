using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LIbrary.DTOs
{
    public partial class MasterClassModel
    {
        public int Id { get; set; }
        public int MasterCourseId { get; set; }
        public string Name { get; set; } = null!;
        public bool? IsActive { get; set; }
    }
    public partial class AddMasterClassModel
    {
        [Required]
        public int MasterCourseId { get; set; }
        [Required]
        public string Name { get; set; } = null!;
    }
    public partial class EditMasterClassModel
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public int MasterCourseId { get; set; }
        [Required]
        public string Name { get; set; } = null!;
    }
}
