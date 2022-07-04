using AForge.Imaging;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.UI.MobileControls;
using System.Web.UI.WebControls;
using System.Windows.Controls;
using System.Windows.Forms;
using Emgu.CV;

using Emgu.CV.CvEnum;

using Emgu.CV.Structure;


namespace FishingBotFoffosEdition
{
    public static class Core
    {
        public static void ExecuteMainBotFunction(char fishingChar)
        {
            string path = TakeScreenshot();
            MouseUtility mouseUtilityBot = new MouseUtility();
            //string path = "C:\\Users\\andry\\Desktop\\DebugScreens\\Screen2022-07-02_08-27-00.jpg";
            string lurePath = "C:\\Users\\andry\\Desktop\\DebugScreens\\template.png";
            //string lurePathMirrored = "C:\\Users\\andry\\Desktop\\DebugScreens\\WowFishingLure_flip.png";
            //Core.FindImage(path);

            Point centerRectPoint = new Point();

            Rectangle result = Core.newSearch(path, lurePath);

            if (result.Width > 0 && result.Height > 0)
            {
                centerRectPoint = new Point(result.X + result.Width / 2, result.Y + result.Height / 2);
                mouseUtilityBot.MoveMouseToPos(centerRectPoint.X, centerRectPoint.Y);

                //TODO wait until right event for click
                mouseUtilityBot.RightMouseClick();
            }
        }
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

        



        


        public static bool FindImage(string imageOriginPath)
        {
            System.Drawing.Bitmap originalSourceImage = (Bitmap)Bitmap.FromFile(imageOriginPath);
            System.Drawing.Bitmap sourceImage2dbppRgb = ConvertBitmapToPixelFormat(originalSourceImage, PixelFormat.Format8bppIndexed);
            System.Drawing.Bitmap template = (Bitmap)Bitmap.FromFile($"C:\\Users\\andry\\Desktop\\DebugScreens\\WowFishingLure.png");
            // create template matching algorithm's instance
            // (set similarity threshold to 92.1%)

            ExhaustiveTemplateMatching tm = new ExhaustiveTemplateMatching(0.921f);
            // find all matchings with specified above similarity

            TemplateMatch[] matchings = tm.ProcessImage(sourceImage2dbppRgb, template);
            // highlight found matchings

            BitmapData data = sourceImage2dbppRgb.LockBits(
                 new Rectangle(0, 0, sourceImage2dbppRgb.Width, sourceImage2dbppRgb.Height),
                 ImageLockMode.ReadWrite, sourceImage2dbppRgb.PixelFormat);
            foreach (TemplateMatch m in matchings)
            {

                Drawing.Rectangle(data, m.Rectangle, Color.White);

                MessageBox.Show(m.Rectangle.Location.ToString());
                // do something else with matching
            }
            sourceImage2dbppRgb.UnlockBits(data);
            return true;
        }


        public static Bitmap ConvertBitmapToPixelFormat(Bitmap originBitmap, PixelFormat format)
        {
            Bitmap clone = new Bitmap(originBitmap.Width, originBitmap.Height, format);

            using (Graphics gr = Graphics.FromImage(clone))
            {
                gr.DrawImage(originBitmap, new Rectangle(0, 0, clone.Width, clone.Height));
                return clone;
            }
        }

