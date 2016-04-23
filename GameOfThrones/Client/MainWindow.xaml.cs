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
using Client.ViewModels;

namespace Client
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public  MyTcpClient client;
        public  ChatViewModel vmChat;
        public  MessagePlayerInfo myInfo;
        public  PlayerInfoViewModel vmPlayerInfo;
        public MessageManager MessageManager;

        public MainWindow(string ip, int port, string userName)
        {            
            //Init View Models
            client = new MyTcpClient(ip, port);

            MessageManager = new MessageManager();
            client.DataReceived += MessageManager.ListenHandler;

            vmChat = new ChatViewModel(this);            
            vmPlayerInfo = new PlayerInfoViewModel(this);
            //Init UI
            InitializeComponent();
            //Init DataContext
            gvChat.DataContext = vmChat;
            //ucPlayers.DataContext = vmPlayerInfo;
            //Send Message To Host about our player info
            myInfo = new MessagePlayerInfo();
            myInfo.Name = userName;
            ctlPlayerName.Text = userName;
            ctlPlayerColor.SelectedColor = myInfo.PlayerColor;
            client.SendMessage(myInfo);

            

            ucBoard.Drop += ucBoard_Drop;
            RefreshToolBox();


            client.Subscribe<MessageGameBoardUpdate>(HostGameBoardUpdate);
        }

        private void RefreshToolBox()
        {
            ucToolbox.Children.Clear();
            AddPieceToToolbox(GamePieceType.Pawn);
            AddPieceToToolbox(GamePieceType.Knight);
            AddPieceToToolbox(GamePieceType.Engine);
            AddPieceToToolbox(GamePieceType.Ship);
            AddPieceToToolbox(GamePieceType.Marker);
        }

        private void AddPieceToToolbox(GamePieceType t)
        {
            var uc = new UserControls.ucGamePiece();
            uc.PieceColor = myInfo.PlayerColor;
            uc.GamePieceType = t;
            uc.RefreshUI();
            uc.Width = 60;
            uc.Height = 60;

            ucToolbox.Children.Add(uc);
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

                MessageGamePieceInfo msg = new MessageGamePieceInfo(myInfo.PlayerKey);
                msg.Key = GamePieces.FirstOrDefault(o => o.Value == element).Key;
                msg.PosX = dragEnd.X - dragStart.Value.X-10;
                msg.PosY = dragEnd.Y - dragStart.Value.Y-10;


                //LOGIC TO CHECK BOUNDS OF THE CANVAS IN REGARD TO GAME PIECE
                //Rect p1 = new Rect();
                //p1.Location = ucBoard.PointToScreen(new Point(0, 0));
                //p1.Height = ucBoard.ActualHeight;
                //p1.Width = ucBoard.ActualWidth;

                //Rect p2 = new Rect();
                //p2.Location = element.PointToScreen(new Point(0, 0));
                //p2.Height = element.ActualHeight;
                //p2.Width = element.ActualWidth;

                //if (!p1.IntersectsWith(p2))
                //{
                //    msg.Action = MessageGamePieceInfo.Actions.Reset;
                //}

                Utility.SoundUtility.PlayMovePiece();

                client.SendMessage(msg);

                element.ReleaseMouseCapture();                
                dragStart = null;
            }
        }
 
        public void BringToFront(UserControl element)
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

                            MessageGamePieceInfo msg = new MessageGamePieceInfo(myInfo.PlayerKey);
                            GamePieces.Add(msg.Key, element);                            
                            msg.PosX = e.GetPosition(canvas).X - 25;
                            msg.PosY = e.GetPosition(canvas).Y - 25;
                            msg.PieceType = element.GamePieceType;

                            Utility.SoundUtility.Play(element.GamePieceType);

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

                //TODO: fix
                //element.PieceColor = msg.Player.PlayerColor;
                element.RefreshUI();
            });
        }

        public Dictionary<Guid, UserControls.ucGamePiece> GamePieces = new Dictionary<Guid, UserControls.ucGamePiece>();

        private void ctlPlayerName_TextChanged(object sender, TextChangedEventArgs e)
        {
            myInfo.Name = ctlPlayerName.Text;
            client.SendMessage(myInfo); 
        }

        private void ctlPlayerColor_SelectedColorChanged(object sender, RoutedPropertyChangedEventArgs<Color?> e)
        {
            myInfo.PlayerColor = ctlPlayerColor.SelectedColor.Value;
            client.SendMessage(myInfo);
            
            RefreshToolBox();
        }
    }
}
