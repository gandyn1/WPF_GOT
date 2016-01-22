using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfThronesCoreLibrary.Messages
{
    [Serializable]
    public class MessagePieceMove : IMessage
    {
        public double PosX;
        public double PosY;

        public string Message
        {
            get
            {
                return "Updating a piece location";
            }
        }
    }
}
