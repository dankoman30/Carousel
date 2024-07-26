using MahApps.Metro.IconPacks;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using WpfApp1.Models;

namespace WpfApp1.UserControls
{
    public partial class CarouselUC : UserControl
    {
        private double ellipseWidth;
        private double ellipseHeight;
        private double ellipseCenterX;
        private double ellipseCenterY;
        private double buttonWidth;
        private double buttonHeight;
        private const double DefaultCornerRadius = 5;

        private Button[] buttons;
        private double[] angles;

        public ObservableCollection<CarouselButton> CarouselButtons
        {
            get { return (ObservableCollection<CarouselButton>)GetValue(CarouselButtonsProperty); }
            set { SetValue(CarouselButtonsProperty, value); }
        }

        public static readonly DependencyProperty CarouselButtonsProperty =
            DependencyProperty.Register("CarouselButtons",
                typeof(ObservableCollection<CarouselButton>),
                typeof(CarouselUC),
                new PropertyMetadata(null, new PropertyChangedCallback(OnCarouselButtonsChanged)));

        private static void OnCarouselButtonsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is CarouselUC carousel)
            {
                if (e.OldValue is ObservableCollection<CarouselButton> oldCollection)
                {
                    oldCollection.CollectionChanged -= carousel.CarouselButtons_CollectionChanged;
                }

                if (e.NewValue is ObservableCollection<CarouselButton> newCollection)
                {
                    newCollection.CollectionChanged += carousel.CarouselButtons_CollectionChanged;
                }

                carousel.InitializeCarousel();
            }
        }

        private void CarouselButtons_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            InitializeCarousel();
        }

        private static void OnCarouselSizeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is CarouselUC carousel)
            {
                carousel.UpdateCarouselSize();
            }
        }

        public double CarouselWidth
        {
            get { return (double)GetValue(CarouselWidthProperty); }
            set { SetValue(CarouselWidthProperty, value); }
        }

        public static readonly DependencyProperty CarouselWidthProperty =
            DependencyProperty.Register("CarouselWidth",
                typeof(double),
                typeof(CarouselUC),
                new PropertyMetadata(400.0, new PropertyChangedCallback(OnCarouselSizeChanged)));

        private void UpdateCarouselSize()
        {
            if(buttons == null) return;

            ellipseWidth = CarouselWidth;
            ellipseHeight = CarouselHeight;
            ellipseCenterX = ActualWidth / 2;
            ellipseCenterY = ActualHeight / 2;

            for (int i = 0; i < buttons.Length; i++)
            {
                PositionButton(buttons[i], angles[i]);
            }
        }

        public double CarouselHeight
        {
            get { return (double)GetValue(CarouselHeightProperty); }
            set { SetValue(CarouselHeightProperty, value); }
        }

        public static readonly DependencyProperty CarouselHeightProperty =
            DependencyProperty.Register("CarouselHeight",
                typeof(double),
                typeof(CarouselUC),
                new PropertyMetadata(100.0, new PropertyChangedCallback(OnCarouselSizeChanged)));

        public CarouselUC()
        {
            InitializeComponent();
            this.Loaded += CarouselControl_Loaded;
        }

        private void CarouselControl_Loaded(object sender, RoutedEventArgs e)
        {
            InitializeCarousel();
        }

        private void InitializeCarousel()
        {
            if (CarouselButtons == null) return;

            mainCanvas.Children.Clear();

            ellipseWidth = CarouselWidth;
            ellipseHeight = CarouselHeight;
            ellipseCenterX = ActualWidth / 2;
            ellipseCenterY = ActualHeight / 2;
            buttonWidth = 50;
            buttonHeight = 25;

            int numberOfButtons = CarouselButtons.Count;
            buttons = new Button[numberOfButtons];
            angles = new double[numberOfButtons];

            for (int i = 0; i < numberOfButtons; i++)
            {
                var carouselButton = CarouselButtons[i];
                buttons[i] = new Button
                {
                    Width = buttonWidth,
                    Height = buttonHeight,
                    Style = (Style)FindResource("CarouselButtonStyle"),
                    Content = new PackIconMaterial
                    {
                        Kind = carouselButton.Icon,
                        Width = 20,
                        Height = 20
                    }
                };

                angles[i] = i * 360.0 / numberOfButtons;
                buttons[i].Click += Button_Click;
                PositionButton(buttons[i], angles[i]);
                mainCanvas.Children.Add(buttons[i]);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button? clickedButton = sender as Button;
            if (clickedButton == null) return;

            int clickedIndex = Array.IndexOf(buttons, clickedButton);
            double targetAngle = 90; // Target bottom position angle
            double currentAngle = angles[clickedIndex];

            // Check if the clicked button is already at the bottom (90 degrees)
            if (Math.Abs(currentAngle - targetAngle) < 0.1) // Use a small threshold for floating-point comparison
            {
                // Execute the command associated with the clicked button
                var carouselButton = CarouselButtons[clickedIndex];
                if (carouselButton.Command != null && carouselButton.Command.CanExecute(null))
                {
                    carouselButton.Command.Execute(null);
                }
            }
            else
            {
                // Rotate the carousel as before
                double rotationAngle = targetAngle - currentAngle;
                if (rotationAngle < 0) rotationAngle += 360;
                RotateCarousel(rotationAngle);
            }
        }

        private void RotateCarousel(double rotationAngle)
        {
            for (int i = 0; i < buttons.Length; i++)
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
                double opacity = CalculateOpacity(y);

                button.Width = buttonWidth * scale;
                button.Height = buttonHeight * scale;
                button.Opacity = opacity;

                // Set scale and corner radius
                CarouselProperties.SetScaleFactor(button, scale);
                button.Tag = new CornerRadius(DefaultCornerRadius * Math.Sqrt(scale));

                Canvas.SetLeft(button, x - button.Width / 2);
                Canvas.SetTop(button, y - button.Height / 2);

                step++;
            };
        }

        private void PositionButton(Button? button, double angle)
        {
            if (button == null)
                return;

            double radiusX = ellipseWidth / 2;
            double radiusY = ellipseHeight / 2;

            double x = ellipseCenterX + radiusX * Math.Cos(angle * Math.PI / 180);
            double y = ellipseCenterY + radiusY * Math.Sin(angle * Math.PI / 180);

            double scale = CalculateScale(y);
            double opacity = CalculateOpacity(y);

            button.Width = buttonWidth * scale;
            button.Height = buttonHeight * scale;
            button.Opacity = opacity;

            // Set scale and corner radius
            CarouselProperties.SetScaleFactor(button, scale);
            button.Tag = new CornerRadius(DefaultCornerRadius * Math.Sqrt(scale));

            Canvas.SetLeft(button, x - button.Width / 2);
            Canvas.SetTop(button, y - button.Height / 2);
        }

        private double CalculateOpacity(double y)
        {
            double minY = ellipseCenterY - ellipseHeight / 2;
            double maxY = ellipseCenterY + ellipseHeight / 2;
            double normalizedY = (y - minY) / (maxY - minY);
            return 0.6 + normalizedY * 0.4; // Opacity from 0.6 to 1.0
        }

        private double CalculateScale(double y)
        {
            double minY = ellipseCenterY - ellipseHeight / 2;
            double maxY = ellipseCenterY + ellipseHeight / 2;
            double normalizedY = (y - minY) / (maxY - minY);
            return 0.7 + normalizedY * 0.6; // Scale from 0.7 to 1.3
        }
    }
}

