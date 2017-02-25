using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameOfThrones.Common.Enums;

namespace GameOfThrones.Common.Messages
{
    [Serializable]
    public class MessageGamePieceInfo : IMessageAboutPlayer, IUnique 
    {
        private Guid _Key = Guid.NewGuid();
        string IUnique.Key
        {
            get { return _Key.ToString();  }
        }

        public MessageGamePieceInfo(string playerKey)
        {
            _PlayerKey = playerKey;
        }

        private string _PlayerKey;
        public string PlayerKey { get { return _PlayerKey; } }

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


        
    }
}
