using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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
using System.Windows.Controls.Primitives;
using System.Windows.Media.TextFormatting;
using System.Globalization;
using System.ComponentModel;

namespace ItViteaLetterTypeChaos
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

        private void WindowLoaded(object sender, EventArgs e)
        {
            var VHost = new VHScramble();
            MyCanvas.Children.Add(VHost);
        }

        //public variables.
        public string textPlain, textChaos;

        //This region is for all methods relating to manipulating text and aren't directly tied to objects in the view.
        #region Support Methods


       
        #endregion

        //This region is specifically for all methods linked to buttons.
        #region Button Methods
        private void Btn_Click_Import(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            if (openFileDialog.ShowDialog() == true)
            {
                BoxTxt.Text = File.ReadAllText(openFileDialog.FileName);
            }
        }

        private void Btn_Click_Export(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            saveFileDialog.Filter = "Text file (*.txt)|*.txt";
            if (saveFileDialog.ShowDialog() == true)
                File.WriteAllText(saveFileDialog.FileName, BoxTxt.Text);
        }
        #endregion
    }

}