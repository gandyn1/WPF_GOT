using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace Messages
{
    public class MyTcpClient
    {
        public event EventHandler<IMessage<object>> DataReceived;

        private TcpClient _client = new TcpClient();

        public MyTcpClient(TcpClient client)
        {
            _client = client;
            Task.Factory.StartNew(new Action(RecieveListener));
        }
        public MyTcpClient(string ip, int port)
        {
            _client.Connect(ip, port);
            Task.Factory.StartNew(new Action(RecieveListener));
        }

        public void SendMessage(IMessage<object> obj)
        {
            IFormatter formatter = new BinaryFormatter();
            NetworkStream stream = _client.GetStream();
            formatter.Serialize(stream, obj);
            stream.Close();
        }

        private void RecieveListener()
        {
            NetworkStream strm = null;
            while (true)
            {
                strm = _client.GetStream();
                IFormatter formatter = new BinaryFormatter();
                IMessage<object> msg = (IMessage<object>)formatter.Deserialize(strm);
                DataReceived(this, msg);

                if (_dictionaryOfSubscribers.ContainsKey(msg.GetType()))
                {
                    foreach(Action<IMessage<object>> action in _dictionaryOfSubscribers[msg.GetType()])
                    {
                        action.Invoke(msg);
                    }
                }

            }                        
        }

        private Dictionary<Type, List<Action<IMessage<object>>>> _dictionaryOfSubscribers = new Dictionary<Type, List<Action<IMessage<object>>>>();

        public void Subscribe<T>(Action<object> action) where T : IMessage<object>
        {            
            Type key = typeof(T);
            if (_dictionaryOfSubscribers.ContainsKey(key))
            {
                _dictionaryOfSubscribers[key].Add(action);
            }
        }
    }
}
