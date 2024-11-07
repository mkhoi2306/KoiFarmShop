using KoiFarmShop.Repository.IRepo;
using KoiFarmShop.Repository.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiFarmShop.Repository.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly KoiFarmShopContext _context;

        public CustomerRepository(KoiFarmShopContext context)
        {
            this._context = context;
        }
        public async Task<bool> CreateCustomer(Customer customer)
        {
            //if (user.Role == "Customer")
            //{
            //    Customer customer = new Customer();
            //    customer.UserId = user.UserId;
            //    customer.Email = user.Email;
            //    customer.Phone = user.Phone;
            //    customer.CreatedDate = DateTime.Now;
            //    customer.CreatedBy = user.UserName;
            //    await _context.Customers.AddAsync(customer);
            //    await _context.SaveChangesAsync();

            //}
            try
            {
                await _context.Customers.AddAsync(customer);
                await _context.SaveChangesAsync();
                return true;
            }catch (Exception ex)
            {
                return false;
            }
        }

        public long GetNextCustomerId()
        {
            var maxId = _context.Customers.Max(c => (long?)c.CustomerId) ?? 0;
            return maxId + 1;
        }
    }
}
