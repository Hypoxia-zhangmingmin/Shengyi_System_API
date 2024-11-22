
using DemoTest.Utils;
using Microsoft.EntityFrameworkCore;
using Shengyi_WebAPI.EFCore;

namespace Shengyi_WebAPI.Daos.Customers
{
    public class CustomerDao : ICustomerDao
    {
        private readonly DB _db;
        public CustomerDao(DB db)
        {
            _db = db;
        }

        public async Task<object> CreateCustomerAsync(string name, string phone, string address, int creaditId)
        {
            Customer customer = new Customer()
            {
                Name = name,
                Phone = phone,
                Address = address,
                CreditId = creaditId,
                CreateTime = DateTime.Now,
                IsDelete = false
            };
            _db.Customers.Add(customer);
            return await _db.SaveChangesAsync() >= 1;
        }

        public async Task<object> DeleteCustomerAsync(int id)
        {
            var customer = await _db.Customers.SingleOrDefaultAsync(a => a.CustomerId == id);
            if(customer == null)
            {
                return false;
            }
            _db.Remove(customer);
            return await _db.SaveChangesAsync()>=1;
        }

        public async Task<object> EditCustomerAsync(int id, string name, string phone, string address, int creaditId)
        {
            var customer = await _db.Customers.SingleOrDefaultAsync(a => a.CustomerId == id);
            if(customer == null)
            {
                return false;
            }
            customer.Name = name;
            customer.Phone = phone;
            customer.Address = address;
            customer.CreditId = creaditId;
            return await _db.SaveChangesAsync() >= 1;
        }

        public async Task<object> GetAddress(int id)
        {
            var result =await _db.Customers.SingleOrDefaultAsync(a => a.CustomerId == id);
            if(result == null)
            {
                return false;
            }
            return result;
        }

        public async Task<object> GetAll()
        {
            var result = await _db.Customers.Select(a => new
            {
                a.CustomerId,
                a.Name,
                a.Address
            }).ToListAsync();
            return result;
        }

        public async Task<object> SearchCreditAsync()
        {
            var result = await _db.Credits.Select(a => new
            {
                a.Id,
                a.CreditName
            }).ToListAsync();
            return result;
        }

        public async Task<object> SearchCustomerAsync(string name)
        {
            var result = await _db.Customers.Where(a => string.IsNullOrWhiteSpace(name) || a.Name.Contains(name)).Select(a => new
            {
                Id = a.CustomerId,
                a.Name,
                a.Phone,
                a.Address,
                a.CreditId,
                a.Credit.CreditName
            }).ToListAsync();
            return result;
        }
    }
}
