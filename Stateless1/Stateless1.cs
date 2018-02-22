using System.Collections.Generic;
using System.Fabric;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.ServiceFabric.Services.Communication.Runtime;
using Microsoft.ServiceFabric.Services.Remoting.Runtime;
using Microsoft.ServiceFabric.Services.Runtime;

namespace Stateless1
{
    internal sealed class Stateless1 : StatelessService, IStartableAndStoppableService
    {
        private EndpointCommunicationListener communicationListener;

        public Stateless1(StatelessServiceContext context)
            : base(context)
        { }

        protected override IEnumerable<ServiceInstanceListener> CreateServiceInstanceListeners()
        {
            var remotingListeners = this.CreateServiceRemotingInstanceListeners();
            communicationListener = new EndpointCommunicationListener();
            return remotingListeners.Union(new[] {new ServiceInstanceListener(c => communicationListener)});
        }

        public Task Start()
        {
            return communicationListener.Start();
        }

        public Task Stop()
        {
            return communicationListener.Stop();
        }
    }
}
