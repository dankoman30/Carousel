using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace WpfApp1
{
    public partial class MainWindow : Window
    {
        private double ellipseWidth = 300;
        private double ellipseHeight = 200;
        private double ellipseCenterX = 400; // Center of Canvas (Width / 2)
        private double ellipseCenterY = 200; // Center of Canvas (Height / 2)

        private double maxButtonWidth = 70;
        private double maxButtonHeight = 35;
        private double minButtonWidth = 30;
        private double minButtonHeight = 15;

        private const int NumButtons = 8;
        private const double BottomAngle = 90; // 270 degrees is the bottom of the ellipse

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
            Button clickedButton = (Button)sender;
            int clickedIndex = Array.IndexOf(buttons, clickedButton);

            // Calculate the angle difference to move the clicked button to the bottom
            double angleDifference = BottomAngle - angles[clickedIndex];
            if (angleDifference < 0) angleDifference += 360;

            // Rotate all buttons by the calculated difference
            for (int i = 0; i < NumButtons; i++)
            {
                double newAngle = angles[i] + angleDifference;
                if (newAngle >= 360) newAngle -= 360;
                AnimateButtonToNewPosition(buttons[i], angles[i], newAngle);
                angles[i] = newAngle;
            }
        }

        private void PositionButton(Button button, double angle)
        {
            double radiusX = ellipseWidth / 2;
            double radiusY = ellipseHeight / 2;

            double x = ellipseCenterX + radiusX * Math.Cos(angle * Math.PI / 180);
            double y = ellipseCenterY + radiusY * Math.Sin(angle * Math.PI / 180);

            CalculateButtonSize(y, out double width, out double height);

            button.Width = width;
            button.Height = height;

            Canvas.SetLeft(button, x - (width / 2));
            Canvas.SetTop(button, y - (height / 2));
        }

        private void AnimateButtonToNewPosition(Button button, double fromAngle, double toAngle)
        {
            double radiusX = ellipseWidth / 2;
            double radiusY = ellipseHeight / 2;

            double fromX = ellipseCenterX + radiusX * Math.Cos(fromAngle * Math.PI / 180);
            double fromY = ellipseCenterY + radiusY * Math.Sin(fromAngle * Math.PI / 180);
            double toX = ellipseCenterX + radiusX * Math.Cos(toAngle * Math.PI / 180);
            double toY = ellipseCenterY + radiusY * Math.Sin(toAngle * Math.PI / 180);

            CalculateButtonSize(fromY, out double fromWidth, out double fromHeight);
            CalculateButtonSize(toY, out double toWidth, out double toHeight);

            TimeSpan duration = TimeSpan.FromSeconds(0.5); // Adjust animation duration as needed

            DoubleAnimation xAnimation = new DoubleAnimation(fromX - (fromWidth / 2), toX - (toWidth / 2), duration);
            DoubleAnimation yAnimation = new DoubleAnimation(fromY - (fromHeight / 2), toY - (toHeight / 2), duration);
            DoubleAnimation widthAnimation = new DoubleAnimation(fromWidth, toWidth, duration);
            DoubleAnimation heightAnimation = new DoubleAnimation(fromHeight, toHeight, duration);

            // Add easing function for smoother animation
            xAnimation.EasingFunction = new QuadraticEase();
            yAnimation.EasingFunction = new QuadraticEase();
            widthAnimation.EasingFunction = new QuadraticEase();
            heightAnimation.EasingFunction = new QuadraticEase();

            xAnimation.Completed += (s, e) => Canvas.SetLeft(button, toX - (toWidth / 2));
            yAnimation.Completed += (s, e) => Canvas.SetTop(button, toY - (toHeight / 2));

            button.BeginAnimation(Canvas.LeftProperty, xAnimation);
            button.BeginAnimation(Canvas.TopProperty, yAnimation);
            button.BeginAnimation(Button.WidthProperty, widthAnimation);
            button.BeginAnimation(Button.HeightProperty, heightAnimation);
        }

        private void CalculateButtonSize(double y, out double width, out double height)
        {
            double range = ellipseCenterY * 2; // Total vertical range of the ellipse
            double verticalPosition = y / range; // Normalized vertical position (0 to 1)

            // Calculate width and height based on Y position
            width = minButtonWidth + (maxButtonWidth - minButtonWidth) * verticalPosition;
            height = minButtonHeight + (maxButtonHeight - minButtonHeight) * verticalPosition;
        }
    }
}
