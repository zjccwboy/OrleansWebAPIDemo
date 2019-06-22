using Demo.Business;
using Demo.IBusiness;
using Orleans;
using Orleans.Hosting;
using OrleansWebCore;
using OrleansWebCore.Configuration;
using System;

namespace Demo.DashboardService
{
    class Program
    {
        private static ISiloHost SiloHostServer { get; set; }
        private static IClusterClient ClusterClient { get; set; }

        static void Main(string[] args)
        {
            var config = OrleansConfigReader.ReadDashboardConfig();
            SiloHostServer = OrleansSiloStartup.StartDashboardServer(config, new Type[] { typeof(UserServer) });
            ClusterClient = OrleansSiloStartup.StartOrleansClient(config, new Type[] { typeof(IUser) }, null);

            Console.Read();
        }
    }
}
