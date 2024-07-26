using System.Collections.ObjectModel;
using WpfApp1.Models;
using WpfApp1.ViewModels.Base;

using MahApps.Metro.IconPacks;

namespace WpfApp1.ViewModels
{
    public class CarouselViewModel:ViewModelBase
    {
        private ObservableCollection<CarouselButton> _buttons = null;
        public ObservableCollection<CarouselButton>? Buttons
        {
            get => _buttons;
            set
            {
                _buttons = value;
                OnPropertyChanged();
            }
        }

        public CarouselViewModel()
        {
            Buttons = new ObservableCollection<CarouselButton>
            {
                new CarouselButton { Icon = PackIconMaterialKind.Abacus },
                new CarouselButton { Icon = PackIconMaterialKind.TableAccount },
                new CarouselButton { Icon = PackIconMaterialKind.Kangaroo },
                new CarouselButton { Icon = PackIconMaterialKind.BabyBuggy },
                new CarouselButton { Icon = PackIconMaterialKind.CabinAFrame },
                new CarouselButton { Icon = PackIconMaterialKind.DanceBallroom },
                new CarouselButton { Icon = PackIconMaterialKind.FaceAgent },
            };
        }
    }
}
