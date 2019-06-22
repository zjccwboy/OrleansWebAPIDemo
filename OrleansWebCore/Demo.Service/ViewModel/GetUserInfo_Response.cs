using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Demo.Service.ViewModel
{
    /// <summary>
    /// 获取用户信息返回内容
    /// </summary>
    public class GetUserInfo_Response
    {
        /// <summary>
        /// 用户 Id
        /// </summary>
        public long UserId { get; set; }
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 昵称
        /// </summary>
        public string NikeName { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
        public bool Sex { get; set; }
    }
}
