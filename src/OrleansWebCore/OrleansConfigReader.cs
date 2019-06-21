using OrleansWebCore.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace OrleansWebCore
{
    /// <summary>
    /// Orleans宿主配置
    /// </summary>
    public class OrleansConfigReader
    {
        /// <summary>
        /// 读取Silo集群配置
        /// </summary>
        /// <returns></returns>
        public static OrleansSoliHostConfiguration ReadSiloConfig()
        {
            var configJson = File.ReadAllText("OrleansSoliConfig.json");
            return Newtonsoft.Json.JsonConvert.DeserializeObject<OrleansSoliHostConfiguration>(configJson);
        }

        /// <summary>
        /// 读取Dashboard集群配置
        /// </summary>
        /// <returns></returns>
        public static DashboardConfiguration ReadDashboardConfig()
        {
            var configJson = File.ReadAllText("OrleansDashboardConfig.json");
            return Newtonsoft.Json.JsonConvert.DeserializeObject<DashboardConfiguration>(configJson);
        }
    }
}
