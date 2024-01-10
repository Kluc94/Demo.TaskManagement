namespace Demo.TaskManagement.Services
{
    public interface ITaskService
    {
        public IQueryable<Data.Entities.Task> GetTasksByView(string viewName, int accountId, int? userId);
    }
}
