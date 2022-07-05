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
using CSCore;
using CSCore.SoundIn;
using CSCore.CoreAudioAPI;
using System.IO;

namespace FishingBotFoffosEdition
{
    public static class Core
    {
        private const int X_OFFSET = -3;
        private const int Y_OFFSET = -3;

        private const int EXECUTION_DELAY = 1000;
        private const int EXECUTION_DELAY_FAST = 100;
        private const int EXECUTION_DELAY_EXTENDED = 2000;
        private const double AUDIO_TRASHOLD = 0.3;
        private const string LURE_FOLDER_PATH = "C:\\Users\\andry\\Desktop\\DebugScreens\\Templates\\";
        enum State
        {
            ReadyToFish,
            FishingPoleUsed,
            FishingLurePositionFound,
            Fished
        };
        public static void ExecuteMainBotFunction(char fishingChar)
        {

            State state = State.ReadyToFish;
            var device = AudioManager.GetDefaultRenderDevice();
            //var device = AudioManager.GetSecondRenderDevice();
            MouseUtility mouseUtilityBot = new MouseUtility();
            Thread.Sleep(EXECUTION_DELAY_EXTENDED);

            using (var meter = AudioMeterInformation.FromDevice(device))
            {
                while (true)
                {
                    switch (state)
                    {
                        //use fishing pole
                        case State.ReadyToFish:
                            Thread.Sleep(EXECUTION_DELAY);
                            Console.WriteLine($"Execute_ReadyToFish");
                            mouseUtilityBot.KeyboardPressFishing();
                            state = State.FishingPoleUsed;
                            break;

                        //search for target
                        case State.FishingPoleUsed:
                            Thread.Sleep(EXECUTION_DELAY);
                            Console.WriteLine($"Execute_FishingPoleUsed");
                            Point centerRectPoint = new Point();
                            string path = TakeScreenshot();
                            List<SearchResult> searchResutList = new List<SearchResult>();
                            foreach (var template in Directory.GetFiles(LURE_FOLDER_PATH))
                            {
                                SearchResult searchResult = Core.newSearch(path, template);
                                searchResutList.Add(searchResult);
                            }

                            SearchResult topResult = searchResutList.OrderByDescending(x => x.precision).First();
                            Console.WriteLine($"____________________________________");
                            Console.WriteLine($"top precision: {topResult.precision}");
                            var rect = topResult.rect;
                            if (rect.Width > 0 && rect.Height > 0)
                            {
                                centerRectPoint = new Point(rect.X + (rect.Width / 2) + X_OFFSET, rect.Y + (rect.Height / 2) + Y_OFFSET);
                                mouseUtilityBot.MoveMouseToPos(centerRectPoint.X, centerRectPoint.Y);
                                state = State.FishingLurePositionFound;
                            }

                            break;

                        //check audio for click
                        case State.FishingLurePositionFound:
                            Thread.Sleep(EXECUTION_DELAY_FAST);
                            Console.WriteLine($"Execute_FishingLurePositionFound");
                            float volume = meter.PeakValue;
                            Console.WriteLine($"volume: {volume} / {AUDIO_TRASHOLD}");
                            if (volume > AUDIO_TRASHOLD)
                            {
                                mouseUtilityBot.RightMouseClick();
                                Console.WriteLine("CLICK");
                                state = State.Fished;
                            }

                            break;

                        //restart
                        case State.Fished:
                            Thread.Sleep(EXECUTION_DELAY_EXTENDED);
                            Console.WriteLine($"Execute_Fished");
                            state = State.ReadyToFish;
                            break;
                    }
                }
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



        public static void testAudioCapture()
        {

            int device = getDevices();

            var a = new WasapiLoopbackCapture();
            a.Initialize();
            //IntPtr intptr = new IntPtr(device);
            //IntPtr.Zero
            //a.Device = new CSCore.CoreAudioAPI.MMDevice(intptr);
            a.DataAvailable += dataavailable;
            a.Start();
            Thread.Sleep(5000);
            a.Stop();

        }
        private static void dataavailable(object sender, DataAvailableEventArgs e)
        {
            Console.WriteLine(e.Data.ToString());
        }




        public static int getDevices()
        {
            uint nNumDevices = waveOutGetNumDevs();

            if (nNumDevices >= 1)
            {
                int nRet = MMSYSERR_NODRIVER;
                uint dwFlags = 0;
                uint uWaveID = 0;
                nRet = waveOutMessage((IntPtr)WAVE_MAPPER, DRVM_MAPPER_PREFERRED_GET, out uWaveID, out dwFlags);
                if (nRet == MMSYSERR_NOERROR)
                {
                    WAVEOUTCAPS caps = new WAVEOUTCAPS();
                    nRet = waveOutGetDevCaps((IntPtr)uWaveID, out caps, Marshal.SizeOf(typeof(WAVEOUTCAPS)));
                    return nRet;
                }
            }
            return MMSYSERR_NODRIVER;
        }

        [DllImport("Winmm.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern uint waveOutGetNumDevs();

        [DllImport("Winmm.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern int waveOutGetDevCaps(IntPtr uDeviceID, out WAVEOUTCAPS pwoc, int cbwoc);

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        public struct WAVEOUTCAPS
        {
            public ushort wMid;
            public ushort wPid;
            public uint vDriverVersion;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
            public string szPname;
            public int dwFormats;
            public ushort wChannels;
            public ushort wReserved1;
            public int dwSupport;
        }

        [DllImport("Winmm.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern int waveOutMessage(IntPtr deviceID, uint uMsg, out uint dwParam1, out uint dwParam2);

        public const int WAVE_MAPPER = (-1);

        public const int MMSYSERR_BASE = 0;
        /* general error return values */
        public const int MMSYSERR_NOERROR = 0;                    /* no error */
        public const int MMSYSERR_ERROR = (MMSYSERR_BASE + 1);  /* unspecified error */
        public const int MMSYSERR_BADDEVICEID = (MMSYSERR_BASE + 2);  /* device ID out of range */
        public const int MMSYSERR_NOTENABLED = (MMSYSERR_BASE + 3);  /* driver failed enable */
        public const int MMSYSERR_ALLOCATED = (MMSYSERR_BASE + 4);  /* device already allocated */
        public const int MMSYSERR_INVALHANDLE = (MMSYSERR_BASE + 5);  /* device handle is invalid */
        public const int MMSYSERR_NODRIVER = (MMSYSERR_BASE + 6);  /* no device driver present */
        public const int MMSYSERR_NOMEM = (MMSYSERR_BASE + 7);  /* memory allocation error */
        public const int MMSYSERR_NOTSUPPORTED = (MMSYSERR_BASE + 8);  /* function isn't supported */
        public const int MMSYSERR_BADERRNUM = (MMSYSERR_BASE + 9);  /* error value out of range */
        public const int MMSYSERR_INVALFLAG = (MMSYSERR_BASE + 10); /* invalid flag passed */
        public const int MMSYSERR_INVALPARAM = (MMSYSERR_BASE + 11); /* invalid parameter passed */
        public const int MMSYSERR_HANDLEBUSY = (MMSYSERR_BASE + 12); /* handle being used simultaneously on another thread (eg callback) */
        public const int MMSYSERR_INVALIDALIAS = (MMSYSERR_BASE + 13); /* specified alias not found */
        public const int MMSYSERR_BADDB = (MMSYSERR_BASE + 14); /* bad registry database */
        public const int MMSYSERR_KEYNOTFOUND = (MMSYSERR_BASE + 15); /* registry key not found */
        public const int MMSYSERR_READERROR = (MMSYSERR_BASE + 16); /* registry read error */
        public const int MMSYSERR_WRITEERROR = (MMSYSERR_BASE + 17); /* registry write error */
        public const int MMSYSERR_DELETEERROR = (MMSYSERR_BASE + 18); /* registry delete error */
        public const int MMSYSERR_VALNOTFOUND = (MMSYSERR_BASE + 19); /* registry value not found */
        public const int MMSYSERR_NODRIVERCB = (MMSYSERR_BASE + 20); /* driver does not call DriverCallback */
        public const int MMSYSERR_MOREDATA = (MMSYSERR_BASE + 21); /* more data to be returned */
        public const int MMSYSERR_LASTERROR = (MMSYSERR_BASE + 21); /* last error in range */

        public const int DRV_RESERVED = 0x0800;

        public const int DRVM_MAPPER = (0x2000);
        public const int DRVM_USER = 0x4000;
        public const int DRVM_MAPPER_STATUS = (DRVM_MAPPER + 0);
        public const int DRVM_MAPPER_RECONFIGURE = (DRVM_MAPPER + 1);
        public const int DRVM_MAPPER_PREFERRED_GET = (DRVM_MAPPER + 21);
        public const int DRVM_MAPPER_CONSOLEVOICECOM_GET = (DRVM_MAPPER + 23);

        public const int DRV_QUERYDEVNODE = (DRV_RESERVED + 2);
        public const int DRV_QUERYMAPPABLE = (DRV_RESERVED + 5);
        public const int DRV_QUERYMODULE = (DRV_RESERVED + 9);
        public const int DRV_PNPINSTALL = (DRV_RESERVED + 11);
        public const int DRV_QUERYDEVICEINTERFACE = (DRV_RESERVED + 12);
        public const int DRV_QUERYDEVICEINTERFACESIZE = (DRV_RESERVED + 13);
        public const int DRV_QUERYSTRINGID = (DRV_RESERVED + 14);
        public const int DRV_QUERYSTRINGIDSIZE = (DRV_RESERVED + 15);
        public const int DRV_QUERYIDFROMSTRINGID = (DRV_RESERVED + 16);
        public const int DRV_QUERYFUNCTIONINSTANCEID = (DRV_RESERVED + 17);
        public const int DRV_QUERYFUNCTIONINSTANCEIDSIZE = (DRV_RESERVED + 18);
        //
        // DRVM_MAPPER_PREFERRED_GET flags
        //
        public const int DRVM_MAPPER_PREFERRED_FLAGS_PREFERREDONLY = 0x00000001;
    }
}