        public static Rectangle FindImageInsideAnother(string imgContainerPath, string imgContainedPath)
        {
            System.Drawing.Bitmap bmpMatch = (Bitmap)Bitmap.FromFile(imgContainerPath);
            System.Drawing.Bitmap ScreenBmp = (Bitmap)Bitmap.FromFile(imgContainedPath);
            bool ExactMatch = false;

            //Bitmap bmpMatch, bool ExactMatch, Bitmap ScreenBmp
            //Bitmap originalSourceImage = (Bitmap)Bitmap.FromFile(imageOriginPath);



            BitmapData ImgBmd = bmpMatch.LockBits(new Rectangle(0, 0, bmpMatch.Width, bmpMatch.Height), ImageLockMode.ReadOnly, PixelFormat.Format24bppRgb);
            BitmapData ScreenBmd = ScreenBmp.LockBits(new Rectangle(0, 0, ScreenBmp.Width, ScreenBmp.Height), ImageLockMode.ReadOnly, PixelFormat.Format24bppRgb);

            byte[] ImgByts = new byte[(Math.Abs(ImgBmd.Stride) * bmpMatch.Height) - 1 + 1];
            byte[] ScreenByts = new byte[(Math.Abs(ScreenBmd.Stride) * ScreenBmp.Height) - 1 + 1];

            Marshal.Copy(ImgBmd.Scan0, ImgByts, 0, ImgByts.Length);
            Marshal.Copy(ScreenBmd.Scan0, ScreenByts, 0, ScreenByts.Length);

            bool FoundMatch = false;
            Rectangle rct = Rectangle.Empty;
            int sindx, iindx;
            int spc, ipc;

            int skpx = System.Convert.ToInt32((bmpMatch.Width - 1) / (double)10);
            if (skpx < 1 | ExactMatch)
                skpx = 1;
            int skpy = System.Convert.ToInt32((bmpMatch.Height - 1) / (double)10);
            if (skpy < 1 | ExactMatch)
                skpy = 1;

            for (int si = 0; si <= ScreenByts.Length - 1; si += 3)
            {
                FoundMatch = true;
                for (int iy = 0; iy <= ImgBmd.Height - 1; iy += skpy)
                {
                    for (int ix = 0; ix <= ImgBmd.Width - 1; ix += skpx)
                    {
                        sindx = (iy * ScreenBmd.Stride) + (ix * 3) + si;
                        iindx = (iy * ImgBmd.Stride) + (ix * 3);
                        spc = Color.FromArgb(ScreenByts[sindx + 2], ScreenByts[sindx + 1], ScreenByts[sindx]).ToArgb();
                        ipc = Color.FromArgb(ImgByts[iindx + 2], ImgByts[iindx + 1], ImgByts[iindx]).ToArgb();
                        if (spc != ipc)
                        {
                            FoundMatch = false;
                            iy = ImgBmd.Height - 1;
                            ix = ImgBmd.Width - 1;
                        }
                    }
                }
                if (FoundMatch)
                {
                    double r = si / (double)(ScreenBmp.Width * 3);
                    double c = ScreenBmp.Width * (r % 1);
                    if (r % 1 >= 0.5)
                        r -= 1;
                    rct.X = System.Convert.ToInt32(c);
                    rct.Y = System.Convert.ToInt32(r);
                    rct.Width = bmpMatch.Width;
                    rct.Height = bmpMatch.Height;
                    break;
                }
            }

            bmpMatch.UnlockBits(ImgBmd);
            ScreenBmp.UnlockBits(ScreenBmd);
            //ScreenBmp.Dispose();
            return rct;
        }


        public static Rectangle newSearch(string imgContainerPath, string imgContainedPath)
        {
            Rectangle returnedRect = new Rectangle();
            Image<Bgr, byte> source = new Image<Bgr, byte>(imgContainerPath); // Image B
            Image<Bgr, byte> template = new Image<Bgr, byte>(imgContainedPath); // Image A
            Image<Bgr, byte> imageToShow = source.Copy();

            using (Image<Gray, float> result = source.MatchTemplate(template, TemplateMatchingType.CcoeffNormed))
            {
                double[] minValues, maxValues;
                Point[] minLocations, maxLocations;
                result.MinMax(out minValues, out maxValues, out minLocations, out maxLocations);

                var compareValue = maxValues[0];
                // You can try different values of the threshold. I guess somewhere between 0.75 and 0.95 would be good.
                if (compareValue > 0.6)
                {
                    // This is a match. Do something with it, for example draw a rectangle around it.
                    returnedRect = new Rectangle(maxLocations[0], template.Size);
                    imageToShow.Draw(returnedRect, new Bgr(Color.Red), 3);
                }
                else
                    return returnedRect;
            }

            // Show imageToShow in an ImageBox (here assumed to be called imageBox1)
            //imageBox1.Image = imageToShow;
            imageToShow.Save($"C:\\Users\\andry\\Desktop\\DebugScreens\\Processed\\{DateTime.Now.ToString("yyyy-MM-dd_hh-mm-ss")}.png");
            return returnedRect;
        }

    }
}
