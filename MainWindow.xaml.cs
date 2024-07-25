using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using System.Windows.Media;

namespace WpfApp1
{
    public partial class MainWindow : Window
    {
        private double ellipseWidth = 300;
        private double ellipseHeight = 200;
        private double ellipseCenterX = 400; // Center of Canvas (Width / 2)
        private double ellipseCenterY = 200; // Center of Canvas (Height / 2)
        private double buttonWidth = 50;
        private double buttonHeight = 25;
        private double scaleFactor = 3;

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
            button.RenderTransform = new ScaleTransform(scale, scale, buttonWidth / scaleFactor, buttonHeight / scaleFactor);

            Canvas.SetLeft(button, x - buttonWidth / 2 * scale);
            Canvas.SetTop(button, y - buttonHeight / 2 * scale);
        }

        private double CalculateScale(double y)
        {
            double minY = ellipseCenterY - ellipseHeight / 2;
            double maxY = ellipseCenterY + ellipseHeight / 2;
            double normalizedY = (y - minY) / (maxY - minY);
            return 0.5 + normalizedY * 0.5; // Scale between 0.5 and 1
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
            double radiusX = ellipseWidth / 2;
            double radiusY = ellipseHeight / 2;

            double fromX = ellipseCenterX + radiusX * Math.Cos(fromAngle * Math.PI / 180);
            double fromY = ellipseCenterY + radiusY * Math.Sin(fromAngle * Math.PI / 180);
            double toX = ellipseCenterX + radiusX * Math.Cos(toAngle * Math.PI / 180);
            double toY = ellipseCenterY + radiusY * Math.Sin(toAngle * Math.PI / 180);

            DoubleAnimation xAnimation = new DoubleAnimation(fromX - buttonWidth / 2, toX - buttonWidth / 2, TimeSpan.FromSeconds(1));
            DoubleAnimation yAnimation = new DoubleAnimation(fromY - buttonHeight / 2, toY - buttonHeight / 2, TimeSpan.FromSeconds(1));

            xAnimation.Completed += (s, e) => Canvas.SetLeft(button, toX - buttonWidth / 2);
            yAnimation.Completed += (s, e) => Canvas.SetTop(button, toY - buttonHeight / 2);

            button.BeginAnimation(Canvas.LeftProperty, xAnimation);
            button.BeginAnimation(Canvas.TopProperty, yAnimation);

            // Scale animation
            double fromScale = CalculateScale(fromY);
            double toScale = CalculateScale(toY);
            DoubleAnimation scaleXAnimation = new DoubleAnimation(fromScale, toScale, TimeSpan.FromSeconds(1));
            DoubleAnimation scaleYAnimation = new DoubleAnimation(fromScale, toScale, TimeSpan.FromSeconds(1));
            ScaleTransform scaleTransform = new ScaleTransform(fromScale, fromScale, buttonWidth / 2, buttonHeight / 2);
            button.RenderTransform = scaleTransform;

            scaleTransform.BeginAnimation(ScaleTransform.ScaleXProperty, scaleXAnimation);
            scaleTransform.BeginAnimation(ScaleTransform.ScaleYProperty, scaleYAnimation);
        }
    }
}
