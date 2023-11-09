using Entities;
using Entities.Auth;
using Microsoft.AspNetCore.SignalR;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using CJTSmartData.DBContext;

namespace CJTSmartData.Hubs
{
    public class DataProviderHub : Hub
    {
        private readonly IConnectionMultiplexer _redis;
        private readonly IServiceScopeFactory _serviceScopeFactory;
         


        public DataProviderHub(IConnectionMultiplexer redis,IServiceScopeFactory serviceScopeFactory) 
        {
            _redis=redis;
            _serviceScopeFactory=serviceScopeFactory;
        } 

       

        public override Task OnConnectedAsync()
        { 
            return base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            string connectionId = Context.ConnectionId; 
            var redisdb = _redis.GetDatabase(1);
            string userId=  redisdb.StringGet(String.Format($"{ConstValue.SignalRConnectPre}_{connectionId}"));
            if (userId != null)
            {
                redisdb.KeyDelete(String.Format($"{ConstValue.SignalRConnectPre}_{connectionId}"));

                //update Redis 
                var userConnections = redisdb.StringGet(userId);
                if (userConnections != RedisValue.Null)
                {
                    List<string> userConnectionList = JsonHelper.DeserializeJsonToObject<List<string>>(userConnections);
                    List<string> newUserConnectionList = new List<string>();
                    foreach (var _connectionId in userConnectionList)
                    {
                        if (_connectionId != connectionId)
                        {
                            newUserConnectionList.Add(_connectionId);
                        }
                    }

                    if (newUserConnectionList.Count == 0)
                    {
                        await redisdb.KeyDeleteAsync(userId);
                    }
                    else
                    {
                        await redisdb.StringSetAsync(userId, JsonHelper.SerializeObject(newUserConnectionList));
                    }
                }
            }

            else 
            {
                //delete admin 
                List<string> connectionIds = new List<string>();
                var userConnections = redisdb.StringGet(ConstValue.SignalRAdmin);
                if (userConnections != RedisValue.Null)
                {
                    connectionIds = JsonHelper.DeserializeJsonToList<String>(userConnections);
                    if (connectionIds!=null && connectionIds.Contains(connectionId))
                    {
                        connectionIds.Remove(connectionId);
                    }
                } 
              await  redisdb.StringSetAsync(ConstValue.SignalRAdmin, JsonHelper.SerializeObject(connectionIds));
            }
         

            await base.OnDisconnectedAsync(exception); 
        }

        #region Test
        public async Task Send(string userId)
        {
            var message = $"Send message to you with user id { Context.ConnectionId}";
            var clients = Clients.All;

            var xxxx = Clients.Client("xxx");
            if (xxxx == null)
            {
                string x = "1";
            }

            await Clients.All.SendAsync("onStringTest", message);
            // await Clients.Client(userId).SendAsync("onStringTest", message);
        }
        public string GetConnectionId()
        {
            return Context.ConnectionId;
        }
        #endregion 
    }
     
}
