using KnihovnaZoldak.Model;
using System.Linq;
using System.Windows.Input;
using System.Windows;

namespace KnihovnaZoldak.ViewModel
{
    internal class KnihyViewModel : ViewModelBasic
    {

        private Kniha kniha;
        private string nazev;
        private int strany;
        private int pocet;
        public ICommand OkCommand { get; private set; }
        public ICommand CancelCommand { get; private set; }

        public KnihyViewModel()
        {
            InitializeCommands();
        }
        public KnihyViewModel(Kniha kniha)
        {
            NazevKnihy = kniha.Nazev;
            StranyKnihy = kniha.PocetStran;
            PocetKnih = kniha.PocetKnih;
            Kniha = kniha;
            InitializeCommands();
        }
        public string NazevKnihy
        {
            get { return nazev; }
            set { SetProperty(ref nazev, value); }
        }

        public int StranyKnihy
        {
            get { return strany; }
            set { SetProperty(ref strany, value); }
        }
        public int PocetKnih
        {
            get { return pocet; }
            set { SetProperty(ref pocet, value); }
        }
        private bool? dialogResult;
        public bool? DialogResult
        {
            get { return dialogResult; }
            set { SetProperty(ref dialogResult, value); }
        }
        private bool pridaniKnihy;
        public bool PridaniKnihy
        {
            get { return pridaniKnihy; }
            set { SetProperty(ref pridaniKnihy, value); }
        }
        public Kniha Kniha
        {
            get { return kniha; }
            set { SetProperty(ref kniha, value); }
        }
        private void InitializeCommands()
        {
            OkCommand = new RelayCommand(_ => Ok());
            CancelCommand = new RelayCommand(_ => Cancel());
        }

        private void Ok()
        {
            try
            {
                if (!string.IsNullOrEmpty(NazevKnihy) && StranyKnihy > 0 && PocetKnih > 0)
                {
                    Kniha = new Kniha(NazevKnihy, StranyKnihy, PocetKnih);
                    DialogResult = true;
                    PridaniKnihy = true;
                    var window = Application.Current.Windows.OfType<Window>().SingleOrDefault(w => w.IsActive);
                    window?.Close();
                }
                else
                {
                    MessageBox.Show("Špatně zadané hodnoty!");
                }
            }
            catch
            {
                MessageBox.Show("Špatně zadané hodnoty!");
            }
        }

        private void Cancel()
        {
            DialogResult = false;
            var window = Application.Current.Windows.OfType<Window>().SingleOrDefault(w => w.IsActive);
            window?.Close();
        }
    }
}
