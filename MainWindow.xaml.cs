using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace WpfApp1
{
    public partial class MainWindow : Window
    {
        private double ellipseWidth = 300;
        private double ellipseHeight = 100; // Changed from 200 to 100
        private double ellipseCenterX = 400; // Center of Canvas (Width / 2)
        private double ellipseCenterY = 150; // Changed from 200 to 150 (assuming canvas height is 300)
        private double buttonWidth = 50;
        private double buttonHeight = 25;
        private const double DefaultCornerRadius = 5; // Adjust this value as needed

        private const int NumButtons = 8;
        private Button[] buttons = new Button[NumButtons];
        private double[] angles = new double[NumButtons];

        public MainWindow()
        {
            InitializeComponent();

            buttons[0] = button1;
            buttons[1] = button2;
            buttons[2] = button3;
            buttons[3] = button4;
            buttons[4] = button5;
            buttons[5] = button6;
            buttons[6] = button7;
            buttons[7] = button8;

            for (int i = 0; i < NumButtons; i++)
            {
                angles[i] = i * 360.0 / NumButtons;
                buttons[i].Click += Button_Click;
                PositionButton(buttons[i], angles[i]);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button clickedButton = sender as Button;
            int clickedIndex = Array.IndexOf(buttons, clickedButton);
            double targetAngle = 90; // Target bottom position angle
            double currentAngle = angles[clickedIndex];

            double rotationAngle = targetAngle - currentAngle;
            if (rotationAngle < 0) rotationAngle += 360;

            RotateCarousel(rotationAngle);
        }

        private void PositionButton(Button button, double angle)
        {
            double radiusX = ellipseWidth / 2;
            double radiusY = ellipseHeight / 2;

            double x = ellipseCenterX + radiusX * Math.Cos(angle * Math.PI / 180);
            double y = ellipseCenterY + radiusY * Math.Sin(angle * Math.PI / 180);

            double scale = CalculateScale(y);

            button.Width = buttonWidth * scale;
            button.Height = buttonHeight * scale;

            // Adjust corner radius
            button.Template = CreateButtonTemplate(scale);

            Canvas.SetLeft(button, x - button.Width / 2);
            Canvas.SetTop(button, y - button.Height / 2);
        }

        private double CalculateScale(double y)
        {
            double minY = ellipseCenterY - ellipseHeight / 2;
            double maxY = ellipseCenterY + ellipseHeight / 2;
            double normalizedY = (y - minY) / (maxY - minY);
            return 0.5 + normalizedY * 1.0; // Scale from 0.5 to 1.5
        }

        private void RotateCarousel(double rotationAngle)
        {
            for (int i = 0; i < NumButtons; i++)
            {
                double newAngle = angles[i] + rotationAngle;
                if (newAngle >= 360) newAngle -= 360;
                AnimateButtonToNewPosition(buttons[i], angles[i], newAngle);
                angles[i] = newAngle;
            }
        }

        private void AnimateButtonToNewPosition(Button button, double fromAngle, double toAngle)
        {
            double durationSeconds = 0.5;
            int steps = 35;
            double deltaAngle;

            // Normalize angles to be between 0 and 360
            fromAngle = (fromAngle % 360 + 360) % 360;
            toAngle = (toAngle % 360 + 360) % 360;

            // Calculate both clockwise and counterclockwise distances
            double clockwiseDistance = (toAngle - fromAngle + 360) % 360;
            double counterclockwiseDistance = (fromAngle - toAngle + 360) % 360;

            // Choose the shorter path
            if (clockwiseDistance <= counterclockwiseDistance)
            {
                // Clockwise rotation
                deltaAngle = clockwiseDistance / steps;
            }
            else
            {
                // Counterclockwise rotation
                deltaAngle = -counterclockwiseDistance / steps;
            }

            double durationPerStep = durationSeconds / steps;

            int step = 0;
            CompositionTarget.Rendering += (s, e) =>
            {
                if (step > steps)
                {
                    CompositionTarget.Rendering -= null;
                    return;
                }

                double currentAngle = fromAngle + deltaAngle * step;
                currentAngle = (currentAngle % 360 + 360) % 360; // Normalize to 0-360 range

                double radiusX = ellipseWidth / 2;
                double radiusY = ellipseHeight / 2;

                double x = ellipseCenterX + radiusX * Math.Cos(currentAngle * Math.PI / 180);
                double y = ellipseCenterY + radiusY * Math.Sin(currentAngle * Math.PI / 180);
                double scale = CalculateScale(y);

                button.Width = buttonWidth * scale;
                button.Height = buttonHeight * scale;

                // Adjust corner radius
                button.Template = CreateButtonTemplate(scale);

                Canvas.SetLeft(button, x - button.Width / 2);
                Canvas.SetTop(button, y - button.Height / 2);

                step++;
            };
        }

        private ControlTemplate CreateButtonTemplate(double scale)
        {
            double cornerRadius = DefaultCornerRadius * Math.Sqrt(scale);

            var template = new ControlTemplate(typeof(Button));
            var border = new FrameworkElementFactory(typeof(Border));
            border.SetValue(Border.CornerRadiusProperty, new CornerRadius(cornerRadius));
            border.SetValue(Border.BackgroundProperty, new TemplateBindingExtension(Button.BackgroundProperty));

            var contentPresenter = new FrameworkElementFactory(typeof(ContentPresenter));
            contentPresenter.SetValue(ContentPresenter.HorizontalAlignmentProperty, HorizontalAlignment.Center);
            contentPresenter.SetValue(ContentPresenter.VerticalAlignmentProperty, VerticalAlignment.Center);

            border.AppendChild(contentPresenter);
            template.VisualTree = border;

            return template;
        }
    }
}
