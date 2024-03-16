
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
using Ubiety.Dns.Core;
using System.Drawing;

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
                        ConfigType = newrecord.ConfigType,
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
            if (updateItem != null && updateItem.Id > 0)
            {
                using var context = _serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<DataConfigContext>();
                var transaction = context.Database.BeginTransaction();
                try
                {
                    var updateObj = context.tb_config.Find(updateItem.Id);
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
                        if (updateItem.ConfigType != null) { updateObj.ConfigType = updateItem.ConfigType; }

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
            if (delItem?.Id > 0)
            {
                using var context = _serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<DataConfigContext>();
                var transaction = context.Database.BeginTransaction();
                try
                {
                    var deleteItem = context.tb_config.Find(delItem.Id);
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
                    var deleteItems = context.tb_config.Where(x => IdArray.Contains(x.Id));
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

        [HttpPost("export")]
        public async Task<IActionResult> exportRecordAsync([FromBody] int[] IdArray) 
        {
            //var IdArray =new int[] { 1, 2, 3 };
            if (IdArray?.Length > 0)
            {
                Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
                Workbook ExcelFileWorkbook = new Workbook();
                Worksheet ExcelFileSheet = ExcelFileWorkbook.Worksheets[0];
                Cells SheetCells = ExcelFileSheet.Cells;

                using (var dbcontext = _serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<DataConfigContext>())
                {
                    var exportDataList = dbcontext.tb_config.Where(s => s.Status == 1 && IdArray.Contains(s.Id)).ToList();

                    ImportTableOptions imp = new ImportTableOptions();
                    imp.InsertRows = true;
                    SheetCells.ImportCustomObjects(
                        (System.Collections.ICollection)exportDataList,
                        new string[] { "ServerName", "InnerIP", "OuterIP", "Username", "Userpassword", "Token", "Remark", "Remark2", "Remark3 ", "MachineName", "TextRecord", "WebUrl", "WebBindMail", "WebName", "ConfigType" },
                        true,
                        0,
                        0,
                        exportDataList.Count,
                        true,
                        "yyyy-MM-dd",
                        false);
                }
                 
                Response.Headers.Add("Access-Control-Expose-Headers", "Content-Disposition"); 
                Response.Headers.Add("Content-Disposition", $"attachment; Filename=\"{$"ConfigData_{DateTime.Now.ToString("yyyyMMdd")}.xlsx"}\"");
                Response.ContentType = "application/octet-stream"; 

                //Create a named range
                Aspose.Cells.Range range = ExcelFileSheet.Cells.CreateRange("A1", "E1");
                //Set the name of the named range
                range.Name = "Range1";
                Aspose.Cells.Style style = ExcelFileWorkbook.CreateStyle();
                style.ForegroundColor = System.Drawing.Color.FromArgb(50, 83, 220);
                style.Pattern = BackgroundType.Solid;
                style.Font.Color = Color.White;
                style.Font.Size = 12;

                // Adding a thick top border with blue line
                range.SetOutlineBorder(BorderType.TopBorder, CellBorderType.Thick, Color.Blue);

                // Adding a thick bottom border with blue line
                range.SetOutlineBorder(BorderType.BottomBorder, CellBorderType.Thick, Color.Blue);

                // Adding a thick left border with blue line
                range.SetOutlineBorder(BorderType.LeftBorder, CellBorderType.Thick, Color.Blue);

                // Adding a thick right border with blue line
                range.SetOutlineBorder(BorderType.RightBorder, CellBorderType.Thick, Color.Blue);

                StyleFlag styleFlag = new StyleFlag();
                styleFlag.All = true;
                range.ApplyStyle(style, styleFlag);

                ExcelFileSheet.AutoFitColumns(0, 0, 0, 5); 

                var stream = new MemoryStream();
                ExcelFileWorkbook.Save(stream, SaveFormat.Xlsx);
                stream.Position = 0;

                await stream.CopyToAsync(Response.Body); 
                stream.Dispose(); 
            } 
            return new EmptyResult();
        }
    }
}
