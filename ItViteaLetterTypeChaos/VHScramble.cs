using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace ItViteaLetterTypeChaos
{
   public class VHScramble : FrameworkElement
    {
        // Create a host visual derived from the FrameworkElement class.
        // This class provides layout, event handling, and container support for the child visual objects.

            // Create a collection of child visual objects.
            //A Random class for generating random numbers. And a static list of all the FontFamilies.
            private VisualCollection _children;
            public Random rnd;
            public static readonly List<string> FontNames = Fonts.SystemFontFamilies.Select(f => f.Source).ToList();
            private static string testString = "Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor";

        public VHScramble()
        {
            rnd = new Random();
            _children = new VisualCollection(this);
            UpdateScramble(testString);
        }

        // Provide a required override for the VisualChildrenCount property.
        protected override int VisualChildrenCount => _children.Count;

            // Provide a required override for the GetVisualChild method.
            protected override Visual GetVisualChild(int index)
            {
                if (index < 0 || index >= _children.Count)
                {
                    throw new ArgumentOutOfRangeException();
                }

                return _children[index];
            }

        //Generates Scrambled text based on input string.
        public void UpdateScramble(string TextString)
        {
            DrawingVisual drawingVisual = new DrawingVisual();
            DrawingContext drawingContext = drawingVisual.RenderOpen();

            // Create the initial formatted text string.
            FormattedText formattedText = new FormattedText(
                TextString,
                CultureInfo.GetCultureInfo("en-us"),
                FlowDirection.LeftToRight,
                new Typeface("Segoe UI"),
                16,
                Brushes.Black);
            
            //Use an int index to keep track of the location of each char in the string as you change the Fontfamily per char.
            int index = 0;
            foreach (char c in TextString)
            {
                formattedText.SetFontFamily(FontNames[rnd.Next(0, FontNames.Count)], index, 1);
                index++;
            }
            //Set the max width to prevent text going out of bounds. 
            //Set the Height to the height of the formattedText to allow for the scrollbar to work.
            formattedText.MaxTextWidth = 640;
            Height = formattedText.Height;

            // Draw the formatted text string to the DrawingContext of the control.
            drawingContext.DrawText(formattedText, new Point(0, 0));


            // Close the DrawingContext to persist changes to the DrawingVisual.
            drawingContext.Close();

            //Clear the prior text and add the newly generated drawingVisual.
            _children.Clear();
            _children.Add(drawingVisual);
        }
    }
}
