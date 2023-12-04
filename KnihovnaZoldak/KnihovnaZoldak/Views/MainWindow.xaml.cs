using KnihovnaZoldak.ViewModel;
using System.Windows;

namespace KnihovnaZoldak
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class PujcovnaKnihWindow : Window
    {
        private MainViewModel viewModel;

        public PujcovnaKnihWindow()
        {
            InitializeComponent();
            viewModel = new MainViewModel();
            DataContext = viewModel;
        }

        private void ViewModel_ItemFoundEvent(object sender, int index)
        {
            listBoxInformace.SelectedIndex = index;
        }
    }
}
