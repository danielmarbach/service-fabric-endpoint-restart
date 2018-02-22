using System.Threading.Tasks;
using Messages;
using NServiceBus;

namespace Stateless1
{
    public class Handler : IHandleMessages<MyCommand> {
        public Task Handle(MyCommand message, IMessageHandlerContext context)
        {
            return context.Reply(new MyReply());
        }
    }
}