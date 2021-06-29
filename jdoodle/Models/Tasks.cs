using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace jdoodle.Models
{
    public partial class Tasks
    {
        public Tasks()
        {
            UserTask = new HashSet<UserTask>();
        }

        public string Id { get; set; }
        public string TaskName { get; set; }
        public string Description { get; set; }
        public string InputParams { get; set; }
        public string OutputParams { get; set; }

        public virtual ICollection<UserTask> UserTask { get; set; }
    }
}