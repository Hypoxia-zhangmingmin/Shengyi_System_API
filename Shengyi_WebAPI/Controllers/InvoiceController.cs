using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Shengyi_WebAPI.Models.In;
using Shengyi_WebAPI.Models.Out;
using Shengyi_WebAPI.Services.Invoices;
using System.Globalization;
using System.Net;

namespace Shengyi_WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoiceController : ControllerBase
    {
        private readonly IInvoicesService _invoicesService;

        public InvoiceController(IInvoicesService invoicesService)
        {
            _invoicesService = invoicesService;
        }

        /// <summary>
        /// 查询开出的发票信息
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        [HttpPost("sale/search")]
        public async Task<object> SearchSaleAsync([FromBody] SearchSaleInvoicesInfo info)
        {
            var result = await _invoicesService.SearchSaleAsync(info.Customer, info.Start, info.End, info.CurrentPage);
            return Ok(new OutputModel<object>().Success(result));
        }
        /// <summary>
        /// 查询收入的发票信息
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        [HttpPost("purchase/search")]
        public async Task<object> SearchPurchase([FromBody] SearchPurchaseInvoicesInfo info)
        {
            var result = await _invoicesService.SearchPurchaseAsync(info.supplier, info.Start, info.End, info.CurrentPage);
            return Ok(new OutputModel<object>().Success(result));
        }
        /// <summary>
        /// 添加开出的发票信息
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        [HttpPost("sale/create")]
        public async Task<object> CreateSaleAsync([FromForm] CreateSaleInvoiceInfo info)
        {
            var result = await _invoicesService.CreateSaleAsync(info.File, info.CustomerId, info.TotalPrice, info.TaxRate, info.Price, info.Tax, info.Date, info.Md5, info.Remark);
            return ((bool)result) == true ? Ok(new OutputModel<object>().Success(result)) : Ok(new OutputModel<object>().Failed());
        }

        /// <summary>
        /// 添加购入的发票信息
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        [HttpPost("purchase/create")]
        public async Task<object> CreatePurchaseAsync([FromForm] CreatePurchaseInvoiceInfo info)
        {
            var result = await _invoicesService.CreatePurchaseAsync(info.File, info.SupplierId, info.TotalPrice, info.TaxRate, info.Price, info.Tax, info.Date, info.Md5, info.Remark);
            return ((bool)result) == true ? Ok(new OutputModel<object>().Success(result)) : Ok(new OutputModel<object>().Failed());
        }
        /// <summary>
        /// 在线查看开出的发票文件
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("sale/getfile")]
        public async Task<IActionResult> GetSaleFile(int id )
        {
            var fileResult = await _invoicesService.GetSaleFileAsync(id);
            if (fileResult == null)
            {
                return Ok(new OutputModel<object>().Failed());
            }
            string mimeType = GetMimeType(fileResult.FileName);
            Response.Headers.Add("Content-Disposition", $"inline; filename=\"{fileResult.FileName}\"");
            return File(fileResult.FileStream, mimeType,fileResult.FileName);
        }
        /// <summary>
        /// 下载开出的发票文件
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("sale/downloadfile")]
        public async Task<IActionResult> DownloadSaleFile(int id)
        {
            var fileResult = await _invoicesService.GetSaleFileAsync(id);
            if (fileResult == null)
            {
                return Ok(new OutputModel<object>().Failed());
            }
            string mimeType = GetMimeType(fileResult.FileName);
            Response.Headers.Add("Content-Disposition", $"attachment; filename=\"{fileResult.FileName}\"");
            return File(fileResult.FileStream, mimeType, fileResult.FileName);
        }
        /// <summary>
        /// 在线查看购入的发票文件
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("purchase/getfile")]
        public async Task<IActionResult> GetPurchaseFile(int id)
        {
            var fileResult = await _invoicesService.GetPurchaseFileAsync(id);
            if (fileResult == null)
            {
                return Ok(new OutputModel<object>().Failed());
            }
            string mimeType = GetMimeType(fileResult.FileName);
            Response.Headers.Add("Content-Disposition", $"inline; filename=\"{fileResult.FileName}\"");
            return File(fileResult.FileStream, mimeType, fileResult.FileName);
        }
        /// <summary>
        /// 下载收入的发票文件
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("purchase/downloadfile")]
        public async Task<IActionResult> DownloadPurchaseFile(int id)
        {
            var fileResult = await _invoicesService.GetPurchaseFileAsync(id);
            if (fileResult == null)
            {
                return Ok(new OutputModel<object>().Failed());
            }
            string mimeType = GetMimeType(fileResult.FileName);
            Response.Headers.Add("Content-Disposition", $"attachment; filename=\"{fileResult.FileName}\"");
            return File(fileResult.FileStream, mimeType, fileResult.FileName);
        }

        /// <summary>
        /// 判断获取到的文件类型
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        private string GetMimeType(string fileName)
        {
            var extension = Path.GetExtension(fileName).ToLowerInvariant();

            return extension switch
            {
                ".pdf" => "application/pdf",
                ".jpg" => "image/jpeg",
                ".jpeg" => "image/jpeg",
                ".png" => "image/png",
                _ => "application/octet-stream" // Default binary stream
            };
        }





        [HttpPost("sale/fil")]
        public async Task<IActionResult> UploadFile(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("File is not selected");

            // 将文件保存到本地临时路径
            var filePath = Path.Combine(Path.GetTempPath(), file.FileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            string ftpUrl = "ftp://192.168.20.209";
            string username = "shengyi";
            string password = "shengyi051505";

            bool uploadSuccess = UploadToFTP(ftpUrl, filePath, username, password);

            System.IO.File.Delete(filePath);
            if (uploadSuccess)
                return Ok(new OutputModel<object>().Success());
            else
                return Ok(new OutputModel<object>().Failed());
        }


        private bool UploadToFTP(string ftpUrl, string filePath, string username, string password)
        {
            try
            {
                FileInfo fileInfo = new FileInfo(filePath);
                string uri = ftpUrl + "/" + fileInfo.Name;

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

    }
}
