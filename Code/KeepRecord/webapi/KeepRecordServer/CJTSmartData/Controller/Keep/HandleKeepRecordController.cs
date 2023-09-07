
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

namespace TextVoiceServer.ContentMgmt
{
    [Route("api/[controller]")]
    [ApiController]
    public class HandleKeepRecordController : ControllerBase
    {
        ILog log = LogManager.GetLogger(typeof(HandleKeepRecordController));
        private readonly IServiceScopeFactory _serviceScopeFactory;
        public HandleKeepRecordController(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
        }

        [HttpGet]
        public string Get()
        {
            return "hah";

        }

        [HttpPost("Add")]
        public async Task<IActionResult> InsertRecord([FromBody] KeepRecord newInsertParam)
        {
            TipResult tip = new TipResult()
            {
                Status = 0,
                Msg = ""
            };
            if (newInsertParam != null)
            {
                using var context = _serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<DataConfigContext>();
                var transaction = context.Database.BeginTransaction();
                try
                {
                    context.tb_keeprecord.Add(new KeepRecord()
                    {
                        TypeId = newInsertParam.TypeId,
                        Count = newInsertParam.Count,
                        UnitsId = newInsertParam.UnitsId,
                        SubCount = newInsertParam.SubCount,
                        SubUnitsId = newInsertParam.SubUnitsId,
                        Status = 1,
                        RecordDatetime = DateTime.Now,
                        RecordDate = newInsertParam.RecordDate,
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
                tip.Msg = "params error.";
            }
            return Ok(tip);
        }


        [HttpPost("Update")]
        public async Task<IActionResult> UpdateRecord([FromBody] KeepRecord updateParam)
        {
            TipResult tip = new TipResult()
            {
                Status = 0,
                Msg = ""
            };
            if (updateParam != null)
            {
                using var context = _serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<DataConfigContext>();
                var transaction = context.Database.BeginTransaction();
                try
                {
                    var updateObj = context.tb_keeprecord.Find(updateParam.RecID);
                    if(updateObj != null) 
                    {
                        updateObj.TypeId = updateObj.TypeId;
                        updateObj.Count = updateObj.Count;
                        updateObj.UnitsId = updateObj.UnitsId;
                        updateObj.SubCount = updateObj.SubCount;
                        updateObj.SubUnitsId = updateObj.SubUnitsId;
                        updateObj.Status = 1;
                    }

                    //context.tb_keeprecord.Add(new KeepRecord()
                    //{
                    //    TypeId = newInsertParam.TypeId,
                    //    Count = newInsertParam.Count,
                    //    UnitsId = newInsertParam.UnitsId,
                    //    SubCount = newInsertParam.SubCount,
                    //    SubUnitsId = newInsertParam.SubUnitsId,
                    //    Status = 1,
                    //    RecordDatetime = DateTime.Now,
                    //    RecordDate = newInsertParam.RecordDate,
                    //});
                  //  await context.SaveChangesAsync();
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
                tip.Msg = "params error.";
            }
            return Ok(tip);
        }


        [HttpGet("Query")]
        [HttpPost("Query")]
        public async Task<IActionResult> GetRecord([FromBody] PageQuery pagequery)
        {
            KeepRecordViewTip datatip = new KeepRecordViewTip()
            {
                Status = 0,
                Data = new List<KeepRecordView>()
            };
            try
            {
                using (var dbcontext = _serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<DataConfigContext>())
                {
                    int skipcount = (pagequery.PageIndex <= 1) ? 0 : ((pagequery.PageIndex - 1) * pagequery.PageCount);
                    datatip.Data = await dbcontext.view_keeprecord.OrderBy(x => x.RecID).Skip(skipcount).Take(pagequery.PageCount).ToListAsync();
                    datatip.Total = dbcontext.view_keeprecord.Count();
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

        [HttpPost("Del")]
        public async Task<IActionResult> DeleteRecord([FromBody] KeepRecord keeprec)
        {
            TipResult tip = new TipResult()
            {
                Status = 0,
                Msg = ""
            };
            if (keeprec?.RecID != -1)
            {
                using var context = _serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<DataConfigContext>();
                var transaction = context.Database.BeginTransaction();
                try
                {
                    var deleteItem = context.tb_keeprecord.Find(keeprec.RecID);
                    if (deleteItem != null)
                    {
                        deleteItem.Status = 0;
                    }
                    await context.SaveChangesAsync();
                    transaction.Commit();

                    tip.Status = 1;
                    tip.Msg = "Deleted successfully.";
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
                tip.Msg = "params error";
            } 
            return Ok(tip);
        }

    }
}
