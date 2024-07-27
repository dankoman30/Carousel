﻿using MahApps.Metro.IconPacks;
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
        private double _ellipseWidth;
        private double _ellipseHeight;
        private double _ellipseCenterX;
        private double _ellipseCenterY;
        private double _buttonWidth;
        private double _buttonHeight;
        private const double _defaultCornerRadius = 5;

        private Button[] buttons;
        private double[] angles;

        #region "Observable Collections Dependency Properties"
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

        #endregion

        #region "Double Dependency Properties"
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


        public double CarouselTop
        {
            get { return (double)GetValue(CarouselTopProperty); }
            set { SetValue(CarouselTopProperty, value); }
        }

        public static readonly DependencyProperty CarouselTopProperty =
            DependencyProperty.Register("CarouselTop",
                typeof(double),
                typeof(CarouselUC),
                new PropertyMetadata(0.0, new PropertyChangedCallback(OnCarouselSizeChanged)));

        public double CarouselLeft
        {
            get { return (double)GetValue(CarouselLeftProperty); }
            set { SetValue(CarouselLeftProperty, value); }
        }

        public static readonly DependencyProperty CarouselLeftProperty =
            DependencyProperty.Register("CarouselLeft",
                typeof(double),
                typeof(CarouselUC),
                new PropertyMetadata(0.0, new PropertyChangedCallback(OnCarouselSizeChanged)));

        public double CarouselReflectionOpacity
        {
            get { return (double)GetValue(CarouselReflectionOpacityProperty); }
            set { SetValue(CarouselReflectionOpacityProperty, value); }
        }

        public static readonly DependencyProperty CarouselReflectionOpacityProperty =
            DependencyProperty.Register("CarouselReflectionOpacity",
                typeof(double),
                typeof(CarouselUC),
                new PropertyMetadata(0.25));

        public double CarouselButtonIconWidth
        {
            get { return (double)GetValue(CarouselButtonIconWidthProperty); }
            set { SetValue(CarouselButtonIconWidthProperty, value); }
        }

        public static readonly DependencyProperty CarouselButtonIconWidthProperty =
            DependencyProperty.Register("CarouselButtonIconWidth",
                typeof(double),
                typeof(CarouselUC),
                new PropertyMetadata(20.0));

        public double CarouselButtonIconHeight
        {
            get { return (double)GetValue(CarouselButtonIconHeightProperty); }
            set { SetValue(CarouselButtonIconHeightProperty, value); }
        }

        public static readonly DependencyProperty CarouselButtonIconHeightProperty =
            DependencyProperty.Register("CarouselButtonIconHeight",
                typeof(double),
                typeof(CarouselUC),
                new PropertyMetadata(20.0));

        public double CarouselButtonIconBorderWidth
        {
            get { return (double)GetValue(CarouselButtonIconBorderWidthProperty); }
            set { SetValue(CarouselButtonIconBorderWidthProperty, value); }
        }

        public static readonly DependencyProperty CarouselButtonIconBorderWidthProperty =
            DependencyProperty.Register("CarouselButtonIconBorderWidth",
                typeof(double),
                typeof(CarouselUC),
                new PropertyMetadata(5.0));
        
        #endregion

        #region "Brush Dependency Properties"

        public Brush CarouselButtonBackground
        {
            get { return (Brush)GetValue(CarouselButtonBackgroundProperty); }
            set { SetValue(CarouselButtonBackgroundProperty, value); }
        }

        public static readonly DependencyProperty CarouselButtonBackgroundProperty =
            DependencyProperty.Register("CarouselButtonBackground",
                typeof(Brush),
                typeof(CarouselUC),
                new PropertyMetadata(Brushes.Green));


        public Brush CarouselButtonBorderBrush
        {
            get { return (Brush)GetValue(CarouselButtonBorderBrushProperty); }
            set { SetValue(CarouselButtonBorderBrushProperty, value); }
        }

        public static readonly DependencyProperty CarouselButtonBorderBrushProperty =
            DependencyProperty.Register("CarouselButtonBorderBrush",
                typeof(Brush),
                typeof(CarouselUC),
                new PropertyMetadata(Brushes.DodgerBlue));

        public Brush CarouselButtonMouseOverBackground
        {
            get { return (Brush)GetValue(CarouselButtonMouseOverBackgroundProperty); }
            set { SetValue(CarouselButtonMouseOverBackgroundProperty, value); }
        }

            public static readonly DependencyProperty CarouselButtonMouseOverBackgroundProperty =
            DependencyProperty.Register("CarouselButtonMouseOverBackground",
                typeof(Brush),
                typeof(CarouselUC),
                new PropertyMetadata(Brushes.LightBlue));

        public Brush CarouselButtonIsPressedBackground
        {
            get { return (Brush)GetValue(CarouselButtonIsPressedBackgroundProperty); }
            set { SetValue(CarouselButtonIsPressedBackgroundProperty, value); }
        }

        public static readonly DependencyProperty CarouselButtonIsPressedBackgroundProperty =
            DependencyProperty.Register("CarouselButtonIsPressedBackground",
                typeof(Brush),
                typeof(CarouselUC),
                new PropertyMetadata(Brushes.LightGreen));

        public Brush CarouselButtonIconBackground
        {
            get { return (Brush)GetValue(CarouselButtonIconBackgroundProperty); }
            set { SetValue(CarouselButtonIconBackgroundProperty, value); }
        }

        public static readonly DependencyProperty CarouselButtonIconBackgroundProperty =
            DependencyProperty.Register("CarouselButtonIconBackground",
                typeof(Brush),
                typeof(CarouselUC),
                new PropertyMetadata(Brushes.White,new PropertyChangedCallback(OnCarouselButtonIconBackgroundChanged)));


        public Brush CarouselButtonIconForeground
        {
            get { return (Brush)GetValue(CarouselButtonIconForegroundProperty); }
            set { SetValue(CarouselButtonIconForegroundProperty, value); }
        }

        public static readonly DependencyProperty CarouselButtonIconForegroundProperty =
            DependencyProperty.Register("CarouselButtonIconForeground",
                typeof(Brush),
                typeof(CarouselUC),
                new PropertyMetadata(Brushes.Black, new PropertyChangedCallback(OnCarouselButtonIconBackgroundChanged)));


        public Brush CarouselButtonTextForeground
        {
            get { return (Brush)GetValue(CarouselButtonTextForegroundProperty); }
            set { SetValue(CarouselButtonTextForegroundProperty, value); }
        }

        public static readonly DependencyProperty CarouselButtonTextForegroundProperty =
            DependencyProperty.Register("CarouselButtonTextForeground",
                typeof(Brush),
                typeof(CarouselUC),
                new PropertyMetadata(Brushes.White));


        #endregion

        #region "Color Dependency Property"

        public Color CarouselReflectionGradientStart
        {
            get { return (Color)GetValue(CarouselReflectionGradientStartProperty); }
            set { SetValue(CarouselReflectionGradientStartProperty, value); }
        }

        public static readonly DependencyProperty CarouselReflectionGradientStartProperty =
            DependencyProperty.Register("CarouselReflectionGradientStart",
                typeof(Color),
                typeof(CarouselUC),
                new PropertyMetadata(Colors.White));

        public Color CarouselReflectionGradientEnd
        {
            get { return (Color)GetValue(CarouselReflectionGradientEndProperty); }
            set { SetValue(CarouselReflectionGradientEndProperty, value); }
        }

        public static readonly DependencyProperty CarouselReflectionGradientEndProperty =
            DependencyProperty.Register("CarouselReflectionGradientEnd",
                typeof(Color),
                typeof(CarouselUC),
                new PropertyMetadata(Colors.Transparent));

        #endregion

        public CarouselUC()
        {
            InitializeComponent();
            this.Loaded += CarouselControl_Loaded;
        }

        #region "Carousel Methods"

        private void CarouselControl_Loaded(object sender, RoutedEventArgs e)
        {
            InitializeCarousel();
        }

        private void InitializeCarousel()
        {
            if (CarouselButtons == null) return;

            mainCanvas.Children.Clear();

            SetButtonPositions();
            _buttonHeight = (CarouselButtonIconHeight + CarouselButtonIconBorderWidth);

            int numberOfButtons = CarouselButtons.Count;
            buttons = new Button[numberOfButtons];
            angles = new double[numberOfButtons];

            for (int i = 0; i < numberOfButtons; i++)
            {
                var carouselButton = CarouselButtons[i];
                buttons[i] = new Button
                {
                    Width = _buttonWidth,
                    Height = _buttonHeight,
                    Style = (Style)FindResource("CarouselButtonStyle"),
                    Content = new PackIconMaterial
                    {
                        Kind = carouselButton.Icon,
                        Width = CarouselButtonIconWidth,
                        Height = CarouselButtonIconHeight,
                        Background = CarouselButtonIconBackground
                    },
                    Tag = carouselButton
                };

                angles[i] = i * 360.0 / numberOfButtons;
                buttons[i].Click += Button_Click;
                PositionButton(buttons[i], angles[i]);
                mainCanvas.Children.Add(buttons[i]);
            }
        }

        private void UpdateCarouselSize()
        {
            if (buttons == null) return;

            SetButtonPositions();

            for (int i = 0; i < buttons.Length; i++)
            {
                PositionButton(buttons[i], angles[i]);
            }
        }

        private void SetButtonPositions()
        {
            _ellipseWidth = CarouselWidth;
            _ellipseHeight = CarouselHeight;
            _ellipseCenterX = (((_ellipseWidth / 2) + CarouselLeft) + _buttonWidth);
            _ellipseCenterY = (((_ellipseHeight / 2) + CarouselTop) + _buttonHeight);
            _buttonWidth = (CarouselButtonIconWidth + CarouselButtonIconBorderWidth);
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

                UpdateButton(button, currentAngle);

                step++;
            };
        }

        private void PositionButton(Button? button, double angle)
        {
            if (button == null)
                return;

            UpdateButton(button, angle);
        }

        private void UpdateButton(Button button, double currentAngle)
        {
            double radiusX = _ellipseWidth / 2;
            double radiusY = _ellipseHeight / 2;

            double x = _ellipseCenterX + radiusX * Math.Cos(currentAngle * Math.PI / 180);
            double y = _ellipseCenterY + radiusY * Math.Sin(currentAngle * Math.PI / 180);
            double scale = CalculateScale(y);
            double opacity = CalculateOpacity(y);

            button.Width = _buttonWidth * scale;    // Scale the buttons width
            button.Height = _buttonHeight * scale;  // Scale the buttons height
            button.Opacity = opacity * scale;       // Fade out the buttons at the top

            // Set scale and corner radius
            CarouselProperties.SetScaleFactor(button, scale);
            button.Tag = new CornerRadius(_defaultCornerRadius * Math.Sqrt(scale));

            Canvas.SetLeft(button, x - button.Width / 2);
            Canvas.SetTop(button, y - (button.Height / 2));
        }

        private double CalculateOpacity(double y)
        {
            double minY = _ellipseCenterY - _ellipseHeight / 2;
            double maxY = _ellipseCenterY + _ellipseHeight / 2;
            double normalizedY = (y - minY) / (maxY - minY);
            return 0.6 + normalizedY * 0.4; // Opacity from 0.6 to 1.0
        }
        private double CalculateScale(double y)
        {
            double minY = _ellipseCenterY - _ellipseHeight / 2;
            double maxY = _ellipseCenterY + _ellipseHeight / 2;
            double normalizedY = (y - minY) / (maxY - minY);
            return 0.7 + normalizedY * 0.6; // Scale from 0.7 to 1.3
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


        private static void OnCarouselButtonIconBackgroundChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is CarouselUC carousel)
            {
                carousel.RefreshButtons();
            }
        }
        private void RefreshButtons()
        {
            if (buttons != null)
            {
                foreach (var button in buttons)
                {
                    if (button.Content is PackIconMaterial icon)
                    {
                        icon.Background = CarouselButtonIconBackground;
                        icon.Foreground = CarouselButtonIconForeground;
                    }
                }
            }
        }

        #endregion
    }
}

