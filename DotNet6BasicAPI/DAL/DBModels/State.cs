using System;
using System.Collections.Generic;

namespace DotNet6BasicAPI.DAL.DBModels
{
    public partial class State
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
    }
}
