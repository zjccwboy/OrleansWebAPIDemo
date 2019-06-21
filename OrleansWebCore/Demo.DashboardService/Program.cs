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
            var config = OrleansConfigReader.ReadDashboardConfig();
            OrleansSiloStartup.StartDashboardServer(config);

            Console.Read();
        }
    }
}
