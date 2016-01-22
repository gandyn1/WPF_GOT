using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using GameOfThronesCoreLibrary;
using GameOfThronesCoreLibrary.Messages;

namespace Client
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static MyTcpClient client;
        public static ChatViewModel vmChat;
        public static MessagePlayerInfo myInfo;
        public static PlayerInfoViewModel vmPlayerInfo;

        public MainWindow()
        {            
            //Init View Models
            client = new MyTcpClient("192.168.1.167", 420);
            vmChat = new ChatViewModel();            
            vmPlayerInfo = new PlayerInfoViewModel();
            //Init UI
            InitializeComponent();
            //Init DataContext
            gvChat.DataContext = vmChat;
            ucPlayers.DataContext = vmPlayerInfo;
            //Send Message To Host about our player info
            myInfo = new MessagePlayerInfo();
            myInfo.Name = "Nick";
            client.SendMessage(myInfo);
        }


        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                txtEnteredText.GetBindingExpression(TextBox.TextProperty).UpdateSource();
                vmChat.SendMessageCommand.Execute(null);
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            client.client.Close();
        }
    }
}
