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

using Microsoft.Win32;


//to read convert and work with PDF files
using Syncfusion.Pdf;
using Syncfusion.Pdf.Graphics;
using Syncfusion.Pdf.Parsing;

//for our theme
using Syncfusion.SfSkinManager;
using Syncfusion.Themes.FluentLight.WPF;

//to start a proccess we need the diagnostics namespace
using System.Diagnostics;

//to work with images
using System.Drawing;

//to save and read files
using System.IO;
using System.Windows;



namespace ImageConverter
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void GetFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            bool? result = openFileDialog.ShowDialog();

            if (result.HasValue && result.Value)
            {
                pathTextBox.Text = openFileDialog.FileName;

            }

        }
            private void OpenFolder(string folderPath)
            {
                ProcessStartInfo startInfo = new ProcessStartInfo()
                {
                    Arguments = folderPath.Substring(0, folderPath.LastIndexOf('\\')),
                    FileName = "explorer.exe"
                };
                Process.Start(startInfo);

            }

        private void Convert_Click(object sender, RoutedEventArgs e)
        {
            if (pathTextBox.Text == String.Empty)
            {

                MessageBox.Show("Please select a file");
                return;

            }

            else
            {
                ConvertPNGToPDF(pathTextBox.Text);
                
                   
            }

            OpenFolder(pathTextBox.Text);
        }



        private void ConvertPNGToPDF(string pngPath)
        {
            PdfDocument pdfDoc = new PdfDocument();
            PdfImage pdfImage = PdfImage.FromStream(new FileStream(pngPath, FileMode.Open));
            PdfPage pdfPage = new PdfPage();
            PdfSection pdfSection = pdfDoc.Sections.Add();
            pdfSection.Pages.Insert(0, pdfPage);
            pdfPage.Graphics.DrawImage(pdfImage, 0, 0);

            string newPNGPath = pngPath.Split('.')[0] + ".pdf";
            pdfDoc.Save(newPNGPath);
            pdfDoc.Close(true);
        }
    }
    }
    
