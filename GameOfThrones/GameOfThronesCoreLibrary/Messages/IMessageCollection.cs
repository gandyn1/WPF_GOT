using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfThronesCoreLibrary.Messages
{
    interface IMessageCollection<TMessage> where TMessage : IMessage
    {
        List<TMessage> Messages { get; set; }
    }
}
