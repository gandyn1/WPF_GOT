using GameOfThronesCoreLibrary.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Extensions
{
    public static class ExtendMessageAboutPlayer
   {
        public static MessagePlayerInfo PlayerInfo(this IMessageAboutPlayer msg)
        {
            return MainWindow.vmPlayerInfo.PlayerInfos.Where(o => o.PlayerKey == msg.PlayerKey).Single();
        }
    }
}
