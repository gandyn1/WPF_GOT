using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messages
{
    public interface IMessage<T>
    {
        string Message { get; }
        T Data { get; }
    }

    public interface IMessage : IMessage<object>
    {
 
    }

}
