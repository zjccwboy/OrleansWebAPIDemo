using Demo.Business;
using Demo.IBusiness;
using OrleansWebCore;
using OrleansWebCore.Configuration;
using System;

namespace Demo.DashboardService
{
    class Program
    {
        static void Main(string[] args)
        {
            //var config = new DashboardConfiguration
            //{
            //    Username = "admin",
            //    Password = "123456",
            //    Port = 6666,
            //    UpdateIntervalMs = 2000,
            //    ClusterId = "dev",
            //    ServiceId = "DemoService",
            //    SiloPort = 20001,
            //    GatewayPort = 30002,
            //    OrleansStorage = "OrleansStorage",
            //    ClusterDatabase = new DatabaseConfiguration
            //    {
            //        DataSource = "127.0.0.1",
            //        InitialCatalog = "OrleansWebCore",
            //        User = "sa",
            //        Password = "123456",
            //    },
            //};
            //Console.Write(Newtonsoft.Json.JsonConvert.SerializeObject(config, Newtonsoft.Json.Formatting.Indented));


            var config = OrleansConfigReader.ReadDashboardConfig();
            OrleansDashboardStartup.StartDashboardServer(config, new Type[] { typeof(UserServer) });
            OrleansDashboardStartup.StartOrleansClient(config, new Type[] { typeof(IUser) });

            Console.Read();
            Console.WriteLine("Hello World!");
        }
    }
}
