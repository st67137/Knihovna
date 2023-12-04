using LiteDB;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using System.Windows;
using KnihovnaZoldak.Model;

namespace KnihovnaZoldak.ViewModel
{
    internal class VypujckyViewModel : ViewModelBasic
    {
        private ObservableCollection<string> knihovnyItems;
        private ObservableCollection<string> knihyItems;
        private ObservableCollection<string> zakazniciItems;
        private string vybranaKnihovna;
        private string vybranaKniha;
        private string vybranyZakaznik;
        public Knihovna knihovna;
        public Kniha kniha;
        public Zakaznik zakaznik;
        private Zaznamy<Knihovna> poleKnihoven;
        private Zaznamy<Kniha> poleKnih;
        private Zaznamy<Zakaznik> poleZakazniku;
        private Vypujcka vypujcenaKniha;
        private bool? dialogResult;
        private bool pridaniVypujcky;

        public ICommand OkCommand { get; private set; }
        public ICommand CancelCommand { get; private set; }

        public ObservableCollection<string> KnihovnyItems
        {
            get { return knihovnyItems; }
            set { SetProperty(ref knihovnyItems, value); }
        }

        public ObservableCollection<string> KnihyItems
        {
            get { return knihyItems; }
            set { SetProperty(ref knihyItems, value); }
        }

        public ObservableCollection<string> ZakazniciItems
        {
            get { return zakazniciItems; }
            set { SetProperty(ref zakazniciItems, value); }
        }

        public string VybranaKnihovna
        {
            get { return vybranaKnihovna; }
            set
            {
                SetProperty(ref vybranaKnihovna, value);
            }
        }

        public string VybranaKniha
        {
            get { return vybranaKniha; }
            set
            {
                SetProperty(ref vybranaKniha, value);
            }
        }

        public string VybranyZakaznik
        {
            get { return vybranyZakaznik; }
            set { SetProperty(ref vybranyZakaznik, value); }
        }
        public bool? DialogResult
        {
            get { return dialogResult; }
            set { SetProperty(ref dialogResult, value); }
        }
        public bool PridaniVypujcky
        {
            get { return pridaniVypujcky; }
            set { SetProperty(ref pridaniVypujcky, value); }
        }
        public Vypujcka VypujcenaKniha
        {
            get { return vypujcenaKniha; }
            set { SetProperty(ref vypujcenaKniha, value); }
        }
        public VypujckyViewModel(Zaznamy<Knihovna> poleKnihoven,Zaznamy<Kniha> poleKnih, Zaznamy<Zakaznik> poleZakazniku)
        {
            this.poleKnihoven = poleKnihoven;
            this.poleZakazniku = poleZakazniku;
            this.poleKnih = poleKnih;
            InitializeCommands();
            InitializeData();
        }
        private void InitializeData()
        {
            KnihovnyItems = new ObservableCollection<string>(poleKnihoven.GetAll().Select(u => u.Nazev));
            KnihyItems = new ObservableCollection<string>();
            foreach (var kniha in poleKnih.GetAll())
            {
                KnihyItems.Add(kniha.Nazev + " (" + kniha.PocetKnih + ")");
            }
            
            ZakazniciItems = new ObservableCollection<string>(poleZakazniku.GetAll().Select(p => p.Prijmeni));
            vybranaKnihovna = KnihovnyItems.FirstOrDefault();
            vybranaKniha = KnihyItems.FirstOrDefault();
            VybranyZakaznik = ZakazniciItems.FirstOrDefault();
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
                string[] parts = VybranaKniha.Split(" (");
                if (parts.Length > 0)
                {
                    VybranaKniha = parts[0];
                }
                knihovna = poleKnihoven.GetAll().FirstOrDefault(k => k.Nazev.Equals(VybranaKnihovna));
                kniha = poleKnih.GetAll().FirstOrDefault(k => k.Nazev.Equals(VybranaKniha));
                zakaznik = poleZakazniku.GetAll().FirstOrDefault(z => z.Prijmeni.Equals(VybranyZakaznik));
                if (knihovna != null && kniha != null && zakaznik != null)
                {
                    if (kniha.PocetKnih > 0)
                    {
                        Vypujcka novaPujcka = new Vypujcka(knihovna, kniha, zakaznik);
                        vypujcenaKniha = novaPujcka;
                        DialogResult = true;
                        PridaniVypujcky = true;
                        var window = Application.Current.Windows.OfType<Window>().SingleOrDefault(w => w.IsActive);
                        window?.Close();
                    }
                    else
                    {
                        MessageBox.Show("Buhužel, kniha není momentálně dostupná");
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

