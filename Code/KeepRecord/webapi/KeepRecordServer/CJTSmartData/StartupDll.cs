using Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq; 
using System.Threading.Tasks; 

namespace TextVoiceServer
{
    public partial class Startup
    {

        /// <summary>
        /// 初始化Redis缓存
        /// </summary>
        private void InitRedis()
        {
            //redis缓存
            var section = Configuration.GetSection("Redis:Default");

            //连接字符串
            string _connectionString = section.GetSection("Connection").Value;

            //默认数据库
            int _defaultDB = int.Parse(section.GetSection("DefaultDB").Value ?? "1");

            //前缀
            string _prefixString = section.GetSection("Prefix").Value; 

            //crredis connection string
            var csredis = new CSRedis.CSRedisClient($"{_connectionString},defaultDatabase={_defaultDB},idleTimeout=3000,poolsize=5,prefix={_prefixString}:KEY_");
            RedisHelper.Initialization(csredis);
        }
    } 
}
