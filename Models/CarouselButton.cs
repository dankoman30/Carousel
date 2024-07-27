using MahApps.Metro.IconPacks;
using System.Windows.Input;

namespace WpfApp1.Models
{
    public class CarouselButton
    {
        public PackIconMaterialKind Icon { get; set; } = PackIconMaterialKind.None;
        public ICommand? Command { get; set; } = null;
        public string? Text { get; set; } = string.Empty;
    }
}
