
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
                    if (newInsertParam.Id > 0)
                    {
                        var updateObj = context.tb_keeprecord.Where(x => x.Id == newInsertParam.Id).FirstOrDefault();
                        updateObj.TypeId = newInsertParam.TypeId;
                        updateObj.Count = newInsertParam.Count;
                        updateObj.UnitsId = newInsertParam.UnitsId;
                        updateObj.SubCount = newInsertParam.SubCount;
                        updateObj.SubUnitsId = newInsertParam.SubUnitsId;
                        updateObj.Status = 1;
                        updateObj.RecordDatetime = DateTime.Now;
                        updateObj.RecordDate = newInsertParam.RecordDate; 
                    }
                    else
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
                    }
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
                    var updateObj = context.tb_keeprecord.Find(updateParam.Id);
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
                    await context.SaveChangesAsync();
                    transaction.Commit();

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
                tip.Msg = "params error.";
            }
            return Ok(tip);
        }


        [HttpGet("Query")]
        [HttpPost("Query")]
        public async Task<IActionResult> GetRecord([FromBody] PageQuery pagequery)
        {
            if(!pagequery.DateStart.HasValue) 
            {
                pagequery.DateStart=DateTime.MinValue;
            }
            if (!pagequery.DateEnd.HasValue)
            {
                pagequery.DateEnd = DateTime.MaxValue;
            }
            else 
            {
                //if (pagequery.DateEnd != DateTime.MaxValue) 
                //{
                //    pagequery.DateEnd= pagequery.DateEnd.Value.AddDays(1); 
                //} 
            }

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
                    datatip.Data = await dbcontext.view_keeprecord.Where(a => a.RecordDate >= pagequery.DateStart && a.RecordDate <= pagequery.DateEnd ).OrderBy(x => x.Id).Skip(skipcount).Take(pagequery.PageCount).ToListAsync();
                    datatip.Total = dbcontext.view_keeprecord.Where(a => a.RecordDate >= pagequery.DateStart && a.RecordDate <= pagequery.DateEnd ).Count(); 
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
         
        [HttpPost("RecordInfo")]
        public async Task<IActionResult> GetSingleRecord([FromBody] KeepRecord KeepRecord)
        { 
            int Id = KeepRecord.Id;
            KeepRecordResult datatip = new KeepRecordResult()
            {
                Status = 0,
                Data = new KeepRecord()
            };
            if (Id >= 0)
            {
                try
                {
                    using (var dbcontext = _serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<DataConfigContext>())
                    {
                        datatip.Data = await dbcontext.tb_keeprecord.Where(a => a.Id == Id && a.Status == 1).FirstOrDefaultAsync();
                        datatip.Status = 1;
                        datatip.Msg = "Success";
                    }
                }
                catch (Exception ex)
                {
                    datatip.Msg = $"Exception:{ex.Message}";
                }
            }
            else 
            {
                datatip.Status = 0;
                datatip.Msg = "Fail";
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
            if (keeprec?.Id != -1)
            {
                using var context = _serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<DataConfigContext>();
                var transaction = context.Database.BeginTransaction();
                try
                {
                    var deleteItem = context.tb_keeprecord.Find(keeprec.Id);
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
