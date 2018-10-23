using System.Collections.Generic;
using System.Linq;
using MelbourneData.Areas.MoulaCodeChalleng.Models;

namespace MelbourneData.Areas.MoulaCodeChalleng.Repository
{
    public class CustomerRepository : Repository<Customer>
    {
        public CustomerRepository(IDbContext dataContext)
            : base(dataContext)
        {
        }

        /// <summary>
        /// Find all customers with specified email
        /// </summary>
        /// <param name="email"></param>
        /// <returns>Iqueryable of customers who have the email</returns>
        public IQueryable<Customer> FindCustomerByEmail(string email)
        {
            return DbSet.Where(h => h.Email.Equals(email));
        }

        /// <summary>
        /// locates the oldest five by DoB
        /// </summary>
        /// <returns>list of 5 oldest customers</returns>
        public List<Customer> GetOldestFive()
        {
            return DbSet.OrderBy(o => o.DateOfBirth).Take(5).ToList();
        }

        /// <summary>
        /// Checks to see if customer is in the database
        /// </summary>
        /// <param name="customerCode">customer code consisting of first lastname and dob</param>
        /// <returns>true if found otherwise false</returns>
        public bool DoesCustomerExist(string customerCode)
        {
            return DbSet.Any(o => o.CustCode == customerCode);
        }

        /// <summary>
        /// returns all customers in database
        /// </summary>
        /// <returns>list of customers in database</returns>
        public List<Customer> GetAllCustomers()
        {
            return GetAll().ToList();
        }
    }
}