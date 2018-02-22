using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.ServiceFabric.Services.Communication.Runtime;
using NServiceBus;
using NServiceBus.Logging;

namespace Stateless1
{
    class EndpointCommunicationListener : ICommunicationListener
    {
        private EndpointConfiguration configuration;
        private IEndpointInstance endpointInstance;

        public Task<string> OpenAsync(CancellationToken cancellationToken)
        {
            return Task.FromResult(default(string));
        }

        public Task CloseAsync(CancellationToken cancellationToken)
        {
            return Stop();
        }

        public void Abort()
        {
        }

        public async Task Start()
        {
            configuration = CreateConfiguration();
            try
            {
                endpointInstance = await Endpoint.Start(configuration);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public async Task Stop()
        {
            await endpointInstance.Stop();
            configuration = null;
        }

        EndpointConfiguration CreateConfiguration()
        {
            var endpointConfiguration = new EndpointConfiguration("restartable-endpoint");

            endpointConfiguration.SetDiagnosticsPath(@"C:\storage");

            var defaultFactory = LogManager.Use<DefaultFactory>();
            defaultFactory.Level(LogLevel.Debug);
            defaultFactory.Directory(@"C:\storage");

            var transportExtensions = endpointConfiguration.UseTransport<LearningTransport>();
            transportExtensions.StorageDirectory(@"C:\storage");

            return endpointConfiguration;
        }
    }
}