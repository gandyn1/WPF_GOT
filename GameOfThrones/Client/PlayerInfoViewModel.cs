using GameOfThronesCoreLibrary.Messages;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    public class PlayerInfoViewModel
    {
        public ObservableCollection<MessagePlayerInfo> PlayerInfos { get; set; }

        public PlayerInfoViewModel()
        {
            PlayerInfos = new ObservableCollection<MessagePlayerInfo>();

            MainWindow.client.Subscribe<MessagePlayerInfos>(ReceivedMessagePlayerInfo);
        }

        public void ReceivedMessagePlayerInfo(object sender, object obj)
        {
            MessagePlayerInfos msg = (MessagePlayerInfos)obj;
            App.Current.Dispatcher.Invoke(() => {
                PlayerInfos.Clear();
                foreach (var p in msg.Players)
                    PlayerInfos.Add(p);
            });            
        }
    }
}
