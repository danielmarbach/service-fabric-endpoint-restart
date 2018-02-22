using System;
using System.Threading.Tasks;
using Messages;
using NServiceBus;

namespace Connector
{
    class ReplyHandler : IHandleMessages<MyReply>
    {
        public Task Handle(MyReply message, IMessageHandlerContext context)
        {
            Console.WriteLine("Reply received");
            return Task.CompletedTask;
        }
    }
}