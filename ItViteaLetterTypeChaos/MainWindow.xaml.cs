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
        public int textLength;
        public VHScramble VHost;
        
        public MainWindow()
        {
            InitializeComponent();
            VHost = new VHScramble();
            MyCanvas.Children.Add(VHost);
        }

    
        #region Methods
        //Click method to scramble text. Checks for text length before calling the scramble method.
        private void Btn_Click_Scramble(object sender, RoutedEventArgs e)
        {
            if (BoxTxt.Text.Length <= 0)
                lNotif.Text = "There is no text to scramble.";
            else if (BoxTxt.Text.Length > 3750)
                lNotif.Text = "Text has to be less than 3750 characters.";
            else
            {
                VHost.UpdateScramble(BoxTxt.Text);
                lNotif.Text = "";
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
        //Method to clear the written text easily.
        private void Btn_Click_Clear(object sender, RoutedEventArgs e)
        {
            BoxTxt.Text = null;
        }
        //Method to keep track of the changing text to update the character counter. 
        //So the user can easily see if they are over the character limit.
        private void BoxTxt_TextChanged(object sender, TextChangedEventArgs e)
        {
            textLength = BoxTxt.Text.Length;
            bTextLength.Text = textLength.ToString();
        }
        #endregion


    }
}