using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace MelbourneData.Areas.MoulaCodeChalleng.Models
{
    public interface IDbContext : IDisposable
    {
        DbSet<Customer> Customers { get; set; }
        IDbSet<T> Set<T>() where T : class;
        DbEntityEntry<T> Entry<T>(T entity) where T : class;
        int SaveChanges();
    }
}
