using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Demo.Service;
using Demo.Business;
using Demo.IBusiness;
using OrleansWebCore;
using Orleans;
using Demo.Service.ViewModel;
using Demo.DataTranferObject;

namespace Demo.Service.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IClusterClient OrleansClient { get; }

        public UserController(IClusterClient client)
        {
            this.OrleansClient = client;
        }

        [HttpGet]
        public async Task<ActionResult<string>> GetUserName(long userId)
        {
            var service = this.OrleansClient.GetGrain<IUser>(userId);
            return await service.GetUserName(userId);
        }

        [HttpPost]
        public async Task<ActionResult<GetUserInfo_Response>> GetUserInfo(GetUserInfo_Request request)
        {
            var service = this.OrleansClient.GetGrain<IUser>(request.UserId);
            var businessInfo = await service.GetUserInfo(new GetUserInfo_In { UserId = request.UserId});
            return new GetUserInfo_Response
            {
                UserId = businessInfo.UserId,
                UserName = businessInfo.UserName,
                NikeName = businessInfo.NikeName,
                Sex = businessInfo.Sex,
            };
        }
    }
}