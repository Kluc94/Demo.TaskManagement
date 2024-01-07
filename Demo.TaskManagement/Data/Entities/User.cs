using Microsoft.AspNetCore.Identity;

namespace Demo.TaskManagement.Data.Entities
{
    public class User : IdentityUser<int>
    {
        public User()
        {
            this.CreatedTasks = new HashSet<Task>();
            this.AssignedTasks = new HashSet<Task>();
            this.Messages = new HashSet<Message>();
        }

        public string? DegreeBefore { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? DegreeAfter { get; set; }
        public string? FullName { get; set; }
        public string? Photo { get; set; }
        public int? AccountId { get; set; }

        public Account? Account { get; set; }

        public ICollection<Task> CreatedTasks { get; set; }
        public ICollection<Task> AssignedTasks { get; set; }
        public ICollection<Message> Messages { get; set; }

    }
}
