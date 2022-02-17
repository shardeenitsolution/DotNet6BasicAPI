using System;
using System.Collections.Generic;

namespace DotNet6BasicAPI.DAL.DBModels
{
    public partial class MasterClass
    {
        public int Id { get; set; }
        public int MasterCourseId { get; set; }
        public string Name { get; set; } = null!;
        public bool? IsActive { get; set; }
        public DateTime ModifiedOn { get; set; }
        public string ModifiedBy { get; set; } = null!;

        public virtual MasterCourse MasterCourse { get; set; } = null!;
        public virtual AspNetUser ModifiedByNavigation { get; set; } = null!;
    }
}
