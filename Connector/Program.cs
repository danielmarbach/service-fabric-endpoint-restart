using System;
using System.Threading.Tasks;
using Messages;
using Microsoft.ServiceFabric.Services.Remoting.Client;
using NServiceBus;
using Stateless1;

namespace Connector
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var endpointConfiguration = new EndpointConfiguration("sender-endpoint");

            var transportExtensions = endpointConfiguration.UseTransport<LearningTransport>();
            transportExtensions.StorageDirectory(@"C:\storage");

            var session = await Endpoint.Start(endpointConfiguration);

            var proxy = ServiceProxy.Create<IStartableAndStoppableService>(new Uri("fabric:/Application1/Stateless1"));
            await proxy.Start();

            Console.WriteLine("Started!");

            await session.Send("restartable-endpoint", new MyCommand());

            await session.Send("restartable-endpoint", new MyCommand());

            Console.WriteLine("Hit Any key to stop!");
            Console.ReadLine();
            await proxy.Stop();

            Console.WriteLine("Stopped!");

            await session.Send("restartable-endpoint", new MyCommand());
            await session.Send("restartable-endpoint", new MyCommand());
            
            Console.WriteLine("Hit Any key to start!");
            Console.ReadLine();
            await proxy.Start();

            Console.WriteLine("Started!");

            Console.ReadLine();

            await session.Stop();
        }
    }
}
