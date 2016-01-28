using GameOfThronesCoreLibrary.Messages;
using Host.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TracerX;

namespace Host
{
    public class Host
    {
        public static HostTcpListner Clients;        
        public static ServiceChat ServiceChat;
        public static ServicePlayerInfo ServicePlayerInfo;
        public static ServiceGamePieceManager ServiceGamePieceManager;
        private static readonly Logger Logger = Logger.GetLogger("Host");

        static void Main()
        {
            Thread.CurrentThread.Name = "Main Thread";
            Logger.DefaultBinaryFile.MaxSizeMb = 10;
            Logger.DefaultBinaryFile.CircularStartSizeKb = 1;
            Logger.DefaultBinaryFile.Open();

            Clients = new HostTcpListner("192.168.1.167", 420);
            ServiceChat = new ServiceChat();
            ServicePlayerInfo = new ServicePlayerInfo();
            ServiceGamePieceManager = new ServiceGamePieceManager();

            while (true)
            {

            }
        }
        
    }
}
