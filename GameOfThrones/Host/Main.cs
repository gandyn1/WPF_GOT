using GameOfThronesCoreLibrary.Messages;
using Host.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Host
{
    public class Host
    {
        public static HostTcpListner Clients;        
        public static ServiceChat ServiceChat;
        public static ServicePlayerInfo ServicePlayerInfo;

        static void Main()
        {
            Clients = new HostTcpListner("192.168.1.167", 420);
            ServiceChat = new ServiceChat();
            ServicePlayerInfo = new ServicePlayerInfo();
           
            while (true)
            {

            }
        }

        

    }
}
