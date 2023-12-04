using KnihovnaZoldak.Model;
using KnihovnaZoldak.ViewModel;
using System.Windows;

namespace KnihovnaZoldak
{
    public partial class KnihyWindow : Window
    {
        private KnihyViewModel viewModel;

        public KnihyWindow()
        {
            InitializeComponent();
        }
        public KnihyWindow(Kniha kniha)
        {
            InitializeComponent();
            viewModel = new KnihyViewModel();
            DataContext = viewModel;
        }
    }
}
