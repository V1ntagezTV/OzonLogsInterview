using Microsoft.EntityFrameworkCore;

namespace WebApiEntity
{
    public class LogContext : DbContext
    {
        public static readonly string ConnString = "Server=localhost\\SQLEXPRESS;Database=LogsDatabase;Trusted_Connection=True;";
        
        public DbSet<LogModel> Logs { get; set; }

        public LogContext(DbContextOptions<LogContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(LogContext.ConnString);
        }
    }
}