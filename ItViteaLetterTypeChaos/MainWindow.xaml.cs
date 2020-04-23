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
        public VHScramble VHost;
        public MainWindow()
        {
            InitializeComponent();
            VHost = new VHScramble();
            MyCanvas.Children.Add(VHost);
            
        }
        //This region is for all methods relating to manipulating text and aren't directly tied to objects in the view.
        #region Support Methods



        #endregion

        //This region is specifically for all methods linked to buttons.
        #region Button Methods

        private void Btn_Click_Scramble(object sender, RoutedEventArgs e)
        {
            if (BoxTxt.Text.Length < 0)
                lNotif.Text = "There is no text to scramble.";
            else if (BoxTxt.Text.Length > 800)
                lNotif.Text = "Text has to be less than 800 characters.";
            else
            {
                lNotif.Text = "Scrambling.. ";
                VHost.UpdateScramble(BoxTxt.Text);
                lNotif.Text = "Text succecfully scrambled.";
            }
        }

        //Import method so one can import already written texts.
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

        
        #endregion
    }

}