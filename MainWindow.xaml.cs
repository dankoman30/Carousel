using System.Windows;
using WpfApp1.ViewModels;

namespace WpfApp1
{
    public partial class MainWindow : Window
    {
        public CarouselViewModel CarouselViewModel { get; set; } = new CarouselViewModel();
        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
        }
    }
}
