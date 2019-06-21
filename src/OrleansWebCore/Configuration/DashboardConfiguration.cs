using System;
using System.Collections.Generic;
using System.Text;

namespace OrleansWebCore.Configuration
{
    /// <summary>
    /// 仪表盘配置项
    /// </summary>
    public class DashboardConfiguration : OrleansSoliHostConfiguration
    {

        /// <summary>
        /// 用户名
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// 端口
        /// </summary>
        public int Port { get; set; }

        /// <summary>
        /// 刷新频率毫秒
        /// </summary>
        public int UpdateIntervalMs { get; set; }
    }
}
