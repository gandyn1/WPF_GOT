using GameOfThronesCoreLibrary.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Host.Services
{
    public class ServicePlayerInfo
    {
        public Dictionary<MyTcpClient, MessagePlayerInfo> PlayerInfoLookup = new Dictionary<MyTcpClient, MessagePlayerInfo>();

        public ServicePlayerInfo()
        {
            Host.Clients.Subscribe<MessagePlayerInfo>(MessagePlayerInfoHandler);
            Host.Clients.DataReceived += Listner_DataReceived;
            Host.Clients.OnClientLeave += Listner_OnClientLeave;
        }

        private void Listner_OnClientLeave(object sender, EventArgs e)
        {
            var client = (MyTcpClient)sender;
            var playerInfo = PlayerInfoLookup[client];
            playerInfo.isActive = false;

            Host.ServiceChat.SendChatMessage(playerInfo.Name + " has left the server");
            BroadcastChangeToClients();
        }

        private void Listner_DataReceived(object sender, object e)
        {
            IMessage msg = (IMessage)e;
            MyTcpClient client = (MyTcpClient)sender;
            MessagePlayerInfo info = null;
            if (PlayerInfoLookup.ContainsKey(client))
                info = PlayerInfoLookup[client];

            Console.WriteLine(String.Format("Recieved {0} from {1}", msg.Message, info != null ? info.Name : "New Player"));
        }

        private void MessagePlayerInfoHandler(object sender, object obj)
        {
            MessagePlayerInfo msg = (MessagePlayerInfo)obj;

            if (PlayerInfoLookup.ContainsKey((MyTcpClient)sender))
            {
                PlayerInfoLookup[(MyTcpClient)sender] = msg;
                BroadcastChangeToClients();
            }
            else
            {
                PlayerInfoLookup.Add((MyTcpClient)sender, msg);
                string text = msg.Name + " has joined the server";
                Console.WriteLine(text);
                Host.ServiceChat.SendChatMessage(text);
                BroadcastChangeToClients();
            }
        }

        private void BroadcastChangeToClients()
        {
            MessagePlayerInfos msg = new MessagePlayerInfos();
            msg.Players = PlayerInfoLookup.Select(o => o.Value).ToList();
            Host.Clients.Broadcast(msg);
        }

    }   
}
