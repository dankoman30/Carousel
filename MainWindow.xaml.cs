﻿using MahApps.Metro.IconPacks;
using System.Collections.ObjectModel;
using System.Windows;
using WpfApp1.Models;


namespace WpfApp1
{
    public partial class MainWindow : Window
    {
        public ObservableCollection<CarouselButton> CarouselButtons { get; set; }
        public MainWindow()
        {
            InitializeComponent();

            CarouselButtons = new ObservableCollection<CarouselButton>
            {
                new CarouselButton { Icon = PackIconMaterialKind.Abacus },
                new CarouselButton { Icon = PackIconMaterialKind.TableAccount },
                new CarouselButton { Icon = PackIconMaterialKind.Kangaroo },
                new CarouselButton { Icon = PackIconMaterialKind.BabyBuggy },
                new CarouselButton { Icon = PackIconMaterialKind.CabinAFrame },
                new CarouselButton { Icon = PackIconMaterialKind.DanceBallroom },
                new CarouselButton { Icon = PackIconMaterialKind.FaceAgent },
            };

            DataContext = this;
        }
    }
}
