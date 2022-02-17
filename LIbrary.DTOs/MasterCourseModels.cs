using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LIbrary.DTOs
{
    public partial class MasterCourseModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Code { get; set; } = null!;
        public int SemesterNumber { get; set; }
        public DateTime LaunchDate { get; set; }
        public bool? IsActive { get; set; }
    }
    public partial class AddMasterCourseModel
    {
        [Required]
        public string Name { get; set; } = null!;
        [Required]
        public string Code { get; set; } = null!;
        [Required]
        public int SemesterNumber { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime LaunchDate { get; set; }
    }
    public partial class EditMasterCourseModel
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = null!;
        [Required]
        public string Code { get; set; } = null!;
        [Required]
        public int SemesterNumber { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime LaunchDate { get; set; }
    }
}
