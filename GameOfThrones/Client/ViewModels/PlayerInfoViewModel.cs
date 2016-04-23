using GameOfThronesCoreLibrary.Messages;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.ViewModels
{
    public class PlayerInfoViewModel : BaseViewModel<MessagePlayerInfos>
    {
        public ObservableCollection<MessagePlayerInfo> PlayerInfos { get; set; }

        public PlayerInfoViewModel(MainWindow main)
            : base(main)
        {
            PlayerInfos = new ObservableCollection<MessagePlayerInfo>();
        }

        public override void MessageReceivedHandler(MyTcpClient client, MessagePlayerInfos msg)
        {
            App.Current.Dispatcher.Invoke(() =>
            {
                PlayerInfos.Clear();
                foreach (var p in msg.Players)
                    PlayerInfos.Add(p);
            });   
        }
    }
}
