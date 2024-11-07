using KoiFarmShop.Repository.IRepo;
using KoiFarmShop.Repository.Models;
using KoiFarmShop.Service.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiFarmShop.Service.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerService(ICustomerRepository customerRepository)
        {
            this._customerRepository = customerRepository;
        }

        public async Task<bool> CreateCustomer(Customer customer)
        {
            return await _customerRepository.CreateCustomer(customer);
        }

        public long GetNextCustomerId()
        {
            return _customerRepository.GetNextCustomerId();
        }
    }
}
