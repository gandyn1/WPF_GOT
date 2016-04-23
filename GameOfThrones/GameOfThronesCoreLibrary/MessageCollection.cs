using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameOfThronesCoreLibrary.Messages;

namespace GameOfThronesCoreLibrary
{
    public class MessageCollection<TMessage> : Messages.IMessageCollection<TMessage> where TMessage : IMessage
    {
        private List<TMessage> _Messages = new List<TMessage>();
        public List<TMessage> Messages
        {
            get
            {
                return _Messages;
            }
            set
            {
                _Messages = value;
            }
        }
    }
}
