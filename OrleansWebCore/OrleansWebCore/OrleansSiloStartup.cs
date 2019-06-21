using Microsoft.Extensions.DependencyInjection;
using Orleans;
using Orleans.Configuration;
using Orleans.Hosting;
using Orleans.Serialization;
using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using OrleansWebCore.Configuration;
using System.Linq;
using Orleans.Runtime;
using System.Reflection;

namespace OrleansWebCore
{
    /// <summary>
    /// Orleans宿主启动管理类
    /// </summary>
    public class OrleansSiloStartup
    {
        private const string Invariant = "System.Data.SqlClient";

        /// <summary>
        /// 启动Orleans Server服务
        /// </summary>
        /// <param name="configure"></param>
        /// <param name="applicationPartTypes"></param>
        /// <param name="services"></param>
        /// <returns></returns>
        public static ISiloHost StartOrleansServer(OrleansSoliHostConfiguration configure, Type[] applicationPartTypes, IServiceCollection services)
        {
            var siloPort = configure.SiloPort;
            var gatewayPort = configure.GatewayPort;

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
                .ConfigureEndpoints(siloPort, gatewayPort)
                .ConfigureApplicationParts(parts =>
                {
                    if(applicationPartTypes != null)
                    {
                        foreach(var type in applicationPartTypes)
                        {
                            parts.AddApplicationPart(type.Assembly).WithReferences();
                        }
                    }
                })
                .ConfigureLogging(log => log.SetMinimumLevel(LogLevel.Warning).AddConsole());

            var host = builder.Build();
            host.StartAsync().Wait();
            services.AddSingleton(host);

            Console.WriteLine("Orleans服务端已经启动");

            return host;
        }

        /// <summary>
        /// 启动Orleans客户端服务
        /// </summary>
        /// <param name="configure"></param>
        /// <param name="applicationPartTypes"></param>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IClusterClient StartOrleansClient(OrleansSoliHostConfiguration configure, Type[] applicationPartTypes, IServiceCollection services)
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
                    if(applicationPartTypes != null)
                    {
                        foreach(var type in applicationPartTypes)
                        {
                            parts.AddApplicationPart(type.Assembly);
                        }
                    }
                })
                .ConfigureLogging(log => log.SetMinimumLevel(LogLevel.Warning).AddConsole())
                .Build();

            client.Connect(async (e)=> 
            {
                Console.WriteLine($"Orleans客户端连接集群错误.  Exception: {e.ToString()}");
                await Task.Delay(TimeSpan.FromSeconds(3));
                Console.WriteLine($"Orleans客户端开始重新连接到集群.");
                return true;
            }).Wait();

            Console.WriteLine("Orleans客户端已经启动");

            services.AddSingleton(client);

            return client;
        }
    }
}
