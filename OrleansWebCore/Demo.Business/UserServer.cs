using System;
using System.Threading.Tasks;
using Demo.DataTranferObject;
using Demo.IBusiness;
using Orleans;
using Orleans.Providers;
using Demo.Repository;
using System.Threading;
using Microsoft.Extensions.Logging;

namespace Demo.Business
{
    [StorageProvider(ProviderName = "OrleansStorage")]
    public class UserServer : Grain, IUser
    {
        public async Task<string> GetUserName(long userId)
        {
            Console.WriteLine($"GetUserName-> UserId:{userId} ThreadId:{Thread.CurrentThread.ManagedThreadId}");
            var repository = new UserRepository();
            var userInfo = await repository.GetUserInfo(userId);
            return userInfo.UserName;
        }

        public async Task<GetUserInfo_Output> GetUserInfo(GetUserInfo_Input input)
        {
            Console.WriteLine($"GetUserInfo-> Input:{Newtonsoft.Json.JsonConvert.SerializeObject(input)} ThreadId:{Thread.CurrentThread.ManagedThreadId}");
            var repository = new UserRepository();
            var userInfo = await repository.GetUserInfo(input.UserId);
            return new GetUserInfo_Output
            {
                UserId = userInfo.UserId,
                UserName = userInfo.UserName,
                NikeName = userInfo.NikeName,
                Sex = userInfo.Sex,
            };
        }
    }
}
