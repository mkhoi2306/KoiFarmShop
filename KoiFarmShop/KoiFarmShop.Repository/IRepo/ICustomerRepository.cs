using KoiFarmShop.Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiFarmShop.Repository.IRepo
{
    public interface ICustomerRepository
    {
        Task<bool> CreateCustomer(Customer customer);

        long GetNextCustomerId();
    }
}
