using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Host.Services
{
    public abstract class BaseService<TMessage> where TMessage : IMessage
    {
        public BaseService(){
            Host.Clients.Subscribe<TMessage>(MessageReceivedHandlerHelper);
        }

        private void MessageReceivedHandlerHelper(object sender, object obj)
        {
            MessageReceivedHandler((MyTcpClient)sender, (TMessage)obj);
        }

        public abstract void MessageReceivedHandler(MyTcpClient client, TMessage msg);

        public abstract bool NotifyClient(MyTcpClient client);

    }
}
