using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks; 
using  System.Windows.Media;

namespace GameOfThronesCoreLibrary.Messages
{
    [Serializable]
    public class MessagePlayerInfo : IMessage
    {
        private Guid _PlayerKey = Guid.NewGuid();
        public Guid PlayerKey { get { return _PlayerKey; } }

        public string Name { get; set; }

        public bool isActive = true;

        public string Message { get { return "PlayerInfo"; } }

        private string _PlayerColor = "#FFDFD99";

        public System.Windows.Media.Color PlayerColor
        {
            get
            {
                return GameOfThronesCoreLibrary.Utility.BitmapUtility.Parse(_PlayerColor);
            }
            set
            {
                _PlayerColor = value.ToString();
            }
        }
      
    }
}
