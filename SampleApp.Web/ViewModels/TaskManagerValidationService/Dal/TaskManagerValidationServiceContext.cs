using Microsoft.EntityFrameworkCore;
using TaskManagerValidationService.Core.Model;

namespace TaskManagerValidationService.Dal
{
    public class TaskManagerValidationServiceContext : DbContext
    {
        public TaskManagerValidationServiceContext(
            DbContextOptions<TaskManagerValidationServiceContext> options) 
            : base(options) { }

        public virtual DbSet<Assignment> Assignment { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new AssignmentConfiguration());

        }
    }
}
