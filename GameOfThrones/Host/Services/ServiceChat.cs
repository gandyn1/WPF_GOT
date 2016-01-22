using GameOfThronesCoreLibrary.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Host.Services
{
    public class ServiceChat
    {
        public ServiceChat()
        {
            Host.Clients.Subscribe<MessageChat>(MessageChatHandler);
        }

        public void MessageChatHandler(object sender, object obj)
        {
            SendChatMessage(sender, obj);
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

        public void SendChatMessage(string senderName, string text)
        {
            MessageChat msg = new MessageChat();
            msg.text = String.Format("{0} {2}: {1}", senderName, text, DateTime.Now.ToShortTimeString());
            Host.Clients.Broadcast(msg);
        }

    }
}
