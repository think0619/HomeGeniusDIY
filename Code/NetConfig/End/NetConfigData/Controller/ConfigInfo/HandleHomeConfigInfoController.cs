
using TextVoiceServer.DBContext;
using Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.IO;
using Entities.Keep;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Org.BouncyCastle.Crypto;
using log4net;
using Entities.Keep.Result;
using Entities.Content;
using Ubiety.Dns.Core.Records;
using System.ComponentModel.DataAnnotations;
using Entities.Net;
using Newtonsoft.Json.Linq;
using Org.BouncyCastle.Crypto.Tls;
using Org.BouncyCastle.Ocsp;
using System.Reflection.PortableExecutable;
using Aspose.Cells;
using System.Text;

namespace TextVoiceServer.ContentMgmt
{
    [Route("api/netcfg")]
    [ApiController]
    public class HandleHomeConfigInfoController : ControllerBase
    {
        ILog log = LogManager.GetLogger(typeof(HandleHomeConfigInfoController));
        private readonly IServiceScopeFactory _serviceScopeFactory;
        public HandleHomeConfigInfoController(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
        }

        [HttpGet]
        public string Get()
        {
            return "hah";
        }

        [HttpGet("query")]
        [HttpPost("query")]
        public async Task<IActionResult> GetAllRecord()
        {
            NetInfoView datatip = new NetInfoView()
            {
                Status = 0,
                Msg = ""
            };
            try
            {
                using (var dbcontext = _serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<DataConfigContext>())
                {
                    datatip.Data = await dbcontext.tb_config.Where(s => s.Status == 1).ToListAsync();
                    datatip.Total = dbcontext.tb_config.Where(s => s.Status == 1).Count();
                    datatip.Status = 1;
                    datatip.Msg = "Success";
                }
            }
            catch (Exception ex)
            {
                datatip.Msg = $"Exception:{ex.Message}";
            }
            return Ok(datatip);
        }

        [HttpPost("add")]
        public async Task<IActionResult> InsertRecord([FromBody] NetInfo newrecord)
        {
            TipResult tip = new TipResult()
            {
                Status = 0,
                Msg = ""
            };
            if (newrecord != null)
            {
                using var context = _serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<DataConfigContext>();
                var transaction = context.Database.BeginTransaction();
                try
                {
                    context.tb_config.Add(new NetInfo()
                    {
                        ServerName = newrecord.ServerName,
                        InnerIP = newrecord.InnerIP,
                        OuterIP = newrecord.OuterIP,
                        WebUrl = newrecord.WebUrl,
                        WebBindMail = newrecord.WebBindMail,
                        Username = newrecord.Username,
                        Userpassword = newrecord.Userpassword,
                        Token = newrecord.Token,
                        Remark = newrecord.Remark,
                        Remark2 = newrecord.Remark2,
                        Remark3 = newrecord.Remark3,
                        Status = 1,
                        MachineName = newrecord.MachineName,
                        TextRecord = newrecord.TextRecord,
                        LastUpdateTime = DateTime.Now
                    });
                    await context.SaveChangesAsync();
                    transaction.Commit();

                    tip.Status = 1;
                    tip.Msg = "Successfully added.";
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    tip.Status = 0;
                    tip.Msg = $"Exception:{ex.Message}.";
                }
            }
            else
            {
                tip.Status = 0;
                tip.Msg = "Invalid argument.";
            }
            return Ok(tip);
        }

        [HttpPost("update")]
        public async Task<IActionResult> UpdateRecord([FromBody] NetInfo updateItem)
        {
            TipResult tip = new TipResult()
            {
                Status = 0,
                Msg = ""
            };
            if (updateItem != null && updateItem.RecId > 0)
            {
                using var context = _serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<DataConfigContext>();
                var transaction = context.Database.BeginTransaction();
                try
                {
                    var updateObj = context.tb_config.Find(updateItem.RecId);
                    if (updateObj != null)
                    {
                        updateObj.Status = 1;
                        if (updateItem.ServerName != null) { updateObj.ServerName = updateItem.ServerName; }
                        if (updateItem.InnerIP != null) { updateObj.InnerIP = updateItem.InnerIP; }
                        if (updateItem.OuterIP != null) { updateObj.OuterIP = updateItem.OuterIP; }
                        if (updateItem.WebUrl != null) { updateObj.WebUrl = updateItem.WebUrl; }
                        if (updateItem.WebBindMail != null) { updateObj.WebBindMail = updateItem.WebBindMail; }
                        if (updateItem.Username != null) { updateObj.Username = updateItem.Username; }
                        if (updateItem.Userpassword != null) { updateObj.Userpassword = updateItem.Userpassword; }
                        if (updateItem.Token != null) { updateObj.Token = updateItem.Token; }
                        if (updateItem.Remark != null) { updateObj.Remark = updateItem.Remark; }
                        if (updateItem.Remark2 != null) { updateObj.Remark2 = updateItem.Remark2; }
                        if (updateItem.Remark3 != null) { updateObj.Remark3 = updateItem.Remark3; }
                        if (updateItem.MachineName != null) { updateObj.MachineName = updateItem.MachineName; }
                        if (updateItem.TextRecord != null) { updateObj.TextRecord = updateItem.TextRecord; }

                        updateObj.LastUpdateTime = DateTime.Now;
                    }
                    context.SaveChanges();
                    await transaction.CommitAsync();

                    tip.Status = 1;
                    tip.Msg = "Successfully updated.";
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    tip.Status = 0;
                    tip.Msg = $"Exception:{ex.Message}.";
                }
            }
            else
            {
                tip.Status = 0;
                tip.Msg = "Invalid argument.";
            }
            return Ok(tip);
        }

