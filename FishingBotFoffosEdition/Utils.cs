using FishingBotFoffosEdition.Properties;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Shapes;
using Path = System.IO.Path;

namespace FishingBotFoffosEdition
{
    public static class Utils
    {
        private static string LOGFILENAME = "Log.log";
        /// <summary>
        /// returns list of folder contained inside the template folder (template folder is taken from Resources)
        /// </summary>
        /// <returns></returns>
        public static List<string> getTemplateFolderList()
        {
            List<string> returnList = new List<string>();

            foreach (var folder in Directory.GetDirectories(Resources.TemplateFolder))
            {
                returnList.Add(folder);
            }
            return returnList;
        }

        public static List<string> getTemplateFolderNamesList()
        {
            List<string> returnList = new List<string>();

            foreach (var folder in Directory.GetDirectories(Resources.TemplateFolder))
            {
                returnList.Add(new DirectoryInfo(folder).Name);
            }
            return returnList;
        }

        public static List<string> GetTemplateFilesInFolder(string templateFolderName)
        {
            string templatesFolderDir = Resources.TemplateFolder;
            var returnList = Directory.GetFiles(Path.Combine(templatesFolderDir, templateFolderName)).ToList();
            return returnList;
        }

        public static void Log(string logString)
        {
            if (!File.Exists(LOGFILENAME))
            {
                using (FileStream fs = File.Create(LOGFILENAME))
                {

                }
            }
            File.AppendAllText(LOGFILENAME, String.Concat($"[{DateTime.Now}] - {logString}"));
        }
    }
}
