using Demo.TaskManagement.Data.Enums;

namespace Demo.TaskManagement.Data.Entities;

public class Task
{
    public Task()
    {
        this.Messages = new HashSet<Message>();
        //this.Attachment = new HashSet<Attachment>();
    }

    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public int CreatedById { get; set; }
    public int AssignedToId { get; set; }
    public DateTime CreatedOn { get; set; }
    public TaskPriority Priority { get; set; }
    public TaskState State { get; set; }
    public int AccountId { get; set; }
    public DateTime Deadline { get; set; }
    public DateTime? Solved { get; set; }
    public int? CheckListId { get; set; }

    public User? CreatedBy { get; set; }
    public User? AssignedTo { get; set; }
    public Account? Account { get; set; }
    public ICollection<Message> Messages { get; set; }
    //public ICollection<Attachment> Attachments { get; set; }
}
