using GameOfThronesCoreLibrary.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Host.Services
{
    public class ServiceGamePieceManager
    {

        public ServiceGamePieceManager()
        {
          Host.Clients.Subscribe<MessageGamePieceInfo>(OnMessagePieceMove);                   
        }

        public void OnMessagePieceMove(object sender, object obj)
        {
            var msg = (MessageGamePieceInfo)obj;

            if (!MessageGamePieceInfo.ContainsKey(msg.Key))
                MessageGamePieceInfo.Add(msg.Key, msg);

            MessageGamePieceInfo[msg.Key] = msg;

            if (msg.Action == GameOfThronesCoreLibrary.Messages.MessageGamePieceInfo.Actions.Delete) {
                MessageGamePieceInfo.Remove(msg.Key);
            }            

            var outMsg = new MessageGameBoardUpdate();
            outMsg.MessageGamePieceInfos = MessageGamePieceInfo.Select(o => o.Value).ToList();

            Host.Clients.Broadcast(outMsg);
        }

        public Dictionary<Guid, MessageGamePieceInfo> MessageGamePieceInfo = new Dictionary<Guid, MessageGamePieceInfo>();

    }
}
