﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfThrones.Common.Messages
{
    public interface IMessageAboutPlayer : IMessage
    {
        string PlayerKey { get; }
    }
}
