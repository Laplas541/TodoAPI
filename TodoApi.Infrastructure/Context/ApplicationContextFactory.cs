using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;


namespace TodoApi.Infrastructure.Context
{
    public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<TodoContext>
    {
        public TodoContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<TodoContext>();
            optionsBuilder.UseSqlServer("Data Source=LAPTOP-CR8DM0Q5\\SQLEXPRESS;Initial Catalog=Tododb;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");

            return new TodoContext(optionsBuilder.Options);
        }
    }
}
