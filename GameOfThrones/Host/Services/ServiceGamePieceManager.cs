﻿using GameOfThronesCoreLibrary.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Host.Services
{
    public class ServiceGamePieceManager : BaseService<MessageGamePieceInfo>
    {

        public Dictionary<Guid, MessageGamePieceInfo> MessageGamePieceInfo = new Dictionary<Guid, MessageGamePieceInfo>();        

        public override void MessageReceivedHandler(MyTcpClient client, MessageGamePieceInfo msg)
        {            
            if (msg.Action != GameOfThronesCoreLibrary.Messages.MessageGamePieceInfo.Actions.Reset)
            {
                if (!MessageGamePieceInfo.ContainsKey(msg.Key))
                {
                    MessageGamePieceInfo.Add(msg.Key, msg);                    
                }

                var owner = MessageGamePieceInfo[msg.Key].Player;
                MessageGamePieceInfo[msg.Key] = msg;

                //switch owner - in the future we might 
                if (msg.Player.PlayerKey != owner.PlayerKey)
                    MessageGamePieceInfo[msg.Key].Player = owner;
            }

            if (msg.Action == GameOfThronesCoreLibrary.Messages.MessageGamePieceInfo.Actions.Delete)
            {
                MessageGamePieceInfo.Remove(msg.Key);
            }

            Host.Clients.Broadcast(GetMessage());
        }

        private MessageGameBoardUpdate GetMessage()
        {
            var outMsg = new MessageGameBoardUpdate();
            outMsg.MessageGamePieceInfos = MessageGamePieceInfo.Select(o => o.Value).ToList();
            return outMsg;
        }

        public void UpdateClient(MessagePlayerInfo player){

            var playerPieces = MessageGamePieceInfo.Where(o => o.Value.Player.PlayerKey == player.PlayerKey)
                                                    .Select(o => o.Value).ToList();

            foreach (var p in playerPieces)
            {
                p.Player = player;
            }

             Host.Clients.Broadcast(GetMessage());
        }

        public override bool NotifyClient(MyTcpClient client)
        {
            client.SendMessage(GetMessage());
            return true;
        }
    }
}
