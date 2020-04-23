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
        // This class provides layout, event handling, and container support for
        // the child visual objects.

            // Create a collection of child visual objects.
            private VisualCollection _children;
            public Random rnd;
            public static readonly List<string> FontNames = Fonts.SystemFontFamilies.Select(f => f.Source).ToList();

            public VHScramble()
            {
                rnd = new Random();
                _children = new VisualCollection(this);
                _children.Add(ScrambledText());
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

        //Generates test scrambled text.
        private DrawingVisual ScrambledText()
        {
            // Create an instance of a DrawingVisual.
            DrawingVisual drawingVisual = new DrawingVisual();

            // Retrieve the DrawingContext from the DrawingVisual.
            DrawingContext drawingContext = drawingVisual.RenderOpen();

            string testString = "Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor";

            // Create the initial formatted text string.
            FormattedText formattedText = new FormattedText(
                testString,
                CultureInfo.GetCultureInfo("en-us"),
                FlowDirection.LeftToRight,
                new Typeface("Verdana"),
                32,
                Brushes.Black);

            formattedText.MaxTextWidth = 600;

            //Use an int index to keep track of the location of each char in the string as you change the Fontfamily per char.
            int index = 0;
            foreach (char c in testString)
            {
                formattedText.SetFontFamily(FontNames[rnd.Next(0, FontNames.Count)], index, 1);
                index++;
            }

            // Draw the formatted text string to the DrawingContext of the control.
            drawingContext.DrawText(formattedText, new Point(10, 0));

            // Close the DrawingContext to persist changes to the DrawingVisual.
            drawingContext.Close();

            return drawingVisual;
        }

        //Updates Scrambled text based on input string.
        public void UpdateScramble(string TextString)
        {
            DrawingVisual drawingVisual = new DrawingVisual();
            DrawingContext drawingContext = drawingVisual.RenderOpen();

            // Create the initial formatted text string.
            FormattedText formattedText = new FormattedText(
                TextString,
                CultureInfo.GetCultureInfo("en-us"),
                FlowDirection.LeftToRight,
                new Typeface("Verdana"),
                16,
                Brushes.Black);

            formattedText.MaxTextWidth = 600;

            //Use an int index to keep track of the location of each char in the string as you change the Fontfamily per char.
            int index = 0;
            foreach (char c in TextString)
            {
                formattedText.SetFontFamily(FontNames[rnd.Next(0, FontNames.Count)], index, 1);
                index++;
            }

            // Draw the formatted text string to the DrawingContext of the control.
            drawingContext.DrawText(formattedText, new Point(10, 0));

            // Close the DrawingContext to persist changes to the DrawingVisual.
            drawingContext.Close();

            _children.Clear();
            _children.Add(drawingVisual);

        }
    }
}
