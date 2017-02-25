using GameOfThrones.Common.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfThrones.Host.Services
{
    public class ServiceChat : BaseService<MessageChat>
    {
        public override void MessageReceivedHandler(MyTcpClient client, MessageChat msg)
        {
            SendChatMessage(client, msg);
        }

        public void SendChatMessage(string text)
        {
            SendChatMessage("Host", text);
        }
        public void SendChatMessage(object sender, object obj)
        {
            var player = Host.ServicePlayerInfo.PlayerInfoLookup[(MyTcpClient)sender];
            MessageChat chat = (MessageChat)obj;
            SendChatMessage(player.Name, chat.text);
        }
        public void SendChatMessage(MyTcpClient client, string text)
        {
            MessageChat msg = new MessageChat();
            msg.text = text;
            client.SendMessage(msg);
        }
        public void SendChatMessage(string senderName, string text)
        {
            MessageChat msg = new MessageChat();
            msg.text = String.Format("{0} {2}: {1}", senderName, text, DateTime.Now.ToShortTimeString());
            Host.Clients.Broadcast(msg);            
        }

        public override bool NotifyClient(MyTcpClient client)
        {
            //Does not apply
            return false;
        }
    }
}
