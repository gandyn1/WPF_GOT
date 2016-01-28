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
using System.Threading;
using System.Windows.Media.Animation;
using GameOfThronesCoreLibrary.Enums;

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
        public static CollisionDetection CollectionDetection;

        public MainWindow()
        {            
            //Init View Models
            client = new MyTcpClient("192.168.1.167", 420);
            vmChat = new ChatViewModel();            
            vmPlayerInfo = new PlayerInfoViewModel();
            //Init UI
            InitializeComponent();
            CollectionDetection = new CollisionDetection(ucBoard);
            //Init DataContext
            gvChat.DataContext = vmChat;
            //ucPlayers.DataContext = vmPlayerInfo;
            //Send Message To Host about our player info
            myInfo = new MessagePlayerInfo();
            myInfo.Name = "Nick";
            client.SendMessage(myInfo);

            ucBoard.Drop += ucBoard_Drop;
            AddPieceToToolbox(GamePieceType.Pawn);
            AddPieceToToolbox(GamePieceType.Knight );
            AddPieceToToolbox(GamePieceType.Engine);
            AddPieceToToolbox(GamePieceType.Ship);


            client.Subscribe<MessageGameBoardUpdate>(HostGameBoardUpdate);
        }

        private void AddPieceToToolbox(GamePieceType t)
        {
            var uc = new UserControls.ucGamePiece();
            uc.GamePieceType = t;
            uc.Width = 50;
            uc.Height = 50;

            ucToolbox.Children.Add(uc);
        }


        private void Image_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            //DragDrop.DoDragDrop((DependencyObject)sender, sender, DragDropEffects.Copy);
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

        Nullable<Point> dragStart = null;

        private void GamePieceDown_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var element = (UserControls.ucGamePiece)sender;
            dragStart = e.GetPosition(element);
            element.CaptureMouse();
            BringToFront(element);
        }

        private void GamePiece_MouseMove(object sender, MouseEventArgs e)
        {
            if (dragStart != null && e.LeftButton == MouseButtonState.Pressed)
            {
                var element = (UserControls.ucGamePiece)sender;
                var p2 = e.GetPosition(ucBoard);
                Canvas.SetLeft(element, p2.X - dragStart.Value.X -10);
                Canvas.SetTop(element, p2.Y - dragStart.Value.Y-10);
            }
        }

        private void GamePiece_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (dragStart != null)
            {
                var element = (UserControls.ucGamePiece)sender;

                var dragEnd = e.GetPosition(ucBoard);
               
                MessageGamePieceInfo msg = new MessageGamePieceInfo();
                msg.Key = GamePieces.FirstOrDefault(o => o.Value == element).Key;
                msg.PosX = dragEnd.X - dragStart.Value.X-10;
                msg.PosY = dragEnd.Y - dragStart.Value.Y-10;

                if (msg.PosX < 0 || msg.PosY < 0)
                    msg.Action = MessageGamePieceInfo.Actions.Delete;

                client.SendMessage(msg);

                element.ReleaseMouseCapture();                
                dragStart = null;
            }
        }
 
            public static void BringToFront(UserControl element)
            {
                if (element == null) return;

                Canvas canvas = element.Parent as Canvas;
                if (canvas == null) return;

                var target = canvas.Children.OfType<UIElement>()
                  .Where(x => x != element)
                  .Select(x => Panel.GetZIndex(x));

                var max = 0;

                if (target.Count() > 0)
                    max = target.Max();

                Canvas.SetZIndex(element, max + 1);
            }
 
        private void ucBoard_Drop(object sender, DragEventArgs e)
        { 
            if (e.Handled == false)
            {
                var canvas = (Canvas)sender;
                var element = (UserControls.ucGamePiece)e.Data.GetData("ucGamePiece");

                if (canvas != null && element != null)
                {
                    Panel _parent = (Panel)VisualTreeHelper.GetParent(element);

                    if (e.AllowedEffects.HasFlag(DragDropEffects.Copy) && e.Effects != DragDropEffects.None)
                    {
                            e.Effects = DragDropEffects.None;

                            HandleGamePieceEvents(element);
                            BringToFront(element);

                            MessageGamePieceInfo msg = new MessageGamePieceInfo();
                            GamePieces.Add(msg.Key, element);                            
                            msg.PosX = e.GetPosition(canvas).X - 25;
                            msg.PosY = e.GetPosition(canvas).Y - 25;
                            msg.PieceType = element.GamePieceType;

                            client.SendMessage(msg);                                       
                    }
                }
            }
        }

        public void HandleGamePieceEvents(UserControls.ucGamePiece uc)
        {
            uc.MouseMove -= GamePiece_MouseMove;
            uc.MouseMove += GamePiece_MouseMove;

            uc.MouseDown -= GamePieceDown_MouseDown;
            uc.MouseDown += GamePieceDown_MouseDown;

            uc.MouseUp -= GamePiece_MouseUp;
            uc.MouseUp += GamePiece_MouseUp;
        }

        public void HostGameBoardUpdate(object sender, object obj)
        {
            var msg = (MessageGameBoardUpdate)obj;

            foreach(var m in msg.MessageGamePieceInfos)
            {
                HostGamePieceUpdate(m);
            }

            foreach(var ctl in GamePieces.Where(c => msg.MessageGamePieceInfos.Where(h=> h.Key == c.Key).Count() == 0).ToList())
            {
                GamePieces.Remove(ctl.Key);
                App.Current.Dispatcher.Invoke(() =>
                {
                    ucBoard.Children.Remove(ctl.Value);
                });
            }            
        }

        public void HostGamePieceUpdate(MessageGamePieceInfo msg)
        {
            if (!GamePieces.ContainsKey(msg.Key))
            {
                App.Current.Dispatcher.Invoke(() =>
                {
                    var uc = new UserControls.ucGamePiece();
                    uc.GamePieceType = msg.PieceType;
                    GamePieces.Add(msg.Key, uc);
                });
            }

            UserControls.ucGamePiece element = GamePieces[msg.Key];

            App.Current.Dispatcher.Invoke(() =>
            {
                HandleGamePieceEvents(element);
                ucBoard.Children.Remove(element);
                ucBoard.Children.Add(element);
                Canvas.SetLeft(element, msg.PosX);
                Canvas.SetTop(element, msg.PosY);
            });
        }

        public Dictionary<Guid, UserControls.ucGamePiece> GamePieces = new Dictionary<Guid, UserControls.ucGamePiece>();

        private void ucBoard_DragOver(object sender, DragEventArgs e)
        {

        }
    }
}