        [HttpPost("del")]
        public async Task<IActionResult> DeleteRecord([FromBody] NetInfo delItem)
        {
            TipResult tip = new TipResult()
            {
                Status = 0,
                Msg = ""
            };
            if (delItem?.RecId > 0)
            {
                using var context = _serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<DataConfigContext>();
                var transaction = context.Database.BeginTransaction();
                try
                {
                    var deleteItem = context.tb_config.Find(delItem.RecId);
                    if (deleteItem != null && deleteItem.Status == 1)
                    {
                        deleteItem.Status = 0;
                        context.SaveChanges();
                        tip.Status = 1;
                        tip.Msg = "Successfully deleted.";
                    }
                    else
                    {
                        tip.Status = 0;
                        tip.Msg = "Record not found.";
                    }
                    await transaction.CommitAsync();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    tip.Status = 0;
                    tip.Msg = $"Exception:{ex.Message}.";
                }
            }
            else
            {
                tip.Status = 0;
                tip.Msg = "Invalid argument.";
            }
            return Ok(tip);
        }

        [HttpPost("bulkdel")]
        public async Task<IActionResult> DeleteBulkRecord([FromBody] int[] IdArray)
        {
            TipResult tip = new TipResult()
            {
                Status = 0,
                Msg = ""
            };
            if (IdArray?.Length > 0)
            {
                using var context = _serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<DataConfigContext>();
                var transaction = context.Database.BeginTransaction();
                try
                {
                   // List<int> validIdList = IdArray.Where(x => x > 0).ToList();
                    var deleteItems = context.tb_config.Where(x => IdArray.Contains(x.RecId));
                    foreach (var item in deleteItems)
                    {
                        item.Status = 0;
                    }
                    context.SaveChanges();
                    tip.Status = 1;
                    tip.Msg = "Successfully deleted.";
                    await transaction.CommitAsync();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    tip.Status = 0;
                    tip.Msg = $"Exception:{ex.Message}.";
                }
            }
            else
            {
                tip.Status = 0;
                tip.Msg = "Invalid argument.";
            }
            return Ok(tip);
        }

        [HttpGet("export")]
        public async Task<IActionResult> exportRecordAsync() 
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            //Create an Excel workbook from the scratch
            Workbook ExcelFileWorkbook = new Workbook();

            //Get the first worksheet (0 indexed position) in the workbook, the default worksheet
            Worksheet ExcelFileSheet = ExcelFileWorkbook.Worksheets[0];

            //Get the cells collection in the default worksheet
            Cells SheetCells = ExcelFileSheet.Cells;

            using (var dbcontext = _serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<DataConfigContext>())
            {
                var exportDataList= dbcontext.tb_config.Where(s => s.Status == 1).ToList();
                ImportTableOptions imp = new ImportTableOptions();
                imp.InsertRows = true;

                // We pick a few columns not all to import to the worksheet 
                SheetCells.ImportCustomObjects(
                    (System.Collections.ICollection)exportDataList,
                    new string[] { "ServerName", "InnerIP", "OuterIP", "Username", "Userpassword", "Token", "Remark", "Remark2", "Remark3 ", "MachineName", "TextRecord", "WebUrl", "WebBindMail", "WebName" },
                    true,
                    0,
                    0,
                    exportDataList.Count,
                    true,
                    "YYYY-MM-DD",
                    false); 
            }
            // SheetCells.AutoFitColumns();

            ////Insert data into the cells of the sheet
            //SheetCells["A1"].PutValue("Customers Report");
            //SheetCells["A2"].PutValue("C_ID");
            //SheetCells["B2"].PutValue("C_Name");
            //SheetCells["A3"].PutValue("C001");
            //SheetCells["B3"].PutValue("Customer1");
            //SheetCells["A4"].PutValue("C002");
            //SheetCells["B4"].PutValue("Customer2");
            //SheetCells["A5"].PutValue("C003");
            //SheetCells["B5"].PutValue("Customer3");
            //SheetCells["A6"].PutValue("C004");
            //SheetCells["B6"].PutValue("Customer4");
            //return File(imageBytes, "image/jpeg");
            //Save to Excel file (XLSX)

            
            var stream = new MemoryStream();
            ExcelFileWorkbook.Save(stream, SaveFormat.Xlsx);

            // Reset the position of the stream to 0
            stream.Position = 0;

            // Set the content type and file name
            var contentType = "application/octet-stream";
            var fileName = "output.xlsx";

            // Set the response headers
            Response.Headers.Add("Content-Disposition", $"attachment; filename=\"{fileName}\"");
            Response.ContentType = contentType;

            // Write the file contents to the response body stream
            await stream.CopyToAsync(Response.Body);

            // Close the file stream
            stream.Dispose();

            // Return the response
            return new EmptyResult();
        }
    }
}
