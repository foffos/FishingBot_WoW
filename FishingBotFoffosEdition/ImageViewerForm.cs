using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using FishingBotFoffosEdition.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Forms;
using System.Windows.Shapes;
using static FishingBotFoffosEdition.VideoManager;

namespace FishingBotFoffosEdition
{
	public partial class ImageViewerForm : Form
	{
		List<SearchResult> resultList;
		private Image originalImage;
		bool isPickingImage;
		bool pictureTaken;

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
			originalImage = Bitmap.FromFile(searchResult.imagePath);
			pictureBox.Image = new Bitmap(originalImage, new Size(pictureBox.Width, pictureBox.Height));
			imageInfoRichTextBox.Clear();
			imageInfoRichTextBox.AppendText($"File name: {System.IO.Path.GetFileName(searchResult.imagePath)}");
			imageInfoRichTextBox.AppendText($"\r\nPrecision: {searchResult.precision * 100}%");
		}

		private void nextButton_Click(object sender, EventArgs e)
		{
			currentImage++;
			if (currentImage + 1 > resultList.Count())
				currentImage = 0;
			updateGUI();
		}

		private void pictureBox_MouseMove(object sender, MouseEventArgs e)
		{
			if (!isPickingImage)
				return;

			//Position creation and validation
			float widthScale = originalImage.Width / pictureBox.Width;
			float heightScale = originalImage.Height / pictureBox.Height;
			Point Location = new Point((int)(e.X * widthScale - ((float)widthNumericUpDown.Value / 2)), (int)(e.Y * heightScale - ((float)heigtNumericUpDown.Value / 2)));
			if (Location.X < widthNumericUpDown.Value)
				Location.X = (int)widthNumericUpDown.Value;
			if (Location.X > originalImage.Width - widthNumericUpDown.Value)
				Location.X = (int)(originalImage.Width - widthNumericUpDown.Value);
			if (Location.Y < heigtNumericUpDown.Value)
				Location.Y = (int)heigtNumericUpDown.Value;
			if (Location.Y > originalImage.Height - heigtNumericUpDown.Value)
				Location.Y = (int)(originalImage.Height - heigtNumericUpDown.Value);

			System.Drawing.Rectangle rect = new System.Drawing.Rectangle(Location, new Size((int)widthNumericUpDown.Value, (int)heigtNumericUpDown.Value));
			Bitmap bmpImage = new Bitmap(originalImage);

			previewPictureBox.Image = bmpImage.Clone(rect, bmpImage.PixelFormat);

		}

		private void startPicLureButton_Click(object sender, EventArgs e)
		{
			startPicLureButton.Enabled = false;
			isPickingImage = true;
			pictureBox.Cursor = Cursors.Hand;
		}

		private void pictureBox_Click(object sender, EventArgs e)
		{
			if (!isPickingImage)
				return;
			startPicLureButton.Enabled = true;
			isPickingImage = false;
			pictureBox.Cursor = Cursors.Default;
			pictureTaken = true;
			saveTemplateButton.Enabled = true;
			templateNametextBox.Enabled = true;
		}

		private void saveTemplateButton_Click(object sender, EventArgs e)
		{
			try
			{
				string outputDirPath = System.IO.Path.Combine(Resources.TemplateFolder, templateNametextBox.Text);
				if (Directory.Exists(outputDirPath))
				{
					saveInfoLabel.ForeColor = Color.Red;
					saveInfoLabel.Text = templateNametextBox.Text + " already exists";
					return;
				}
				Directory.CreateDirectory(outputDirPath);

				using (MemoryStream memory = new MemoryStream())
				{
					using (FileStream fs = new FileStream(System.IO.Path.Combine(outputDirPath, "Lure.png"), FileMode.Create, FileAccess.ReadWrite))
					{
						Bitmap bmpImage = new Bitmap(previewPictureBox.Image);
						bmpImage.Save(memory, ImageFormat.Jpeg);
						byte[] bytes = memory.ToArray();
						fs.Write(bytes, 0, bytes.Length);
					}
				}
				saveInfoLabel.ForeColor = Color.Green;
				saveInfoLabel.Text = templateNametextBox.Text + " Created successfully";
			}
			catch (Exception ex)
			{
				saveInfoLabel.ForeColor = Color.Red;
				saveInfoLabel.Text = "ERROR";
			}
		}
	}
}