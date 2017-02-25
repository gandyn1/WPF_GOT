using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameOfThrones.Common.Messages;

namespace GameOfThrones.Common
{
    
    public class MessageManager  
    {
        public List<MessagePlayerInfo> Players = new List<MessagePlayerInfo>();

        private Dictionary<string, Dictionary<Type, List<object>>> MessageLibrary = new Dictionary<string, Dictionary<Type, List<object>>>();

        public void ListenHandler(object sender, object msg){
            var collection = msg as IMessageCollection<IMessage>;

            if (collection != null)            
                collection.Messages.ForEach(o => ProcessMessage(o));            
            else
                ProcessMessage(msg as IMessage);
        }

        private void ProcessMessage(IMessage msg){

            var aboutPlayer = msg as IMessageAboutPlayer;
            if (aboutPlayer != null) ProcessMessageAboutPlayer(aboutPlayer);

        }

        private void ProcessMessageAboutPlayer(IMessageAboutPlayer msg)
        {
            //Add New Player
            if (!MessageLibrary.ContainsKey(msg.PlayerKey))            
                MessageLibrary.Add(msg.PlayerKey, new Dictionary<Type, List<object>>());

            //Get Players Messages
            var playerDictionary = MessageLibrary[msg.PlayerKey];

            //Add new type of message
            if(!playerDictionary.ContainsKey(msg.GetType())){
                playerDictionary.Add(msg.GetType(), new List<object>());
            }

            //Get Messages
            var messagesDictionary = playerDictionary[msg.GetType()];

            if (typeof(IUnique).IsAssignableFrom(msg.GetType()) )
            {
                var oldMsg = messagesDictionary.Where(o => ((IUnique)o).Key == ((IUnique)msg).Key).SingleOrDefault();
                messagesDictionary.Remove(oldMsg);
            }

            messagesDictionary.Add(msg);
        }        

        private void ProcessPlayer(MessagePlayerInfo player)
        {
            var current = Players.Where(o => o.PlayerKey == player.PlayerKey).FirstOrDefault();

            if (current != null)
                Players.Remove(current);

            Players.Add(player);
        }

        public List<T> GetMessages<T>(string playerKey) where T : IMessage
        {
            Dictionary<Type, List<object>> playerDictionary = null;
            
            if(MessageLibrary.ContainsKey(playerKey))
                 playerDictionary =  MessageLibrary[playerKey];

            return playerDictionary != null && playerDictionary.ContainsKey(typeof(T)) 
                ? playerDictionary[typeof(T)].Select(o => (T)o).ToList()  
                : null;
        }

    }



}
