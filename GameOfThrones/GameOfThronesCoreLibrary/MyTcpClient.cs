using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using GameOfThrones.Common.Messages;

 
    public class MyTcpClient
    {
        public event EventHandler<object> DataReceived;
        public event EventHandler OnLeave;

        public TcpClient client = new TcpClient();

        private string _Key = Guid.NewGuid().ToString();
        public string Key { get { return _Key; } }

        public override string ToString()
        {
            return Key;
        }

        public MyTcpClient(TcpClient client)
        {
            this.client = client;
            Task.Factory.StartNew(new Action(RecieveListener));
        }
        public MyTcpClient(string ip, int port)
        {
            client.Connect(ip, port);
            Task.Factory.StartNew(new Action(RecieveListener));
        }

        public void SendMessage(object obj)
        {
            IFormatter formatter = new BinaryFormatter();
            NetworkStream stream = client.GetStream();
            formatter.Serialize(stream, obj);           
        }

        private void RecieveListener()
        {
            try
            {
                NetworkStream strm = null;
                while (true)
                {
                    if (client.Connected)
                    {
                        strm = client.GetStream();
                        IFormatter formatter = new BinaryFormatter();
                        IMessage msg = (IMessage)formatter.Deserialize(strm);

                        if (DataReceived != null)
                            DataReceived(this, msg);

                        if (_dictionaryOfSubscribers.ContainsKey(msg.GetType()))
                        {

                            foreach (Action<object, object> action in _dictionaryOfSubscribers[msg.GetType()])
                            {
                                action.Invoke(this, msg);
                            }
                        }
                    }
                }
            }
            catch (Exception e) {
                if (OnLeave != null)
                    OnLeave(this, null);
            }                                  
        }

        private Dictionary<Type, List<object>> _dictionaryOfSubscribers = new Dictionary<Type, List<object>>();

        public void Subscribe<T>(Action<object, object> action) where T : IMessage
        {            
            Type key = typeof(T);
            if (_dictionaryOfSubscribers.ContainsKey(key))
            {
                _dictionaryOfSubscribers[key].Add(action);
            }
            else
            {
                var actions = new List<object>();
                actions.Add(action);
                _dictionaryOfSubscribers.Add(key, actions);
            }
    }
    }
 