using System.Data.Entity;
using wslyvh.Core.Sample.Data.Model;

namespace wslyvh.Core.Sample.Data
{
    public class MessageContext : DbContext
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<Message> Messages { get; set; }
    }
}
