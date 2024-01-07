namespace Demo.TaskManagement.Data.Entities;

public class Message
{
    public Message()
    {
        //this.Attachments = new HashSet<Attachment>();
    }

    public int Id { get; set; }
    public string Note { get; set; }
    public int UserId { get; set; }
    public int TaskId { get; set; }
    public DateTime Created { get; set; }

    public User? User { get; set; }
    public Task? Task { get; set; }
    //public ICollection<Attachment> Attachments { get; set; }
}
