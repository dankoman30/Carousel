using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private double ellipseWidth = 300;
        private double ellipseHeight = 200;
        private double maxButtonWidth = 50;
        private double minButtonWidth = 100;
        private double maxButtonHeight = 25;
        private double minButtonHeight = 50;
        private double ellipseCenterX = 150;
        private double ellipseCenterY = 100;

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
                MoveButtonAlongPath(buttons[i], angles[i]);
            }

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button selectedButton = sender as Button;
            int selectedIndex = Array.IndexOf(buttons, selectedButton);

            for (int i = 0; i < NumButtons; i++)
            {
                double newAngle = angles[i] - selectedIndex * 360.0 / NumButtons;
                if (newAngle < 0) newAngle += 360;
                AnimateButtonAlongPath(buttons[i], angles[i], newAngle);
                angles[i] = newAngle;
            }
        }

        private void MoveButtonAlongPath(Button button, double angle)
        {
            double radiusX = ellipseWidth / 2;
            double radiusY = ellipseHeight / 2;

            double x = ellipseCenterX + radiusX * Math.Cos(angle * Math.PI / 180);
            double y = ellipseCenterY + radiusY * Math.Sin(angle * Math.PI / 180);

            double scale = 1 - (angle % 360) / 360.0;
            double width = maxButtonWidth - (maxButtonWidth - minButtonWidth) * scale;
            double height = maxButtonHeight - (maxButtonHeight - minButtonHeight) * scale;

            button.RenderTransform = new TranslateTransform(x - width / 2, y - height / 2);
            button.Width = width;
            button.Height = height;
        }

        private void AnimateButtonAlongPath(Button button, double fromAngle, double toAngle)
        {
            double radiusX = ellipseWidth / 2;
            double radiusY = ellipseHeight / 2;

            double fromX = ellipseCenterX + radiusX * Math.Cos(fromAngle * Math.PI / 180);
            double fromY = ellipseCenterY + radiusY * Math.Sin(fromAngle * Math.PI / 180);
            double toX = ellipseCenterX + radiusX * Math.Cos(toAngle * Math.PI / 180);
            double toY = ellipseCenterY + radiusY * Math.Sin(toAngle * Math.PI / 180);

            TranslateTransform translateTransform = new TranslateTransform(fromX - button.Width / 2, fromY - button.Height / 2);
            button.RenderTransform = translateTransform;

            DoubleAnimation xAnimation = new DoubleAnimation(fromX, toX, TimeSpan.FromSeconds(1));
            DoubleAnimation yAnimation = new DoubleAnimation(fromY, toY, TimeSpan.FromSeconds(1));

            translateTransform.BeginAnimation(TranslateTransform.XProperty, xAnimation);
            translateTransform.BeginAnimation(TranslateTransform.YProperty, yAnimation);

            DoubleAnimation widthAnimation = new DoubleAnimation(button.Width, maxButtonWidth - (maxButtonWidth - minButtonWidth) * (1 - (toAngle % 360) / 360.0), TimeSpan.FromSeconds(1));
            DoubleAnimation heightAnimation = new DoubleAnimation(button.Height, maxButtonHeight - (maxButtonHeight - minButtonHeight) * (1 - (toAngle % 360) / 360.0), TimeSpan.FromSeconds(1));

            button.BeginAnimation(Button.WidthProperty, widthAnimation);
            button.BeginAnimation(Button.HeightProperty, heightAnimation);
        }

        private void UpdateButtonPosition(Button button, double angle)
        {
            double radiusX = ellipseWidth / 2;
            double radiusY = ellipseHeight / 2;

            double radian = angle * Math.PI / 180;

            double x = radiusX * Math.Cos(radian);
            double y = radiusY * Math.Sin(radian);

            // Adjust scale calculation to make bottom center (270 degrees) the largest
            double scale = (Math.Cos(radian - Math.PI / 2) + 1) / 2;
            double width = minButtonWidth + (maxButtonWidth - minButtonWidth) * scale;
            double height = minButtonHeight + (maxButtonHeight - minButtonHeight) * scale;

            button.Width = width;
            button.Height = height;

            Canvas.SetLeft(button, ellipseCenterX + x - width / 2);
            Canvas.SetTop(button, ellipseCenterY + y - height / 2);
            Panel.SetZIndex(button, (int)(1000 * (1 - scale))); // Highest Z-index for the bottom button
        }
    }
}
