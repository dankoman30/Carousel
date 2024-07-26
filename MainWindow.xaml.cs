using MahApps.Metro.IconPacks;
using System.Collections.ObjectModel;
using System.Windows;
using WpfApp1.Commands.Base;
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
                new CarouselButton { Icon = PackIconMaterialKind.Abacus, Command = new CommandsBase(abacusExecute) },
                new CarouselButton { Icon = PackIconMaterialKind.TableAccount, Command = new CommandsBase(tableAccountExecute) },
                new CarouselButton { Icon = PackIconMaterialKind.Kangaroo, Command = new CommandsBase(kangarooExecute) },
                new CarouselButton { Icon = PackIconMaterialKind.BabyBuggy, Command = new CommandsBase(babyBuggyExecute) },
                new CarouselButton { Icon = PackIconMaterialKind.CabinAFrame, Command = new CommandsBase(cabinAFrameExecute) },
                new CarouselButton { Icon = PackIconMaterialKind.DanceBallroom, Command = new CommandsBase(danceBallRoomExecute) },
                new CarouselButton { Icon = PackIconMaterialKind.FaceAgent, Command = new CommandsBase(fanceAgentExecute) },
            };

            DataContext = this;
        }

        private void abacusExecute(object? obj)
        {
            MessageBox.Show($"Abacus");
        }

        private void fanceAgentExecute(object? obj)
        {
            MessageBox.Show($"FanceAgent");
        }

        private void danceBallRoomExecute(object? obj)
        {
            MessageBox.Show($"DanceBallRoom");
        }

        private void cabinAFrameExecute(object? obj)
        {
            MessageBox.Show($"CabinAFrame");
        }

        private void babyBuggyExecute(object? obj)
        {
            MessageBox.Show($"BabyBuggy");
        }

        private void kangarooExecute(object? obj)
        {
            MessageBox.Show($"Kangaroo");
        }

        private void tableAccountExecute(object? obj)
        {
            MessageBox.Show($"TableAccount");
        }
    }
}
