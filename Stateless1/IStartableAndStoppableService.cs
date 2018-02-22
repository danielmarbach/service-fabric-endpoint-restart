using System.Threading.Tasks;
using Microsoft.ServiceFabric.Services.Remoting;

namespace Stateless1
{
    public interface IStartableAndStoppableService : IService
    {
        Task Start();
        Task Stop();
    }
}