namespace Demo.TaskManagement.Data.Entities
{
    public class CheckListStep
    {
        public int Id { get; set; }
        public int TaskId { get; set; }
        public string Title { get; set; }
        public DateTime Deadline { get; set; }
        public bool Finished { get; set; }

        public Task? Task { get; set; }
    }
}
