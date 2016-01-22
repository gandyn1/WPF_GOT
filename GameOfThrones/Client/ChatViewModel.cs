using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using GameOfThronesCoreLibrary.WPF;
using GameOfThronesCoreLibrary.Messages;

namespace Client 
{
    public class ChatViewModel : INotifyPropertyChanged 
    {
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

        public event PropertyChangedEventHandler PropertyChanged;

        public ChatViewModel()
        {
            SendMessageCommand = new RelayCommand(SendMessage);
            MainWindow.client.Subscribe<MessageChat>(RecieveMessage);
        }

        private void RecieveMessage(object sender, object obj)
        {
            MessageChat chat = (MessageChat)obj;
            ReceivedText += chat.text + Environment.NewLine;
        }

        private void SendMessage(object obj)
        {
            if(EnteredText != "")
            {
                var chat = new MessageChat();

                chat.text = EnteredText;
                EnteredText = "";

                MainWindow.client.SendMessage(chat);
            }            
        }


    }
}
