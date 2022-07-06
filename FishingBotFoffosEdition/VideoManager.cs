using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FishingBotFoffosEdition
{
    public static class VideoManager
    {
        public static Bitmap GetSreenshot()
        {
            Bitmap bm = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
            Graphics g = Graphics.FromImage(bm);
            g.CopyFromScreen(0, 0, 0, 0, bm.Size);
            return bm;
        }

        public static string TakeScreenshot()
        {

            Rectangle bounds = Screen.GetBounds(Point.Empty);

            using (Bitmap bitmap = new Bitmap(bounds.Width, bounds.Height))
            {
                using (Graphics g = Graphics.FromImage(bitmap))
                {
                    g.CopyFromScreen(Point.Empty, Point.Empty, bounds.Size);
                }
                string path = $"C:\\Users\\andry\\Desktop\\DebugScreens\\Screen{DateTime.Now.ToString("yyyy-MM-dd_hh-mm-ss")}.jpg";
                bitmap.Save(path, ImageFormat.Jpeg);
                return path;
            }
        }

        public class SearchResult
        {
            public Rectangle rect;
            public double precision;
        }
        public static SearchResult newSearch(string imgContainerPath, string imgContainedPath)
        {
            double compareValue = 0;
            Rectangle returnedRect = new Rectangle();
            Image<Bgr, byte> source = new Image<Bgr, byte>(imgContainerPath); // Image B
            Image<Bgr, byte> template = new Image<Bgr, byte>(imgContainedPath); // Image A
            Image<Bgr, byte> imageToShow = source.Copy();

            using (Image<Gray, float> result = source.MatchTemplate(template, TemplateMatchingType.CcoeffNormed))
            {
                double[] minValues, maxValues;
                Point[] minLocations, maxLocations;
                result.MinMax(out minValues, out maxValues, out minLocations, out maxLocations);

                compareValue = maxValues[0];
                returnedRect = new Rectangle(maxLocations[0], template.Size);
                imageToShow.Draw(returnedRect, new Bgr(Color.Red), 3);
                Console.WriteLine($"{Path.GetFileName(imgContainedPath)}: {compareValue}");
                // You can try different values of the threshold. I guess somewhere between 0.75 and 0.95 would be good.
                //if (compareValue > 0.6)
                //{
                //    // This is a match. Do something with it, for example draw a rectangle around it.
                //    //returnedRect = new Rectangle(maxLocations[0], template.Size);
                //    //imageToShow.Draw(returnedRect, new Bgr(Color.Red), 3);
                //    //Console.WriteLine($"Image Found: {compareValue}");
                //}
                //else
                //{
                //    Console.WriteLine($"Image Not Found: {compareValue}");
                //}   
            }

            imageToShow.Save($"C:\\Users\\andry\\Desktop\\DebugScreens\\Processed\\{DateTime.Now.ToString("yyyy-MM-dd_hh-mm-ss")}.png");
            return new SearchResult() { rect = returnedRect, precision = compareValue };
        }
    }
}
