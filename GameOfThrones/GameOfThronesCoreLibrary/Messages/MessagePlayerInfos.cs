using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfThronesCoreLibrary.Messages
{
    [Serializable]
    public class MessagePlayerInfos : IMessage
    {
        public string Message
        {
            get
            {
                return "Players in the room status";
            }
        }

        public List<MessagePlayerInfo> Players = new List<MessagePlayerInfo>();

    }
}
