namespace Demo.TaskManagement.Data.Entities
{
    public class Account
    {
        public Account()
        {
            this.Users = new HashSet<User>();
            this.Tasks = new HashSet<Task>();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string? TAXNumber { get; set; }
        public string? VATNumber { get; set; }

        public ICollection<User> Users { get; set; }
        public ICollection<Task> Tasks { get; set; }
    }
}
