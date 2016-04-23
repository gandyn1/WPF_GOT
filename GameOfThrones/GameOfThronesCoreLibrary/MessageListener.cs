using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameOfThronesCoreLibrary.Messages;

namespace GameOfThronesCoreLibrary
{
    public abstract class MessageListener<T, TMessage> where TMessage : IMessage
    {
        private T _Instance;
        public T Instance { get { return _Instance; } }

        public MessageListener()
        {
            Subscribe<TMessage>(MessageReceivedHandlerHelper);
        }  

        public MessageListener(T instance)
        {
            _Instance = instance;
            Subscribe<TMessage>(MessageReceivedHandlerHelper);
        }        

        private void MessageReceivedHandlerHelper(object sender, object obj)
        {
            MessageReceivedHandler((MyTcpClient)sender, (TMessage)obj);
        }

        public abstract void Subscribe<T>(Action<object, object> action) where T : IMessage;

        public abstract void MessageReceivedHandler(MyTcpClient client, TMessage msg);       
    }
}
