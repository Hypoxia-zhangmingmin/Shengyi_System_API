
using Microsoft.EntityFrameworkCore;
using Shengyi_WebAPI.EFCore;

namespace Shengyi_WebAPI.Daos.Suppliers
{
    public class SupplierDao : ISupplierDao
    {
        private readonly DB _db;
        public SupplierDao(DB db)
        {
            _db = db;
        }
        public async Task<object> CreateSupplierAsync(string supplierName, string phone, string address)
        {
            _db.Suppliers.Add(new Supplier()
            {
                SupplierName = supplierName,
                Phone = phone,
                Address = address,
                CreateTime = DateTime.Now,
                IsDelete = false
            });
            return await _db.SaveChangesAsync() >=1;
        }

        public async Task<object> DeleteSupplierAsync(int id)
        {
            var supplier =await _db.Suppliers.SingleOrDefaultAsync(a=>a.Id == id);
            if(supplier == null)
            {
                return false;
            }
            _db.Suppliers.Remove(supplier);
            return await _db.SaveChangesAsync() >=1;
        }

        public async Task<object> EditSupplierAsync(int id, string supplierName, string phone, string address)
        {
            var supplier = await _db.Suppliers.SingleOrDefaultAsync(a => a.Id == id);
            if(supplier == null)
            {
                return false;
            }
            supplier.SupplierName = supplierName;
            supplier.Phone =    phone;
            supplier.Address = address;
            return await _db.SaveChangesAsync() >= 1;
        }

        public async Task<object> GetAll()
        {
            var result =await _db.Suppliers.Select(a => new
            {
                a.Id,
                a.SupplierName
            }).ToListAsync();
            return result;
        }

        public async Task<object> SearchSupplierAsync(string supplierName)
        {
            var result =await _db.Suppliers.Where(a=> string.IsNullOrWhiteSpace(supplierName) || a.SupplierName.Contains(supplierName)).Select(a => new
            {
                a.Id,
                a.SupplierName,
                a.Phone,
                a.Address
            }).ToListAsync();
            return result;
        }
    }
}
