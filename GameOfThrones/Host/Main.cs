using GameOfThronesCoreLibrary.Messages;
using Host.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TracerX;
using GameOfThronesCoreLibrary;

namespace Host
{
    public class Host
    {
        public static HostTcpListner Clients;        
        public static ServiceChat ServiceChat;
        public static ServicePlayerInfo ServicePlayerInfo;
        public static ServiceGamePieceManager ServiceGamePieceManager;
        public static MessageManager MessageManager; 
        private static readonly Logger Logger = Logger.GetLogger("Host");

        public Host(string ip, int port)
        {
            Thread.CurrentThread.Name = "Main Thread";
            Logger.DefaultBinaryFile.MaxSizeMb = 10;
            Logger.DefaultBinaryFile.CircularStartSizeKb = 1;
            Logger.DefaultBinaryFile.Open();

            Clients = new HostTcpListner(ip, port);

            //MessageManager = new MessageManager();
            //Clients.DataReceived += MessageManager.ListenHandler;

            ServiceChat = new ServiceChat();
            ServicePlayerInfo = new ServicePlayerInfo();
            ServiceGamePieceManager = new ServiceGamePieceManager();                                 
        }
        
    }
}
