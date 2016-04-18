using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Host
{
    public static class GameOfThronesQuotes
    {
        private static List<string> Quotes = new List<string>();

        static GameOfThronesQuotes()
        {
            Quotes.Add("Never forget what you are, for surely the world will not. Make it your strength. Then it can never be your weakness. Armor yourself in it, and it will never be used to hurt you.");
            Quotes.Add("Let them see that their words can cut you, and you’ll never be free of the mockery. If they want to give you a name, take it, make it your own. Then they can’t hurt you with it anymore.");
            Quotes.Add("When you play the game of thrones, you win or you die. There is no middle ground.");
            Quotes.Add("The common people pray for rain, healthy children, and a summer that never ends,’ Ser Jorah told her. ‘It is no matter to them if the high lords play their game of thrones, so long as they are left in peace.’ He gave a shrug. ‘They never are.");
            Quotes.Add("If you would take a man’s life, you owe it to him to look into his eyes and hear his final words. And if you cannot bear to do that, then perhaps the man does not deserve to die.");
            Quotes.Add("Sorcery is the sauce fools spoon over failure to hide the flavor of their own incompetence.");
            Quotes.Add("Power resides where men believe it resides. No more and no less.");
            Quotes.Add("There’s no shame in fear, my father told me, what matters is how we face it.");
            Quotes.Add("Love is poison. A sweet poison, yes, but it will kill you all the same.");
            Quotes.Add("What good is this, I ask you? He who hurries through life hurries to his grave.");
            Quotes.Add("Old stories are like old friends, she used to say. You have to visit them from time to time.");
            Quotes.Add("The greatest fools are ofttimes more clever than the men who laugh at them.");
            Quotes.Add("Everyone wants something, Alayne. And when you know what a man wants you know who he is, and how to move him.");
            Quotes.Add("Always keep your foes confused. If they are never certain who you are or what you want, they cannot know what you are like to do next. Sometimes the best way to baffle them is to make moves that have no purpose, or even seem to work against you.");
            Quotes.Add("One voice may speak you false, but in many there is always truth to be found.");
            Quotes.Add("History is a wheel, for the nature of man is fundamentally unchanging.");
            Quotes.Add("Knowledge is a weapon, Jon. Arm yourself well before you ride forth to battle.");
            Quotes.Add("I prefer my history dead. Dead history is writ in ink, the living sort in blood.");
            Quotes.Add("In the game of thrones, even the humblest pieces can have wills of their own. Sometimes they refuse to make the moves you’ve planned for them. Mark that well, Alayne. It’s a lesson that Cersei Lannister still has yet to learn.");
            Quotes.Add("Every man should lose a battle in his youth, so he does not lose a war when he is old.");
            Quotes.Add("A reader lives a thousand lives before he dies, said Jojen. The man who never reads lives only one.");
            Quotes.Add("The fisherman drowned, but his daughter got Stark to the Sisters before the boat went down. They say he left her with a bag of silver and a bastard in her belly. Jon Snow, she named him, after Arryn.");
            Quotes.Add("You could make a poultice out of mud to cool a fever. You could plant seeds in mud and grow a crop to feed your children. Mud would nourish you, where fire would only consume you, but fools and children and young girls would choose fire every time.");
            Quotes.Add("Men live their lives trapped in an eternal present, between the mists of memory and the sea of shadow that is all we know of the days to come.");
            Quotes.Add("No. Hear me, Daenerys Targaryen. The glass candles are burning. Soon comes the pale mare, and after her the others. Kraken and dark flame, lion and griffin, the sun’s son and the mummer’s dragon. Trust none of them. Remember the Undying. Beware the perfumed seneschal.");
        }

        private static Random _Random = new Random();
        public static string RandomQuote()
        {
            return Quotes[_Random.Next(0, Quotes.Count)];
        }

    }
}
