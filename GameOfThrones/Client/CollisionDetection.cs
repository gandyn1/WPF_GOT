using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Client
{
    public  class CollisionDetection {

        public Canvas GameBoard;

        private  double _ScreenDpiX;
        private double _ScreenDpiY;

        public double ScreenDpiX {
            get {
                SetDpi();
                return _ScreenDpiX;
            }
        }
        public double ScreenDpiY
        {
            get {
                SetDpi();
                return _ScreenDpiY;
            }
        }
        private void SetDpi()
        {
            if (_ScreenDpiX == 0)
            {
                Matrix m = PresentationSource.FromVisual(Application.Current.MainWindow).CompositionTarget.TransformToDevice;
                _ScreenDpiX = m.M11;
                _ScreenDpiY = m.M22;
            }
        }

        //private Bitmap bitmap;
        public CollisionDetection(Canvas gameBoard)
        {
            GameBoard = gameBoard;
            //bitmap = new BitmapImage(new Uri(@"Images/GOT_Board_CollisionDetection.jpg", UriKind.RelativeOrAbsolute));
        }

        //public  Color  GetPixelColor(Visual visual, int x, int y)
        //{
        //    return GetAverageColor(visual, new Rect(x, y, 1, 1));
        //}

        //public  Color GetAverageColor(Visual visual, Rect area)
        //{
        //    var bitmap = new RenderTargetBitmap(1, 1, ScreenDpiX, ScreenDpiY, PixelFormats.Pbgra32);
        //    bitmap.Render(
        //     new Rectangle
        //     {
        //         Width = 1,
        //         Height = 1,
        //         Fill = new VisualBrush { Visual = visual, Viewbox = area }
        //     });
        //    var bytes = new byte[4];
        //    bitmap.CopyPixels(bytes, 1, 0);
        //    return Color.FromArgb(bytes[0], bytes[1], bytes[2], bytes[3]);
        //}

        //public string Territory(double x, double y)
        //{
        //    return null;
        //}

    }
}
