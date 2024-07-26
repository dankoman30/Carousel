using System.Collections.ObjectModel;
using WpfApp1.Models;
using WpfApp1.ViewModels.Base;

using MahApps.Metro.IconPacks;
using System.Windows;
using WpfApp1.Commands.Base;

namespace WpfApp1.ViewModels
{
    public class CarouselViewModel : ViewModelBase
    {
        private ObservableCollection<CarouselButton>? _carouselButtons = null;
        public ObservableCollection<CarouselButton>? CarouselButtons
        {
            get => _carouselButtons;
            set
            {
                _carouselButtons = value;
                OnPropertyChanged();
            }
        }

        public CarouselViewModel()
        {
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
