using KnihovnaZoldak.Model;
using KnihovnaZoldak.ViewModel;
using System.Windows;

namespace KnihovnaZoldak
{
    /// <summary>
    /// Interakční logika pro PobockyWindow.xaml
    /// </summary>
    public partial class PobockyWindow : Window
    {
        private PobockyViewModel viewModel;

        public PobockyWindow()
        {
            InitializeComponent();
        }
        public PobockyWindow(Knihovna pobocka)
        {
            InitializeComponent();
            viewModel = new PobockyViewModel();
            DataContext = viewModel;
        }
    }
}
