using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfThronesCoreLibrary.Messages
{
    [Serializable]
    public class MessageGameBoardUpdate : IMessage
    {
        public List<MessageGamePieceInfo> MessageGamePieceInfos = new List<Messages.MessageGamePieceInfo>();

        public string Message
        {
            get
            {
               return "Update Game Board";
            }
        }
    }
}
