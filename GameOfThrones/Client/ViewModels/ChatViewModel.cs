using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using GameOfThronesCoreLibrary.WPF;
using GameOfThronesCoreLibrary.Messages;

namespace Client.ViewModels
{
    public class ChatViewModel : BaseViewModel<MessageChat>, INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;

        private string _ReceivedText = "";
        public string ReceivedText
        {
            get
            {
                return _ReceivedText;
            }
            set
            {                
                _ReceivedText = value;
                if(PropertyChanged!=null)
                    PropertyChanged(this, new PropertyChangedEventArgs("ReceivedText"));
            }
        }

        private string _EnteredText = "";
        public string EnteredText
        {
            get
            {
                return _EnteredText;
            }
            set
            {
                _EnteredText = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("EnteredText"));
            }
        }

        public RelayCommand SendMessageCommand { get; set; }


        public ChatViewModel(MainWindow main)
            :base(main)
        {
            SendMessageCommand = new RelayCommand(SendMessage);            
        }

        private void SendMessage(object obj)
        {
            if(EnteredText != "")
            {
                var chat = new MessageChat();

                chat.text = EnteredText;
                EnteredText = "";

                Instance.client.SendMessage(chat);                
            }            
        }

        public override void MessageReceivedHandler(MyTcpClient client, MessageChat msg)
        {
            ReceivedText += msg.text + Environment.NewLine;
            Utility.SoundUtility.PlayChatReceived();
        }
    }
}
