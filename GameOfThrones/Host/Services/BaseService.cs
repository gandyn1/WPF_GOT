using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameOfThrones.Common.Messages;

namespace GameOfThrones.Host.Services
{
    public abstract class BaseService<TMessage> : GameOfThrones.Common.MessageListener<Host, TMessage> where TMessage : IMessage
    {
        public override void Subscribe<T>(Action<object, object> action)
        {
            Host.Clients.Subscribe <TMessage>(action);
        }

        public override abstract void MessageReceivedHandler(MyTcpClient client, TMessage msg);

        public abstract bool NotifyClient(MyTcpClient client);
    }
}
