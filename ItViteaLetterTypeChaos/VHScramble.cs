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
   public class VHScramble : FrameworkElement, INotifyPropertyChanged
    {
        // Create a host visual derived from the FrameworkElement class.
        // This class provides layout, event handling, and container support for
        // the child visual objects.

            // Create a collection of child visual objects.
            private VisualCollection _children;
            private string _textString;
            public Random rnd;
            public static readonly List<string> FontNames = Fonts.SystemFontFamilies.Select(f => f.Source).ToList();

            public VHScramble()
            {
                //FillFontList();
                rnd = new Random();
                _children = new VisualCollection(this);
                _children.Add(CreateDrawingVisualRectangle());
                _children.Add(CreateDrawingVisualText());
                _children.Add(CreateDrawingVisualEllipses());


                // Add the event handler for MouseLeftButtonUp.
                MouseLeftButtonUp += MyVisualHost_MouseLeftButtonUp;
            }

            public string TextString
            {
                get { return _textString; }
                set
                {
                    _textString = value;
                    OnPropertyChanged("TextString");
                }
            }


            // Capture the mouse event and hit test the coordinate point value against
            // the child visual objects.
            private void MyVisualHost_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
            {
                // Retreive the coordinates of the mouse button event.
                Point pt = e.GetPosition((UIElement)sender);

                // Initiate the hit test by setting up a hit test result callback method.
                VisualTreeHelper.HitTest(this, null, MyCallback, new PointHitTestParameters(pt));
            }

            // If a child visual object is hit, toggle its opacity to visually indicate a hit.
            public HitTestResultBehavior MyCallback(HitTestResult result)
            {
                if (result.VisualHit.GetType() == typeof(DrawingVisual))
                {
                    ((DrawingVisual)result.VisualHit).Opacity =
                        ((DrawingVisual)result.VisualHit).Opacity == 1.0 ? 0.4 : 1.0;
                }

                // Stop the hit test enumeration of objects in the visual tree.
                return HitTestResultBehavior.Stop;
            }

            // Create a DrawingVisual that contains a rectangle.
            private DrawingVisual CreateDrawingVisualRectangle()
            {
                DrawingVisual drawingVisual = new DrawingVisual();

                // Retrieve the DrawingContext in order to create new drawing content.
                DrawingContext drawingContext = drawingVisual.RenderOpen();

                // Create a rectangle and draw it in the DrawingContext.
                Rect rect = new Rect(new Point(160, 100), new Size(320, 80));
                drawingContext.DrawRectangle(Brushes.LightBlue, null, rect);

                // Persist the drawing content.
                drawingContext.Close();

                return drawingVisual;
            }

            // Create a DrawingVisual that contains text.
            private DrawingVisual CreateDrawingVisualText()
            {
                // Create an instance of a DrawingVisual.
                DrawingVisual drawingVisual = new DrawingVisual();

                // Retrieve the DrawingContext from the DrawingVisual.
                DrawingContext drawingContext = drawingVisual.RenderOpen();

#pragma warning disable CS0618 // 'FormattedText.FormattedText(string, CultureInfo, FlowDirection, Typeface, double, Brush)' is obsolete: 'Use the PixelsPerDip override'
                // Draw a formatted text string into the DrawingContext.
                drawingContext.DrawText(
                    new FormattedText("Click Me!",
                        CultureInfo.GetCultureInfo("en-us"),
                        FlowDirection.LeftToRight,
                        new Typeface("Verdana"),
                        36, Brushes.Black),
                    new Point(200, 116));
#pragma warning enable CS0618 // 'FormattedText.FormattedText(string, CultureInfo, FlowDirection, Typeface, double, Brush)' is obsolete: 'Use the PixelsPerDip override'

                // Close the DrawingContext to persist changes to the DrawingVisual.
                drawingContext.Close();

                return drawingVisual;
            }

            // Create a DrawingVisual that contains an ellipse.
            private DrawingVisual CreateDrawingVisualEllipses()
            {
                DrawingVisual drawingVisual = new DrawingVisual();
                DrawingContext drawingContext = drawingVisual.RenderOpen();

                drawingContext.DrawEllipse(Brushes.Maroon, null, new Point(430, 136), 20, 20);
                drawingContext.Close();

                return drawingVisual;
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

            //Test method.
            protected override void OnRender(DrawingContext drawingContext)
            {
                string testString = "Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor";

                // Create the initial formatted text string.
                FormattedText formattedText = new FormattedText(
                    testString,
                    CultureInfo.GetCultureInfo("en-us"),
                    FlowDirection.LeftToRight,
                    new Typeface("Verdana"),
                    32,
                    Brushes.Black);

                // Set a maximum width and height. If the text overflows these values, an ellipsis "..." appears.
                formattedText.MaxTextWidth = 400;
                formattedText.MaxTextHeight = 240;

                // Use a larger font size beginning at the first (zero-based) character and continuing for 5 characters.
                // The font size is calculated in terms of points -- not as device-independent pixels.
                formattedText.SetFontSize(36 * (96.0 / 72.0), 0, 5);

                // Use a Bold font weight beginning at the 6th character and continuing for 11 characters.
                formattedText.SetFontWeight(FontWeights.Bold, 6, 11);

                // Use a linear gradient brush beginning at the 6th character and continuing for 11 characters.
                formattedText.SetForegroundBrush(
                                        new LinearGradientBrush(
                                        Colors.Orange,
                                        Colors.Teal,
                                        90.0),
                                        6, 11);

                int index = 0;
                foreach (char c in testString)
                {
                    formattedText.SetFontFamily(FontNames[rnd.Next(0, testString.Length)], index, 1);
                    index++;
                }

                // Use an Italic font style beginning at the 28th character and continuing for 28 characters.
                formattedText.SetFontStyle(FontStyles.Italic, 28, 28);

                // Draw the formatted text string to the DrawingContext of the control.
                drawingContext.DrawText(formattedText, new Point(10, 0));
            }

            #region INotifyPropertyChanged Members  
            public event PropertyChangedEventHandler PropertyChanged;
            private void OnPropertyChanged(string propertyName)
            {
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
                }
            }
            #endregion
        
    }
}
