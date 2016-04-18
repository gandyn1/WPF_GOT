using GameOfThronesCoreLibrary.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
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
using System.Reflection;
using System.IO;

namespace Client.UserControls
{
    /// <summary>
    /// Interaction logic for ucGamePiece.xaml
    /// </summary>
    public partial class ucGamePiece : UserControl
    {
        public System.Windows.Media.Color PieceColor = Colors.Blue;

        public GamePieceType GamePieceType { get; set; }

        public void OnGamePieceTypeChanged()
        {
            RefreshUI();
        }

       public void RefreshUI()
        {
            var path = Environment.CurrentDirectory + "\\Pieces\\";

            if (GamePieceType == GamePieceType.Knight)
                path += "Knight.png";

            if (GamePieceType == GamePieceType.Engine)
                path += "Engine.png";

            if (GamePieceType == GamePieceType.Pawn)
                path += "Pawn.png";

            if (GamePieceType == GamePieceType.Ship)
                path += "Ship.png";

            var bmImage = new BitmapImage(new Uri(path));
            var bm = GameOfThronesCoreLibrary.Utility.BitmapUtility.BitmapImageToBitmap(bmImage);
            bm = GameOfThronesCoreLibrary.Utility.BitmapUtility.ColorTint(bm, PieceColor);
            ImageBrush b = new ImageBrush(GameOfThronesCoreLibrary.Utility.BitmapUtility.BitmapToImageSource(bm));
            b.Stretch = Stretch.Uniform;
            ucBorder.Background = b;   
        }

        public ucGamePiece()
        {
            InitializeComponent();
            RefreshUI();
            Margin = new Thickness(10);
            Width = 50;
            Height = 50;
        }

        private void UserControl_MouseMove(object sender, MouseEventArgs e)
        {
            base.OnMouseMove(e);
            if (e.LeftButton == MouseButtonState.Pressed && this.Parent is WrapPanel)
            {
                var uc = this.Clone();

                DataObject data = new DataObject();
                data.SetData("ucGamePiece", uc);
                DragDrop.DoDragDrop(uc, data, DragDropEffects.Copy);                
            }
        }

        public ucGamePiece Clone()
        {
            var uc = new UserControls.ucGamePiece();
            uc.GamePieceType = this.GamePieceType;
            uc.PieceColor = this.PieceColor;
            return uc;
        }
    }
}
