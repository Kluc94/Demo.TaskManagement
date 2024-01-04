using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Demo.TaskManagement.Data.Entities
{
    public class User : IdentityUser<int>
    {
        public User()
        {
            //this.CreatedTasks = new HashSet<Data.Task>();
            //this.AssignedTasks = new HashSet<Data.Task>();
        }

        public string? DegreeBefore { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? DegreeAfter { get; set; }
        public string? FullName { get; set; }
        public string? Photo { get; set; }
        public int? AccountId { get; set; }

        public Account? Account { get; set; }

        //public ICollection<Data.Task> CreatedTasks { get; set; }
        //public ICollection<Data.Task> AssignedTasks { get; set; }

    }
}
