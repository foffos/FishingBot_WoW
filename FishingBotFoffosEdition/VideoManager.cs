using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using FishingBotFoffosEdition.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace FishingBotFoffosEdition
{
    public class VideoManager
    {
        public int offsetClickX;
        public int offsetClickY;
        public VideoManager()
        {
            this.offsetClickX = 0;
            this.offsetClickY = 0;
        }
        /// <summary>
        /// get live screenshot from primary screen based on his resolution, saves it to the configuration folder and return his path
        /// </summary>
        /// <returns>.jpg image file path</returns>
        public static string TakeScreenshot()
        {
            Rectangle bounds = Screen.GetBounds(Point.Empty);

            using (Bitmap bitmap = new Bitmap(bounds.Width, bounds.Height))
            {
                using (Graphics g = Graphics.FromImage(bitmap))
                {
                    g.CopyFromScreen(Point.Empty, Point.Empty, bounds.Size);
                }
                string path = Path.Combine(Resources.TempFolder, $"Screen{DateTime.Now.ToString("yyyy-MM-dd_hh-mm-ss")}.jpg");
                bitmap.Save(path, ImageFormat.Jpeg);
                return path;
            }
        }

        /// <summary>
        /// Class that holds infos about a search made on image
        /// </summary>
        public class SearchResult
        {
            public Rectangle rect;
            public double precision;
            public Point optimizedClickPoint;
            public string imagePath;
        }

        /// <summary>
        /// search the second image inside the first one
        /// </summary>
        /// <param name="imgContainerPath">path of the image that should contain the other one</param>
        /// <param name="imgContainedPath">path of the image that should be contained inside the other one</param>
        /// <returns>search result info</returns>
        public SearchResult newSearch(string imgContainerPath, string imgContainedPath)
        {
            double compareValue = 0;
            Rectangle lureReturnedRect = new Rectangle();
            Point optimizedClickPoint = new Point();
            Image<Bgr, byte> source = new Image<Bgr, byte>(imgContainerPath);
            Image<Bgr, byte> template = new Image<Bgr, byte>(imgContainedPath);
            Image<Bgr, byte> imageToShow = source.Copy();

            using (Image<Gray, float> result = source.MatchTemplate(template, TemplateMatchingType.CcoeffNormed))
            {
                double[] minValues, maxValues;
                Point[] minLocations, maxLocations;
                result.MinMax(out minValues, out maxValues, out minLocations, out maxLocations);
                compareValue = maxValues[0];
                Point foundPoint = maxLocations[0];
                lureReturnedRect = new Rectangle(maxLocations[0], template.Size);
                optimizedClickPoint = new Point(foundPoint.X + (int)(template.Width * 0.5) + offsetClickX, foundPoint.Y + (int)(template.Height * 0.5) + offsetClickY);
                imageToShow.Draw(lureReturnedRect, new Bgr(Color.Red), 2);
                imageToShow.Draw(new Cross2DF(optimizedClickPoint, 10, 10), new Bgr(Color.Purple), 2);
            }
            string pathToSave = Path.Combine(Resources.TempFolder, $"Processed//Screen{DateTime.Now.ToString("yyyy-MM-dd_hh-mm-ss")}.jpg");
            imageToShow.Save(pathToSave);
            return new SearchResult() { rect = lureReturnedRect, precision = compareValue, optimizedClickPoint = optimizedClickPoint, imagePath = pathToSave };
        }

        public List<SearchResult> FindTemplatesInCurrentScreen(List<string> templateFilePathList)
        {
            string screenhotPath = TakeScreenshot();
            List<SearchResult> searchResutList = new List<SearchResult>();
            foreach (var template in templateFilePathList)
            {
                SearchResult searchResult = newSearch(screenhotPath, template);
                searchResutList.Add(searchResult);
            }
            return searchResutList;
        }

        public SearchResult FindTemplateInCurrentScreenBestPrecision(List<string> templateFilePathList)
        {
            var results = FindTemplatesInCurrentScreen(templateFilePathList);
            return results.OrderByDescending(x => x.precision).First();
        }
    }
}
