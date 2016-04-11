using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using SimpleMvvmToolkit;  

namespace GameOfThrones.ViewModels
{
    public class LobbyViewModel : ViewModelBase<LobbyViewModel>
    {

        public DelegateCommand StartCommand {get;set;}
        public DelegateCommand JoinCommand { get; set; }

        private string _UserName = "TornSaint";
        public string UserName{
            get { return _UserName; }
            set {
                _UserName = value;
                NotifyPropertyChanged(o => o.UserName);
            }
        }

        private string _IP = GetIP4Address() + ":123";
        public string IP
        {
            get { return _IP; }
            set {
                _IP = value;
                NotifyPropertyChanged(o => o.IP);
            }
        }

        private string _ErrorMsg;
        public string ErrorMsg
        {
            get { return _ErrorMsg; }
            set {
                if (value != "")
                    AlertMsg = "";

                _ErrorMsg = value;
                NotifyPropertyChanged(o => o.ErrorMsg);
            }
        }

        private string _AlertMsg;
        public string AlertMsg
        {
            get { return _AlertMsg; }
            set
            {
                if(value != "")
                    ErrorMsg = "";

                _AlertMsg = value;
                NotifyPropertyChanged(o => o.AlertMsg);
            }
        }

        public LobbyViewModel()
        {
            StartCommand = new DelegateCommand(OnStartCommand);
            JoinCommand = new DelegateCommand(OnJoinCommand);
        }

        private void OnJoinCommand()
        {
            if (!isValid())
                return;

            var ipAddress = CreateIPEndPoint(IP);

            Task.Run(() =>
            {
                App.Current.Dispatcher.Invoke(() =>
                {
                    try
                    {
                        var client = new Client.MainWindow(ipAddress.Address.ToString(), ipAddress.Port, UserName);
                        client.Show();
                        AlertMsg = string.Format("{0} has joined {1}", UserName, ipAddress.ToString());
                    }
                    catch (Exception ex)
                    {
                        ErrorMsg = ex.Message;
                    }                    
                });                
            }); 

        }

        private void OnStartCommand()
        {
            if (!isValid())
                return;

            var ipAddress = CreateIPEndPoint(IP);

            Task.Run(() =>
            {
                try
                {
                    var host = new Host.Host(ipAddress.Address.ToString(), ipAddress.Port);
                    AlertMsg = string.Format("{0} has started hosting at {1}", UserName, ipAddress.ToString());
                    OnJoinCommand();
                }
                catch (Exception ex)
                {
                    ErrorMsg = ex.Message;
                }                
            });                    
        }

        private bool isValid()
        { 
            ErrorMsg = "";

            if (UserName.Trim() == "")            
                ErrorMsg = "Invalid User Name";            
            else
            {
                try
                {
                    var target = CreateIPEndPoint(IP);                   
                }
                catch (Exception ex)
                {
                    ErrorMsg = ex.Message;
                }                   
            }

            return ErrorMsg == "";
        }

        private static IPEndPoint CreateIPEndPoint(string endPoint)
        {
            string[] ep = endPoint.Split(':');
            if (ep.Length < 2) throw new FormatException("Invalid endpoint format");
            IPAddress ip;
            if (ep.Length > 2)
            {
                if (!IPAddress.TryParse(string.Join(":", ep, 0, ep.Length - 1), out ip))
                {
                    throw new FormatException("Invalid ip-adress");
                }
            }
            else
            {
                if (!IPAddress.TryParse(ep[0], out ip))
                {
                    throw new FormatException("Invalid ip-adress");
                }
            }
            int port;
            if (!int.TryParse(ep[ep.Length - 1], NumberStyles.None, NumberFormatInfo.CurrentInfo, out port))
            {
                throw new FormatException("Invalid port");
            }

            return new IPEndPoint(ip, port);
        }

        public static string GetIP4Address()
        {
            string IP4Address = String.Empty;

            foreach (IPAddress IPA in Dns.GetHostAddresses(Dns.GetHostName()))
            {
                if (IPA.AddressFamily == AddressFamily.InterNetwork)
                {
                    IP4Address = IPA.ToString();
                    break;
                }
            }

            return IP4Address;
        } 
    }
}
