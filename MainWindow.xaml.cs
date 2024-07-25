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
            double radiusX = ellipse.Width / 2;
            double radiusY = ellipse.Height / 2;
            double centerX = ellipse.Width / 2;
            double centerY = ellipse.Height / 2;

            double x = centerX + radiusX * Math.Cos(angle * Math.PI / 180);
            double y = centerY + radiusY * Math.Sin(angle * Math.PI / 180);

            double scale = 1 - (angle % 360) / 360.0;

            button.RenderTransform = new TranslateTransform(x - button.Width / 2, y - button.Height / 2);
            button.LayoutTransform = new ScaleTransform(scale, scale);
        }

        private void AnimateButtonAlongPath(Button button, double fromAngle, double toAngle)
        {
            double radiusX = ellipse.Width / 2;
            double radiusY = ellipse.Height / 2;
            double centerX = ellipse.Width / 2;
            double centerY = ellipse.Height / 2;

            double fromX = centerX + radiusX * Math.Cos(fromAngle * Math.PI / 180);
            double fromY = centerY + radiusY * Math.Sin(fromAngle * Math.PI / 180);
            double toX = centerX + radiusX * Math.Cos(toAngle * Math.PI / 180);
            double toY = centerY + radiusY * Math.Sin(toAngle * Math.PI / 180);

            TranslateTransform translateTransform = new TranslateTransform(fromX - button.Width / 2, fromY - button.Height / 2);
            button.RenderTransform = translateTransform;

            DoubleAnimation xAnimation = new DoubleAnimation(fromX, toX, TimeSpan.FromSeconds(1));
            DoubleAnimation yAnimation = new DoubleAnimation(fromY, toY, TimeSpan.FromSeconds(1));

            translateTransform.BeginAnimation(TranslateTransform.XProperty, xAnimation);
            translateTransform.BeginAnimation(TranslateTransform.YProperty, yAnimation);

            ScaleTransform scaleTransform = new ScaleTransform(1, 1);
            button.LayoutTransform = scaleTransform;

            DoubleAnimation scaleAnimation = new DoubleAnimation(1, 1 - (toAngle % 360) / 360.0, TimeSpan.FromSeconds(1));
            scaleTransform.BeginAnimation(ScaleTransform.ScaleXProperty, scaleAnimation);
            scaleTransform.BeginAnimation(ScaleTransform.ScaleYProperty, scaleAnimation);
        }

    }
}