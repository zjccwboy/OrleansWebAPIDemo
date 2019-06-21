using System;
using System.Collections.Generic;
using System.Text;

namespace OrleansWebCore.Configuration
{
    /// <summary>
    /// Orleans宿主集群配置
    /// </summary>
    public class OrleansSoliHostConfiguration
    {
        /// <summary>
        /// 集群Id
        /// </summary>
        public string ClusterId { get; set; }

        /// <summary>
        /// Orleans服务Id
        /// </summary>
        public string ServiceId { get; set; }

        /// <summary>
        /// 本地服务宿主端口
        /// </summary>
        public int SiloPort { get; set; }

        /// <summary>
        /// 网关端口
        /// </summary>
        public int GatewayPort { get; set; }

        /// <summary>
        /// Orleans仓库名
        /// </summary>
        public string OrleansStorage { get; set; }

        /// <summary>
        /// 集群数据库配置
        /// </summary>
        public DatabaseConfiguration ClusterDatabase { get; set; }
    }
}
