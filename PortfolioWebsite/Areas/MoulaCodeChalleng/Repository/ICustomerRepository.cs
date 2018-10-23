using System;
using System.Collections.Generic;
using System.Linq;
using MelbourneData.Areas.MoulaCodeChalleng.Models;

namespace MelbourneData.Areas.MoulaCodeChalleng.Repository
{
    public interface ICustomerRepository : IDisposable, IRepository<Customer>
    {
        IQueryable<Customer> FindCustomerByEmail(string email);
        List<Customer> GetOldestFive();
        bool DoesCustomerExist(string customerCode);
        List<Customer> GetAllCustomers();

    }
}