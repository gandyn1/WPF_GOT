﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameOfThronesCoreLibrary.Messages;

namespace Client.ViewModels
{
    public abstract class BaseViewModel<TMessage> : GameOfThronesCoreLibrary.MessageListener<MainWindow,TMessage> where TMessage : IMessage
    {
        public BaseViewModel(MainWindow main) 
            :base(main) { }  
      
        public override void Subscribe<T>(Action<object, object> action)
        {
            Instance.client.Subscribe<T>(action);
        }

        public abstract override void MessageReceivedHandler(MyTcpClient client, TMessage msg);

    }
}
