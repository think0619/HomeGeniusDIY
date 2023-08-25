
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

namespace TextVoiceServer.ContentMgmt
{
    [Route("api/[controller]")]
    [ApiController]
    public class HandleKeepRecordController : ControllerBase
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;
        public HandleKeepRecordController(IServiceScopeFactory serviceScopeFactory )
        {
            _serviceScopeFactory = serviceScopeFactory; 
        }

        [HttpGet]
        public string Get()
        {
            return "hah";

        }

        [HttpPost("Add")]
        public string InsertRecord([FromBody] KeepRecord newInsertParam)
        {
            if (newInsertParam != null) 
            {
            using var context = _serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<DataConfigContext>();
            var transaction = context.Database.BeginTransaction();
                try
                {
                    context.tb_keeprecord.Add(new KeepRecord()
                    {
                        TypeId=newInsertParam.TypeId,
                        Count=newInsertParam.Count,
                        UnitsId=newInsertParam.UnitsId,
                        SubCount=newInsertParam.SubCount,
                        SubUnitsId=newInsertParam.SubUnitsId,
                        Status=1,
                        RecordDatetime=DateTime.Now,
                        RecordDate=newInsertParam.RecordDate, 
                    });
                    context.SaveChanges();
                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback(); 
                }
            } 
            return "HH";
        }


        [HttpGet("GetHH")]
        public void GetHH() 
        {
            //var transaction = context.Database.BeginTransaction();
            //using (var dbcontext = _serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<DataConfigContext>())
            //{
            //    foreach (var item in dbcontext.view_keeprecord.Where(alert => alert.recid != 0 && ids.Contains(alert.recid.ToString())))
            //    {
            //        item.Status = false;
            //    }
            //    dbcontext.SaveChanges();
            //}

        }


    }
}
