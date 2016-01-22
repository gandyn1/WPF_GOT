using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks; 

namespace GameOfThronesCoreLibrary.Messages
{
    [Serializable]
    public class MessagePlayerInfo : IMessage
    {
        private Guid _PlayerKey = Guid.NewGuid();
        private Guid PlayerKey { get { return _PlayerKey; } }

        public string Name { get; set; }

        public bool isActive = true;

        public string Message { get { return "PlayerInfo"; } }
    }
}
