using KnihovnaZoldak.Model;
using System.Linq;
using System.Windows.Input;
using System.Windows;
using System.Text.RegularExpressions;

namespace KnihovnaZoldak.ViewModel
{
    internal class ZakazniciViewModel : ViewModelBasic
    {
        private Zakaznik zakaznik;
        private string jmeno;
        private string prijmeni;
        private string telefon;
        public ICommand OkCommand { get; private set; }
        public ICommand CancelCommand { get; private set; }

        public ZakazniciViewModel()
        {
            InitializeCommands();
        }
        public ZakazniciViewModel(Zakaznik zakaznik)
        {
            JmenoZakaznika = zakaznik.Jmeno;
            PrijmeniZakaznika = zakaznik.Prijmeni;
            TelefonZakaznika = zakaznik.Telefon;
            Zakaznik = zakaznik;
            InitializeCommands();
        }
        public string JmenoZakaznika
        {
            get { return jmeno; }
            set { SetProperty(ref jmeno, value); }
        }

        public string PrijmeniZakaznika
        {
            get { return prijmeni; }
            set { SetProperty(ref prijmeni, value); }
        }
        public string TelefonZakaznika
        {
            get { return telefon; }
            set { SetProperty(ref telefon, value); }
        }
        private bool? dialogResult;
        public bool? DialogResult
        {
            get { return dialogResult; }
            set { SetProperty(ref dialogResult, value); }
        }
        private bool pridaniZakaznika;
        public bool PridaniZakaznika
        {
            get { return pridaniZakaznika; }
            set { SetProperty(ref pridaniZakaznika, value); }
        }
        public Zakaznik Zakaznik
        {
            get { return zakaznik; }
            set { SetProperty(ref zakaznik, value); }
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
                if (!string.IsNullOrEmpty(JmenoZakaznika) && !string.IsNullOrEmpty(PrijmeniZakaznika) && !string.IsNullOrEmpty(TelefonZakaznika))
                {
                    Regex regex = new Regex("^[0-9]+$");
                    bool jeCislo = regex.IsMatch(TelefonZakaznika);
                    if (jeCislo)
                    {
                        Zakaznik = new Zakaznik(JmenoZakaznika, PrijmeniZakaznika, TelefonZakaznika);
                        DialogResult = true;
                        PridaniZakaznika = true;
                        var window = Application.Current.Windows.OfType<Window>().SingleOrDefault(w => w.IsActive);
                        window?.Close();
                    }
                    else
                    {
                        MessageBox.Show("Špatně zadané hodnoty!");
                    }
                   
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
