using KnihovnaZoldak.Model;
using KnihovnaZoldak.ViewModel;
using System.Windows;

namespace KnihovnaZoldak
{
    /// <summary>
    /// Interakční logika pro ZakazniciWindow.xaml
    /// </summary>
    public partial class ZakazniciWindow : Window
    {
        private ZakazniciViewModel viewModel;

        public ZakazniciWindow()
        {
            InitializeComponent();
        }
        public ZakazniciWindow(Zakaznik zakaznik)
        {
            InitializeComponent();
            viewModel = new ZakazniciViewModel();
            DataContext = viewModel;
        }
    }
}
