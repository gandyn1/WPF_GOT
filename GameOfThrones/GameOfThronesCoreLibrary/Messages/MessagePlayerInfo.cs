﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks; 
using  System.Windows.Media;

namespace GameOfThrones.Common.Messages
{
    [Serializable]
    public class MessagePlayerInfo : IMessageAboutPlayer
    {
        private Guid _PlayerKey = Guid.NewGuid();
        public string PlayerKey { get { return _PlayerKey.ToString(); } }

        public override string ToString()
        {
            return string.Format("name:  {0}  key;  {1}", Name, PlayerKey) ;
        }

        public string Name { get; set; }

        public bool isActive = true;

        public string Message { get { return "PlayerInfo"; } }

        private string _PlayerColor = "#FFDFD99";

        public System.Windows.Media.Color PlayerColor
        {
            get
            {
                return GameOfThrones.Common.Utility.BitmapUtility.Parse(_PlayerColor);
            }
            set
            {
                _PlayerColor = value.ToString();
            }
        }
      
    }
}
