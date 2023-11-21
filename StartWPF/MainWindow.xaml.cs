using System;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Media.Imaging;
using Emgu.CV;
using Emgu.CV.Structure;

namespace StartWPF
{
    /// <summary>
    /// Interação lógica para MainWindow.xam
    /// </summary>
    public partial class MainWindow : Window
    {
        [System.Runtime.InteropServices.DllImport("gdi32.dll")]
        // System.Runtime.InteropServices.dll
        public static extern bool DeleteObject(IntPtr handle);

        public MainWindow()
        {
            InitializeComponent();
            LoadImages();
        }

        private void LoadImages()
        {
            string imagePath = @"C:\Users\matheus.filipi\Documents\Downloads";
            try
            {
                string[] imageFiles = Directory.GetFiles(imagePath, "*.png");
                foreach (var imageFile in imageFiles)
                {
                    Mat imageMat = CvInvoke.Imread(imageFile, Emgu.CV.CvEnum.ImreadModes.Color);
                    var image = imageMat.ToImage<Bgr, byte>();

                    var teste = BitmapHelper.CreateBitmapSourceFromBitmap(image.ToBitmap());
                    //Image img = (Image)teste;
                    //imageStackPanel.Children.Add();
                }
            }
            catch (Exception)
            {

            }
        }


    }

    class BitmapHelper
    {
        [DllImport("gdi32.dll")]
        private static extern bool DeleteObject(IntPtr hObject);

        public static BitmapSource CreateBitmapSourceFromBitmap(Bitmap bitmap)
        {
            if (bitmap == null)
            {
                throw new ArgumentNullException("bitmap");
            }
            lock (bitmap)
            {
                IntPtr hBitmap = bitmap.GetHbitmap();
                try
                {
                    return System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(
                        hBitmap,
                        IntPtr.Zero,
                        Int32Rect.Empty,
                        BitmapSizeOptions.FromEmptyOptions());
                }
                finally
                {
                    DeleteObject(hBitmap);
                }
            }
        }
    }
}
