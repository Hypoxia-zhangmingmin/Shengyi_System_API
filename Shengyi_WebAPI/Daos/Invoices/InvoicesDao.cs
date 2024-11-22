using Microsoft.EntityFrameworkCore;
using Shengyi_WebAPI.EFCore;
using System.Globalization;
using System.Security.Cryptography;
using FluentFTP;
using System.Net;
using Shengyi_WebAPI.Models.Out;
using Microsoft.AspNetCore.Mvc;
using Shengyi_WebAPI.Models.In;

namespace Shengyi_WebAPI.Daos.Invoices
{
    public class InvoicesDao:IInvoicesDao
    {
        private readonly DB _db;
        private string ftpUrl = "ftp://192.168.20.209";
        private string username = "shengyi";
        private string password = "shengyi051505";
        public InvoicesDao(DB db)
        {
            _db = db;
        }
        /// <summary>
        /// 添加开出发票的信息
        /// </summary>
        /// <param name="file"></param>
        /// <param name="customerId"></param>
        /// <param name="totalPrice"></param>
        /// <param name="taxRate"></param>
        /// <param name="price"></param>
        /// <param name="tax"></param>
        /// <param name="date"></param>
        /// <param name="md5"></param>
        /// <param name="remark"></param>
        /// <returns></returns>
        public async Task<object> CreateSaleAsync(IFormFile file, int customerId, decimal totalPrice, decimal taxRate, decimal price, decimal tax, string date, string md5, string remark)
        {
            if (file == null || file.Length == 0)
                return false;

            var fileExtension = Path.GetExtension(file.FileName);

            var newFileName = $"{Guid.NewGuid()}{fileExtension}";
            //var filePath = Path.Combine(newFileName, fileExtension);
            // 将文件保存到本地临时路径
            var filePath = Path.Combine(Path.GetTempPath(), newFileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            bool uploadSuccess = UploadToFTP(ftpUrl+"/Sale", filePath, username, password);

            string comMd5 = ComputeMd5(file);
            if(comMd5 != md5)
            {
                return false;
            }
            System.IO.File.Delete(filePath);
            if (!uploadSuccess)
                return false;
            SalesInvoice salesInvoice = new SalesInvoice()
            {
                Date = Convert.ToDateTime(date),
                CustomerId = customerId,
                Amount = price,
                TaxRate = taxRate == 1 ?0.13m:0.3m,
                Tax = tax,
                TotalAmount = totalPrice,
                FileName = newFileName,
                Remark = remark,
                CreateTime = DateTime.Now,
                IsDelete = false,
            };
            _db.SalesInvoices.Add(salesInvoice);
            return await _db.SaveChangesAsync() >= 1;
        }

        /// <summary>
        /// 查询购入的发票信息
        /// </summary>
        /// <param name="supplier"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="currentpage"></param>
        /// <returns></returns>
        public async Task<object> SearchPurchaseAsync(string supplier, string start, string end, int currentpage)
        {
            var data = _db.PurchaseInvoices.AsQueryable();

            DateTime? startDate = string.IsNullOrWhiteSpace(start) ? (DateTime?)null : DateTime.Parse(start);
            DateTime? endDate = string.IsNullOrWhiteSpace(end) ? (DateTime?)null : DateTime.Parse(end);
            if (!string.IsNullOrWhiteSpace(start))
            {
                data = data.Where(a => a.Date >= startDate);
            }
            if (!string.IsNullOrWhiteSpace(end))
            {
                data = data.Where(a => a.Date <= endDate);
            }
            if (!string.IsNullOrWhiteSpace(supplier)){
                data = data.Where(a => a.Supplier.SupplierName.Contains(supplier));
            }
            var result =await data.Where(a => a.IsDelete == false).Select(a => new
            {
                a.Id,
                Date = a.Date.ToString("yyyy-MM-dd"),
                a.Supplier.SupplierName,
                a.Supplier.Phone,
                a.TotalAmount,
                a.TaxRate
            }).ToListAsync();
            return result;
        }

        public async Task<object> SearchSaleAsync(string customer, string start, string end, int currentpage)
        {
            var data = _db.SalesInvoices.AsQueryable();

            DateTime? startDate = string.IsNullOrWhiteSpace(start) ? (DateTime?)null : DateTime.Parse(start);
            DateTime? endDate = string.IsNullOrWhiteSpace(end) ? (DateTime?)null : DateTime.Parse(end);
            if (!string.IsNullOrWhiteSpace(start))
            {
                data = data.Where(a => Convert.ToDateTime(a.Date).Date >= Convert.ToDateTime(start).Date);
            }
            if (!string.IsNullOrWhiteSpace(end))
            {
                data = data.Where(a => Convert.ToDateTime(a.Date).Date <= Convert.ToDateTime(end).Date);
            }
            if (!string.IsNullOrEmpty(customer))
            {
                data = data.Where(a => a.Customer.Name.Contains(customer));
            }
            var result = await data.Where(a =>  a.IsDelete == false).Select(a => new
            {
                a.Id,
                Date = a.Date.ToString("yyyy-MM-dd"),
                CustomerName = a.Customer.Name,
                a.TotalAmount,
                a.TaxRate
            }).ToListAsync();
            return result;
        }
        private string ComputeMd5(IFormFile file)
        {
            using(var md5 = MD5.Create())
            using (var stream = file.OpenReadStream())
            {
                var hash = md5.ComputeHash(stream);
                return BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();
            }
        }
       
        private bool UploadToFTP(string ftpUrl, string filePath, string username, string password)
        {
            try
            {
                FileInfo fileInfo = new FileInfo(filePath);
                string uri = ftpUrl + "/" + fileInfo.Name;

                new Guid().ToString();

                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(uri);
                request.Method = WebRequestMethods.Ftp.UploadFile;
                request.Credentials = new NetworkCredential(username, password);
                request.ContentLength = fileInfo.Length;
                request.UsePassive = true;  // 设置为被动模式
                request.UseBinary = true;   // 设置为二进制模式
                request.KeepAlive = false;

                using (FileStream fileStream = fileInfo.OpenRead())
                using (Stream requestStream = request.GetRequestStream())
                {
                    byte[] buffer = new byte[4096];
                    int bytesRead;
                    while ((bytesRead = fileStream.Read(buffer, 0, buffer.Length)) != 0)
                    {
                        requestStream.Write(buffer, 0, bytesRead);
                    }
                }

                using (FtpWebResponse response = (FtpWebResponse)request.GetResponse())
                {
                    Console.WriteLine($"Upload File Complete, status {response.StatusDescription}");
                }

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error uploading to FTP: " + ex.Message);
                return false;
            }
        }

        public async Task<FileDownloadResult> GetSaleFileAsync(int id)
        {
            var result = _db.SalesInvoices.SingleOrDefault(a => a.Id == id);
            if(result == null)
            {
                return null;
            }
            string uri = ftpUrl + "/Sale/" + result.FileName;
            FtpWebRequest request = (FtpWebRequest)WebRequest.Create(uri);
            request.Method = WebRequestMethods.Ftp.DownloadFile;
            request.Credentials = new NetworkCredential(username, password);

            request.UsePassive = true;
            request.UseBinary = true;   // 设置为二进制模式
            request.KeepAlive = false;

            try
            {
                using FtpWebResponse response = (FtpWebResponse)await request.GetResponseAsync();
                using Stream responseStream = response.GetResponseStream();

                // 将文件流复制到 MemoryStream
                MemoryStream memoryStream = new MemoryStream();
                await responseStream.CopyToAsync(memoryStream);

                // 重置内存流的位置，以便读取
                memoryStream.Position = 0;

                return new FileDownloadResult
                {
                    FileStream = memoryStream,
                    FileName = result.FileName
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error downloading from FTP: " + ex.Message);
                return null;
            }
        }

        public async Task<FileDownloadResult> GetPurchaseFileAsync(int id)
        {
            var result = _db.PurchaseInvoices.SingleOrDefault(a => a.Id == id);
            if (result == null)
            {
                return null;
            }
            string uri = ftpUrl + "/Purchase/" + result.FileName;
            FtpWebRequest request = (FtpWebRequest)WebRequest.Create(uri);
            request.Method = WebRequestMethods.Ftp.DownloadFile;
            request.Credentials = new NetworkCredential(username, password);

            request.UsePassive = true;
            request.UseBinary = true;   // 设置为二进制模式
            request.KeepAlive = false;

            try
            {
                using FtpWebResponse response = (FtpWebResponse)await request.GetResponseAsync();
                using Stream responseStream = response.GetResponseStream();

                // 将文件流复制到 MemoryStream
                MemoryStream memoryStream = new MemoryStream();
                await responseStream.CopyToAsync(memoryStream);

                // 重置内存流的位置，以便读取
                memoryStream.Position = 0;

                return new FileDownloadResult
                {
                    FileStream = memoryStream,
                    FileName = result.FileName
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error downloading from FTP: " + ex.Message);
                return null;
            }
        }

        public async Task<object> CreatePurchaseAsync(IFormFile file, int supplierId, decimal totalPrice, decimal taxRate, decimal price, decimal tax, string date, string md5, string remark)
        {
            if (file == null || file.Length == 0)
                return false;

            var fileExtension = Path.GetExtension(file.FileName);

            var newFileName = $"{Guid.NewGuid()}{fileExtension}";
            //var filePath = Path.Combine(newFileName, fileExtension);
            // 将文件保存到本地临时路径
            var filePath = Path.Combine(Path.GetTempPath(), newFileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            bool uploadSuccess = UploadToFTP(ftpUrl+"/Purchase", filePath, username, password);

            string comMd5 = ComputeMd5(file);
            if (comMd5 != md5)
            {
                return false;
            }
            System.IO.File.Delete(filePath);
            if (!uploadSuccess)
                return false;
            PurchaseInvoice purchaseInvoice = new PurchaseInvoice()
            {
                Date = Convert.ToDateTime(date),
                SupplierId = supplierId,
                Amount = price,
                TaxRate = taxRate == 1 ? 0.13m : 0.3m,
                Tax = tax,
                TotalAmount = totalPrice,
                FileName = newFileName,
                Remark = remark,
                CreateTime = DateTime.Now,
                IsDelete = false,
            };
            _db.PurchaseInvoices.Add(purchaseInvoice);
            return await _db.SaveChangesAsync() >= 1;
        }
    }
}
