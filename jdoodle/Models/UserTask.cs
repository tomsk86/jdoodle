// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace jdoodle.Models
{
    public partial class UserTask
    {
        public string Id { get; set; }
        public string TaskId { get; set; }
        public string UserId { get; set; }
        public int Attempt { get; set; }
        public bool Finished { get; set; }

        public virtual Tasks Task { get; set; }
        public virtual AspNetUsers User { get; set; }
    }
}