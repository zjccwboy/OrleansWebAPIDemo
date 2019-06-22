using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Demo.Service.ViewModel
{
    /// <summary>
    /// 获取用户信息请求内容
    /// </summary>
    public class GetUserInfo_Request
    {
        /// <summary>
        /// 用户Id
        /// </summary>
        public long UserId { get; set; }
    }
}
