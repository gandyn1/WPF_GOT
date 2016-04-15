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
 

        public GamePieceType GamePieceType
        {
            get { return  (GamePieceType)GetValue(GamePieceTypeProperty); }
            set { SetValue(GamePieceTypeProperty, value); }
        }

        public static readonly DependencyProperty GamePieceTypeProperty =
     DependencyProperty.Register(
        "GamePieceType", typeof( GamePieceType), typeof(ucGamePiece),
        new FrameworkPropertyMetadata(GamePieceType.Pawn,
            FrameworkPropertyMetadataOptions.AffectsRender,
            (o, e) => ((ucGamePiece)o).OnGamePieceTypeChanged()));

       private void OnGamePieceTypeChanged()
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
            
           ImageBrush b = new ImageBrush(new BitmapImage(new Uri(path)));     ;
           b.Stretch = Stretch.Uniform;
           ucBorder.Background = b;      
        }
 
        public ucGamePiece()
        {
            InitializeComponent();
            OnGamePieceTypeChanged();
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
            return uc;
        }

    }
}
