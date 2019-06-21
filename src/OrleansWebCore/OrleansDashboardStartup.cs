using Microsoft.Extensions.DependencyInjection;
using Orleans;
using Orleans.Configuration;
using Orleans.Hosting;
using OrleansWebCore.Configuration;
using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using System.Net;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore;
using Orleans.Serialization;
using System.Reflection;

namespace OrleansWebCore
{
    /// <summary>
    /// Orleans Dashboard宿主启动管理类
    /// </summary>
    public class OrleansDashboardStartup
    {
        private const string Invariant = "System.Data.SqlClient";

        /// <summary>
        /// 启动Orleans仪表盘服务
        /// </summary>
        /// <param name="configure"></param>
        /// <param name="applicationPartTypes"></param>
        /// <returns></returns>
        public static ISiloHost StartDashboardServer(DashboardConfiguration configure, Type[] applicationPartTypes)
        {
            var gatewayPort = configure.GatewayPort;
            var siloPort = configure.SiloPort;

            var builder = new SiloHostBuilder()
            // Grain State
            .AddAdoNetGrainStorage(configure.OrleansStorage, options =>
            {
                options.Invariant = Invariant;
                options.ConnectionString = configure.ClusterDatabase.ConnectionString;
                options.UseJsonFormat = true;
            })
            // Membership
            .UseAdoNetClustering(options =>
            {
                options.Invariant = Invariant;
                options.ConnectionString = configure.ClusterDatabase.ConnectionString;
            })
            .UseDashboard(options =>
            {
                options.Username = configure.Username;
                options.Password = configure.Password;
                options.HostSelf = true;
                options.HideTrace = false;
                options.Port = configure.Port;
                options.CounterUpdateIntervalMs = configure.UpdateIntervalMs;
            })
            .Configure<ClusterOptions>(options =>
            {
                options.ClusterId = configure.ClusterId;
                options.ServiceId = configure.ServiceId;
            })
            .Configure<SerializationProviderOptions>(options =>
            {
                options.SerializationProviders.Add(typeof(ProtobufSerializer).GetTypeInfo());
                options.FallbackSerializationProvider = typeof(ProtobufSerializer).GetTypeInfo();
            })
            .ConfigureApplicationParts(parts =>
            {
                if (applicationPartTypes != null)
                {
                    foreach (var type in applicationPartTypes)
                    {
                        parts.AddApplicationPart(type.Assembly);
                    }
                }
            })
            .ConfigureEndpoints(siloPort, gatewayPort)
            .ConfigureLogging(log => log.SetMinimumLevel(LogLevel.Warning).AddConsole());

            var host = builder.Build();
            host.StartAsync().Wait();

            Console.WriteLine("Orleans仪表盘已经启动");
            return host;
        }

        /// <summary>
        /// 启动Orleans客户端服务
        /// </summary>
        /// <param name="configure"></param>
        /// <param name="applicationPartTypes"></param>
        /// <returns></returns>
        public static IClusterClient StartOrleansClient(DashboardConfiguration configure, Type[] applicationPartTypes)
        {
            var client = new ClientBuilder()
             .UseAdoNetClustering(options =>
             {
                 options.Invariant = Invariant;
                 options.ConnectionString = configure.ClusterDatabase.ConnectionString;
             })
             .Configure<ClusterOptions>(options =>
             {
                 options.ClusterId = configure.ClusterId;
                 options.ServiceId = configure.ServiceId;
             })
            .Configure<SerializationProviderOptions>(options =>
            {
                options.SerializationProviders.Add(typeof(ProtobufSerializer).GetTypeInfo());
                options.FallbackSerializationProvider = typeof(ProtobufSerializer).GetTypeInfo();
            })
            .ConfigureApplicationParts(parts =>
            {
                if (applicationPartTypes != null)
                {
                    foreach (var type in applicationPartTypes)
                    {
                        parts.AddApplicationPart(type.Assembly);
                    }
                }
            })
             .ConfigureLogging(log => log.SetMinimumLevel(LogLevel.Warning).AddConsole())
             .Build();

            client.Connect(async (e) =>
            {
                Console.WriteLine($"Orleans客户端连接集群错误.  Exception: {e.ToString()}");
                await Task.Delay(TimeSpan.FromSeconds(3));
                Console.WriteLine($"Orleans客户端开始重新连接到集群.");
                return true;
            }).Wait();

            Console.WriteLine("Orleans客户端已经启动");
            return client;
        }
    }
}
