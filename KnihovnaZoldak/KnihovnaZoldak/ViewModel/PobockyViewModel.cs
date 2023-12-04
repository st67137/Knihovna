using System.Linq;
using System.Windows.Input;
using System.Windows;
using KnihovnaZoldak.Model;
using System.Text.RegularExpressions;

namespace KnihovnaZoldak.ViewModel
{
    internal class PobockyViewModel : ViewModelBasic
    {

        private Knihovna pobockaKnihovny;
        private string nazev;
        private string adresa;
        private string telefon;
        public ICommand OkCommand { get; private set; }
        public ICommand CancelCommand { get; private set; }

        public PobockyViewModel()
        {
            InitializeCommands();
        }
        public PobockyViewModel(Knihovna pobockaKnihovny)
        {
            NazevPobocky = pobockaKnihovny.Nazev;
            AdresaPobocky = pobockaKnihovny.Adresa;
            TelefonPobocky = pobockaKnihovny.Telefon;
            Pobocka = pobockaKnihovny;
            InitializeCommands();
        }
        public string NazevPobocky
        {
            get { return nazev; }
            set { SetProperty(ref nazev, value); }
        }

        public string AdresaPobocky
        {
            get { return adresa; }
            set { SetProperty(ref adresa, value); }
        }
        public string TelefonPobocky
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
        private bool pridejPobockuUspesne;
        public bool PridejPobockuUspesne
        {
            get { return pridejPobockuUspesne; }
            set { SetProperty(ref pridejPobockuUspesne, value); }
        }
        public Knihovna Pobocka
        {
            get { return pobockaKnihovny; }
            set { SetProperty(ref pobockaKnihovny, value); }
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
                if (!string.IsNullOrEmpty(NazevPobocky) && !string.IsNullOrEmpty(AdresaPobocky) && !string.IsNullOrEmpty(TelefonPobocky))
                {
                    Regex regex = new Regex("^[0-9]+$");
                    bool jeCislo = regex.IsMatch(TelefonPobocky);
                    if (jeCislo)
                    {
                        Pobocka = new Knihovna(NazevPobocky, AdresaPobocky, TelefonPobocky);
                        DialogResult = true;
                        PridejPobockuUspesne = true;
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
