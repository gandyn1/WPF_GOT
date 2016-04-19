using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameOfThronesCoreLibrary.Enums;

namespace GameOfThronesCoreLibrary.Messages
{
    [Serializable]
    public class MessageGamePieceInfo : IMessage
    {
        public MessageGamePieceInfo(MessagePlayerInfo player)
        {
            this.Player = player;
        }

        public MessagePlayerInfo Player;

        public enum Actions { Insert, Update, Delete, Reset }

        public Actions Action = Actions.Insert;
        public Guid Key = Guid.NewGuid();
        public double PosX;
        public double PosY;

        public GamePieceType PieceType;                

        public string Message
        {
            get
            {
                return "Updating a piece location";
            }
        }

        public override string ToString()
        {
            return Player.ToString();
        }
    }
}
