using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfThrones.Common.Messages
{
    [Serializable]
    public class MessageChat : IMessage
    {
        public string Message
        {
            get
            {
                return "Chat";
            }
        }

        public string text { get; set; }
    }
}
