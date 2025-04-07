using Microsoft.EntityFrameworkCore;

namespace MVCProject.Models
{
    public class DatabaseContext:DbContext
    {
        public DatabaseContext()
        {
        }

        public DatabaseContext(DbContextOptions<DatabaseContext> options)
            : base(options)
        {
        }
        public DbSet<EmployeeModel> Employees { get; set; }
    }
}
