using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.ViewModels
{
    public class BaseViewModel 
    {       
        private MainWindow _Client;
        public MainWindow Instance { get { return _Client; } }

        public BaseViewModel(MainWindow main)
        {
            _Client = main;
        }        
    }
}
