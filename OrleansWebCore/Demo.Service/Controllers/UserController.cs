﻿using System;
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

        /// <summary>
        /// 获取用户名
        /// </summary>
        /// <param name="userId">用户Id</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<string>> GetUserName(long userId)
        {
            var service = this.OrleansClient.GetGrain<IUser>(userId);
            await service.GetUserName(userId);
            return await service.GetUserName(userId);
        }

        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <param name="request">请求信息</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<GetUserInfo_Response>> GetUserInfo(GetUserInfo_Request request)
        {
            var service = this.OrleansClient.GetGrain<IUser>(request.UserId);
            var businessInfo = await service.GetUserInfo(new GetUserInfo_Input { UserId = request.UserId});
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