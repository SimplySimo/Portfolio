using System.Data.Entity;

namespace MelbourneData.Areas.MoulaCodeChalleng.Models
{
    public class DatabaseContext : DbContext, IDbContext
    {
        public DatabaseContext()
            : base("name=DatabaseContext")
        {
        }

        public virtual DbSet<Customer> Customers { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }

        public void MarkAsModified(Customer customer)
        {
            Entry(customer).State = EntityState.Modified;
        }

        public new virtual IDbSet<T> Set<T>() where T : class
        {
            return base.Set<T>();
        }
    }
}
