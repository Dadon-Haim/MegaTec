using System.Data.Entity;

namespace HaimDadon.PeopleManager.web.Models
{
    public class DatabaseContext : DbContext
    {
        public DbSet<Person> Person { get; set; }
    }
}