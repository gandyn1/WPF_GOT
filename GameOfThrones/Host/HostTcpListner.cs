using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using GameOfThronesCoreLibrary;
using GameOfThronesCoreLibrary.Messages;
using TracerX;

namespace Host
{
    public class HostTcpListner
    {        
        public event EventHandler<object> DataReceived;
        public event EventHandler OnClientLeave;

        public List<MyTcpClient> clients;
        private TcpListener server;
        private static readonly Logger Logger = Logger.GetLogger("HostTcpListner");

        public HostTcpListner(string ip, int port)
        {            
                server = new TcpListener(IPAddress.Parse(ip), port);
                clients = new List<MyTcpClient>();
                server.Start();
                Logger.Info("Started Server");                
                Task.Factory.StartNew(new Action(AcceptTcpClients));           
        }

        private void AcceptTcpClients()
        {
            while (true)
            {
                TcpClient client = server.AcceptTcpClient();
                var c = new MyTcpClient(client);
                c.DataReceived += client_DataReceived;
                c.OnLeave += client_OnLeave;
                clients.Add(c);
            }
        }

        private void client_OnLeave(object sender, EventArgs e)
        {
            var client = (MyTcpClient)sender;
            clients.Remove(client);

            if (OnClientLeave != null)
                OnClientLeave(client, null);
        }

        private void client_DataReceived(object sender, object msg)
        {
            if(DataReceived != null)
                DataReceived(sender, msg);

            if (_dictionaryOfSubscribers.ContainsKey(msg.GetType()))
            {
                foreach (Action<object, object> action in _dictionaryOfSubscribers[msg.GetType()])
                {
                    action.Invoke(sender, msg);
                }
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

        public void Broadcast(object obj)
        {
            foreach(var c in clients)
            {
                c.SendMessage(obj);
            }
        }
    }
}
