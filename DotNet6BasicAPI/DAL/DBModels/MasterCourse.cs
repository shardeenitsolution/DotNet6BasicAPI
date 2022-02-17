using System;
using System.Collections.Generic;

namespace DotNet6BasicAPI.DAL.DBModels
{
    public partial class MasterCourse
    {
        public MasterCourse()
        {
            MasterClasses = new HashSet<MasterClass>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Code { get; set; } = null!;
        public int SemesterNumber { get; set; }
        public DateTime LaunchDate { get; set; }
        public bool? IsActive { get; set; }
        public DateTime ModifiedOn { get; set; }
        public string ModifiedBy { get; set; } = null!;

        public virtual AspNetUser ModifiedByNavigation { get; set; } = null!;
        public virtual ICollection<MasterClass> MasterClasses { get; set; }
    }
}
