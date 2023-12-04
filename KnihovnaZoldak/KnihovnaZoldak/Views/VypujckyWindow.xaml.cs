using KnihovnaZoldak.Model;
using KnihovnaZoldak.ViewModel;
using System.Windows;

namespace KnihovnaZoldak
{
    /// <summary>
    /// Interakční logika pro VypujckyWindow.xaml
    /// </summary>
    public partial class VypujckyWindow : Window
    {
        private VypujckyViewModel viewModel;
        public VypujckyWindow(Zaznamy<Knihovna> knihovny, Zaznamy<Kniha> knihy, Zaznamy<Zakaznik> zakaznici)
        {
            InitializeComponent();
            viewModel = new VypujckyViewModel(knihovny,knihy,zakaznici);
            DataContext = viewModel;
        }
    }
}
