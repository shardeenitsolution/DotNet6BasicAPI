using System;
using System.Collections.Generic;

namespace DotNet6BasicAPI.DAL.DBModels
{
    public partial class AcademicYear
    {
        public int Id { get; set; }
        public string Year { get; set; } = null!;
    }
}
