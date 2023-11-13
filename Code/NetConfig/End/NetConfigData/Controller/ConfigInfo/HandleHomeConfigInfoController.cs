
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
                    datatip.Data = await dbcontext.tb_config.Where(s=>s.Status==1).ToListAsync();
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
                        Username = newrecord.Username,
                        Userpassword = newrecord.Userpassword,
                        Token = newrecord.Token,
                        Remark = newrecord.Remark,
                        Remark2 = newrecord.Remark2,
                        Remark3 = newrecord.Remark3,
                        Status = 1,
                        MachineName = newrecord.MachineName,
                        TextRecord = newrecord.TextRecord, 
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
            if (updateItem != null && updateItem.RecId>0)
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
                        if (updateItem.Username != null) { updateObj.Username = updateItem.Username; }
                        if (updateItem.Userpassword != null) { updateObj.Userpassword = updateItem.Userpassword; }
                        if (updateItem.Token != null) { updateObj.Token = updateItem.Token; }
                        if (updateItem.Remark != null) { updateObj.Remark = updateItem.Remark; } 
                        if (updateItem.Remark2 != null) { updateObj.Remark2 = updateItem.Remark2; } 
                        if (updateItem.Remark3 != null) { updateObj.Remark3 = updateItem.Remark3; } 
                        if (updateItem.MachineName != null) { updateObj.MachineName = updateItem.MachineName; }
                        if (updateItem.TextRecord != null) { updateObj.TextRecord = updateItem.TextRecord; } 
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
                    if (deleteItem != null)
                    {
                        deleteItem.Status = 0;
                        context.SaveChanges();
                        tip.Status = 1;
                        tip.Msg = "Successfully deleted.";
                    }
                    else
                    {
                        tip.Status = 1;
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
    }
}
