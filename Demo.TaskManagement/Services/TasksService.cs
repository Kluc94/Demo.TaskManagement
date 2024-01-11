using Demo.TaskManagement.Data;
using Microsoft.EntityFrameworkCore;

namespace Demo.TaskManagement.Services
{
    public class TaskService : ITaskService
    {
        public TaskService(ApplicationDbContext applicationDbContext)
        {
            ApplicationDbContext = applicationDbContext;
        }

        public ApplicationDbContext ApplicationDbContext { get; }

        public IQueryable<Data.Entities.Task> GetTasksByView(string viewName, int accountId, int? userId)
        {
            IQueryable<Data.Entities.Task> tasks = null;

            switch (viewName)
            {
                case "all":
                    tasks = ApplicationDbContext.Tasks
                        .Include(t => t.Account)
                        .Include(t => t.AssignedTo);
                    break;

                case "byCreator":
                    if (userId != null)
                    {
                        tasks = ApplicationDbContext.Tasks
                            .Include(t => t.Account)
                            .Include(t => t.AssignedTo)
                            .Where(t => t.CreatedById == userId);
                    }
                    else
                    {
                        throw new Exception("Nebylo zadáno createdById");
                    }
                    break;

                case "byAssignedTo":
                    if (userId != null)
                    {
                        tasks = ApplicationDbContext.Tasks
                            .Include(t => t.Account)
                            .Include(t => t.AssignedTo)
                            .Where(t => t.AssignedToId == userId);
                    }
                    else
                    {
                        throw new Exception("Nebylo zadáno createdById");
                    }
                    break;

                case "open":
                    tasks = ApplicationDbContext.Tasks
                        .Include(t => t.Account)
                        .Include(t => t.AssignedTo)
                        .Where(t => t.State != Data.Enums.TaskState.Finished);
                    break;

                case "openAfterDeadLine":
                    tasks = ApplicationDbContext.Tasks
                        .Include(t => t.Account)
                        .Include(t => t.AssignedTo)
                        .Where(t => t.State != Data.Enums.TaskState.Finished
                                 && t.Deadline < DateTime.Now);
                    break;

                case "openChecklistAfterDeadline":
                    tasks = ApplicationDbContext.Tasks
                        .Include(t => t.Account)
                        .Include(t => t.AssignedTo)
                        .Where(t => t.CheckListSteps.Any(s => s.Finished != true && s.Deadline < DateTime.Now)
                                    && t.Deadline < DateTime.Now
                               );
                    break;
            }

            if (accountId != null && accountId != 0)
            {
                tasks = tasks?.Where(t => t.AccountId == accountId);
            }

            return tasks;
        }
    }
}
