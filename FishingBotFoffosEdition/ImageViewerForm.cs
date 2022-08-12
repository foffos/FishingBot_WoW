using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static FishingBotFoffosEdition.VideoManager;

namespace FishingBotFoffosEdition
{
    public partial class ImageViewerForm : Form
    {
        List<SearchResult> resultList;
        public int currentImage = 0;
        public ImageViewerForm(List<SearchResult> resultList)
        {
            this.resultList = resultList;
            InitializeComponent();
            this.Enabled = true;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            updateGUI();
        }

        public void updateGUI()
        {
            updateGUI(resultList[currentImage]);
        }
        private void updateGUI(SearchResult searchResult)
        {
            var originalImage = Bitmap.FromFile(searchResult.imagePath);
            pictureBox.Image = new Bitmap(originalImage, new Size(pictureBox.Width, pictureBox.Height));
            imageInfoRichTextBox.Clear();
            imageInfoRichTextBox.AppendText($"File name: {Path.GetFileName(searchResult.imagePath)}");
            imageInfoRichTextBox.AppendText($"\r\nPrecision: {searchResult.precision * 100}%");
        }

        private void nextButton_Click(object sender, EventArgs e)
        {
            currentImage++;
            if (currentImage + 1 > resultList.Count())
                currentImage = 0;
            updateGUI();
        }
    }
}