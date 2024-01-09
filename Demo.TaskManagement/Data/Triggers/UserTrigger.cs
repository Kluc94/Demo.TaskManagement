using Demo.TaskManagement.Data.Entities;
using EntityFrameworkCore.Triggered;

namespace Demo.TaskManagement.Data.Triggers
{
    public class UserTrigger : IBeforeSaveTrigger<User>
    {
        public System.Threading.Tasks.Task BeforeSave(ITriggerContext<User> context, CancellationToken cancellationToken)
        {
            if (context.ChangeType == ChangeType.Added || context.ChangeType == ChangeType.Modified)
            {
                var user = context.Entity;

                string fullName = string.Empty;

                if (string.IsNullOrEmpty(user.DegreeBefore) == false)
                {
                    fullName = user.DegreeBefore.Trim();
                }

                if (string.IsNullOrEmpty(user.FirstName) == false)
                {
                    fullName += " " + user.FirstName.Trim();
                }

                if (string.IsNullOrEmpty(user.LastName) == false)
                {
                    fullName += " " + user.LastName.Trim();
                }

                if (string.IsNullOrEmpty(user.DegreeAfter) == false)
                {
                    fullName += ", " + user.DegreeAfter.Trim();
                }


                context.Entity.FullName = fullName;
            }

            return System.Threading.Tasks.Task.CompletedTask;
        }
    }
}
