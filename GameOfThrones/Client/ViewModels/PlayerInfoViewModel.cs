using GameOfThronesCoreLibrary.Messages;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.ViewModels
{
    public class PlayerInfoViewModel : BaseViewModel
    {
        public ObservableCollection<MessagePlayerInfo> PlayerInfos { get; set; }

        public PlayerInfoViewModel(MainWindow main)
            : base(main)
        {
            PlayerInfos = new ObservableCollection<MessagePlayerInfo>();

            main.client.Subscribe<MessagePlayerInfos>(ReceivedMessagePlayerInfo);
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
