using MahApps.Metro.IconPacks;
using System.Windows.Controls;

namespace WpfApp1.Models
{
    public class CarouselButton : Button
    {
        public PackIconMaterialKind Icon { get; set; } = PackIconMaterialKind.None;
        public string? Text { get; set; } = string.Empty;
    }
}
