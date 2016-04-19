using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;

namespace Client.Utility
{
    public static class SoundUtility
    {
        public static void PlayChatReceived()
        {
            Play(Properties.Resources.Sound_ChatReceived);
        }

        public static void PlayCards()
        {
            Play(Properties.Resources.Sound_Cards);
        }

        public static void PlayEngine()
        {
            Play(Properties.Resources.Sound_Engine);
        }

        public static void PlayKnight()
        {
            Play(Properties.Resources.Sound_Knight);
        }

        public static void PlayPawn()
        {
            Play(Properties.Resources.Sound_Pawn);
        }

        public static void PlayShip()
        {
            Play(Properties.Resources.Sound_Ship);
        }

        public static void PlayMarker()
        {
            PlayMovePiece();
        }

        public static void PlayMovePiece()
        {
            Play(Properties.Resources.Sound_PieceMove);
        }

        public static void Play(GameOfThronesCoreLibrary.Enums.GamePieceType type)
        {
            switch (type)
            {
                case GameOfThronesCoreLibrary.Enums.GamePieceType.Engine: PlayEngine(); break;
                case GameOfThronesCoreLibrary.Enums.GamePieceType.Knight: PlayKnight(); break;
                case GameOfThronesCoreLibrary.Enums.GamePieceType.Marker: PlayMarker(); break;
                case GameOfThronesCoreLibrary.Enums.GamePieceType.Pawn: PlayPawn(); break;
                case GameOfThronesCoreLibrary.Enums.GamePieceType.Ship: PlayShip(); break;
            }
        }

        public static void Play(UnmanagedMemoryStream target)
        {
            SoundPlayer player = new SoundPlayer(target);
            player.Play();
        }
    }
}
