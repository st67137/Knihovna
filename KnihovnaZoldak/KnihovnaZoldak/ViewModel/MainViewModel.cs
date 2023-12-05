using KnihovnaZoldak.Model;
using LiteDB;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace KnihovnaZoldak.ViewModel
{
    internal class MainViewModel : ViewModelBasic
    {
        private ObservableCollection<string> comboBoxKnihovnyItems;
        private ObservableCollection<string> comboBoxKnihyItems;
        private ObservableCollection<string> comboBoxZakazniciItems;
        private string vybranaKnihovna;
        private string vybranaKniha;
        private string vybranyZakaznik;
        private LiteCollection<Knihovna> knihovnyDB;
        private LiteCollection<Kniha> knihyDB;
        private LiteCollection<Zakaznik> zakazniciDB;
        private LiteCollection<Vypujcka> vypujckyDB;
        private LiteDatabase databaze;
        private Zaznamy<Knihovna> poleKnihoven;
        private Zaznamy<Kniha> poleKnih;
        private Zaznamy<Zakaznik> poleZakazniku;
        private Zaznamy<Vypujcka> poleVypujcek;
        private MoznostiEnum moznost = MoznostiEnum.Knihovny;
        private string labelNadpis;
        private readonly string nevybrana = "Všechny";
        public MainViewModel()
        {
            InitializeCommands();
            ListBoxInformaceItems = new ObservableCollection<string>();
            databaze = DatabaseHelper.GetDatabase();
            if (databaze == null) { return; }
            InitializeDatabase();
            Refresh(moznost);
        }
        public string LabelNadpis
        {
            get { return labelNadpis; }
            set { SetProperty(ref labelNadpis, value); }
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
            set
            {
                SetProperty(ref vybranyZakaznik, value);
            }
        }
        private ObservableCollection<string> listBoxInformaceItems;
        public ObservableCollection<string> ListBoxInformaceItems
        {
            get { return listBoxInformaceItems; }
            set { SetProperty(ref listBoxInformaceItems, value); }
        }
        private object selectedListBoxItem;
        public object SelectedListBoxItem
        {
            get { return selectedListBoxItem; }
            set { SetProperty(ref selectedListBoxItem, value); }
        }
        public ICommand ButtonKnihovnyCommand { get; private set; }
        public ICommand ButtonKnihyCommand { get; private set; }
        public ICommand ButtonZakazniciCommand { get; private set; }
        public ICommand ButtonVypujckyCommand { get; private set; }
        public ICommand ButtonPridejCommand { get; private set; }
        public ICommand ButtonUpravCommand { get; private set; }
        public ICommand ButtonOdeberCommand { get; private set; }
        public ICommand ButtonKonecCommand { get; private set; }
        public ICommand ButtonFiltrujCommand { get; private set; }

        public ObservableCollection<string> ComboBoxKnihovnyItems
        {
            get { return comboBoxKnihovnyItems; }
            set { SetProperty(ref comboBoxKnihovnyItems, value); }
        }

        public ObservableCollection<string> ComboBoxKnihyItems
        {
            get { return comboBoxKnihyItems; }
            set { SetProperty(ref comboBoxKnihyItems, value); }
        }
        public ObservableCollection<string> ComboBoxZakazniciItems
        {
            get { return comboBoxZakazniciItems; }
            set { SetProperty(ref comboBoxZakazniciItems, value); }
        }
        private bool buttonUpravIsEnabled;
        public bool ButtonUpravIsEnabled
        {
            get { return buttonUpravIsEnabled; }
            set { SetProperty(ref buttonUpravIsEnabled, value); }
        }
        private Visibility filtrovaniVisibility;
        public Visibility FiltrovaniVisibility
        {
            get { return filtrovaniVisibility; }
            set { SetProperty(ref filtrovaniVisibility, value); }
        }
        private void InitializeCommands()
        {
            ButtonKnihovnyCommand = new RelayCommand(_ => Refresh(MoznostiEnum.Knihovny));
            ButtonKnihyCommand = new RelayCommand(_ => Refresh(MoznostiEnum.Knihy));
            ButtonZakazniciCommand = new RelayCommand(_ => Refresh(MoznostiEnum.Zakaznici));
            ButtonVypujckyCommand = new RelayCommand(_ => Refresh(MoznostiEnum.Vypujcky));
            ButtonPridejCommand = new RelayCommand(_ => ButtonNovy_Click(moznost));
            ButtonUpravCommand = new RelayCommand(_ => ButtonUprav_Click(moznost));
            ButtonOdeberCommand = new RelayCommand(_ => ButtonSmazat_Click(moznost));
            ButtonKonecCommand = new RelayCommand(_ => ButtonKonec_Click());
            ButtonFiltrujCommand = new RelayCommand(_ => ButtonFiltr_Click());
        }
        private void InitializeDatabase()
        {
            try
            {
                knihovnyDB = (LiteCollection<Knihovna>)databaze.GetCollection<Knihovna>("KnihovnyDB");
                poleKnihoven = new Zaznamy<Knihovna>(knihovnyDB);

                knihyDB = (LiteCollection<Kniha>)databaze.GetCollection<Kniha>("KnihyDB");
                poleKnih = new Zaznamy<Kniha>(knihyDB);

                zakazniciDB = (LiteCollection<Zakaznik>)databaze.GetCollection<Zakaznik>("ZakazniciDB");
                poleZakazniku = new Zaznamy<Zakaznik>(zakazniciDB);

                vypujckyDB = (LiteCollection<Vypujcka>)databaze.GetCollection<Vypujcka>("VypujckyDB");
                poleVypujcek = new Zaznamy<Vypujcka>(vypujckyDB);

            }
            catch (Exception e)
            {
                MessageBox.Show("Chyba při vytváření databáze: " + e.Message);
            }

        }

        private void ButtonNovy_Click(MoznostiEnum moznost)
        {
            switch (moznost)
            {
                case MoznostiEnum.Knihovny:
                    pridejPobocku();
                    break;
                case MoznostiEnum.Knihy:
                    pridejKnihu();
                    break;
                case MoznostiEnum.Zakaznici:
                    pridejZakaznika();
                    break;
                case MoznostiEnum.Vypujcky:
                    pridejVypujcku();
                    break;
            }
            Refresh(moznost);
        }

        private void ButtonSmazat_Click(MoznostiEnum moznost)
        {
            switch (moznost)
            {
                case MoznostiEnum.Knihovny:
                    poleKnihoven.Odeber(najdiPrvek());
                    break;
                case MoznostiEnum.Knihy:
                    poleKnih.Odeber(najdiPrvek());
                    break;
                case MoznostiEnum.Zakaznici:
                    poleZakazniku.Odeber(najdiPrvek());
                    break;
                case MoznostiEnum.Vypujcky:
                    Vypujcka? odebranaVypujcka = poleVypujcek.GetAll().FirstOrDefault(h => h.Id == najdiPrvek());
                    if (odebranaVypujcka != null)
                    {
                        var zakaznik = poleZakazniku.GetAll().FirstOrDefault(h => h.Id == odebranaVypujcka.VybranyZakaznik.Id);
                        if (zakaznik != null)
                        {
                            zakaznik.PocetVypujcek--;
                            poleZakazniku.Uprav(zakaznik);
                        }
                        var kniha = poleKnih.GetAll().FirstOrDefault(h => h.Id == odebranaVypujcka.VybranaKniha.Id);
                        if (kniha != null)
                        {
                            kniha.PocetKnih++;
                            poleKnih.Uprav(kniha);
                        }
                        poleVypujcek.Odeber(odebranaVypujcka.Id);
                    }
                    break;
            }
            Refresh(moznost);
        }
        private int najdiPrvek()
        {
            if (SelectedListBoxItem != null)
            {
                var vybranyPrvek = SelectedListBoxItem.ToString();
                string[] parts = vybranyPrvek.Split(new char[] { ':' }, StringSplitOptions.RemoveEmptyEntries);
                if (parts.Length > 0 && int.TryParse(parts[parts.Length - 1].Trim(), out int prvekID))
                {
                    return prvekID;
                }
            }
            return 0;
        }

        private void ButtonKonec_Click()
        {
            MessageBoxResult result = MessageBox.Show("Jsi si jistý, že chceš aplikaci ukončit?", "Potvrzení ukončení", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                Application.Current.Shutdown();
            }
        }

        private void ButtonUprav_Click(MoznostiEnum moznost)
        {
            switch (moznost)
            {
                case MoznostiEnum.Knihovny:
                    var pobockaUprava = poleKnihoven.GetAll().FirstOrDefault(h => h.Id == najdiPrvek());
                    if (pobockaUprava != null)
                    {
                        upravPobocku(pobockaUprava.Id);
                    }
                    break;
                case MoznostiEnum.Knihy:
                    var knihaUprava = poleKnih.GetAll().FirstOrDefault(h => h.Id == najdiPrvek());
                    if (knihaUprava != null)
                    {
                        upravKnihu(knihaUprava.Id);
                    }
                    break;
                case MoznostiEnum.Zakaznici:
                    var zakaznikUprava = poleZakazniku.GetAll().FirstOrDefault(h => h.Id == najdiPrvek());
                    if (zakaznikUprava != null)
                    {
                        upravZakaznika(zakaznikUprava.Id);
                    }
                    break;
            }
            Refresh(moznost);
        }
        private void pridejPobocku()
        {
            PobockyViewModel pobockaVM = new PobockyViewModel();
            PobockyWindow dialogPobocka = new PobockyWindow { DataContext = pobockaVM };

            bool? dialogResultPobocka = dialogPobocka.ShowDialog();

            if (pobockaVM.PridejPobockuUspesne)
            {
                poleKnihoven.Pridej(pobockaVM.Pobocka);
            }
        }
        private void pridejKnihu()
        {
            KnihyViewModel knihaVM = new KnihyViewModel();
            KnihyWindow dialogKniha = new KnihyWindow { DataContext = knihaVM };

            bool? dialogResultKniha = dialogKniha.ShowDialog();

            if (knihaVM.PridaniKnihy)
            {
                poleKnih.Pridej(knihaVM.Kniha);
            }
        }
        private void pridejZakaznika()
        {
            ZakazniciViewModel zakazniciVM = new ZakazniciViewModel();
            ZakazniciWindow dialogZakaznik = new ZakazniciWindow { DataContext = zakazniciVM };

            bool? dialogResultZakaznik = dialogZakaznik.ShowDialog();

            if (zakazniciVM.PridaniZakaznika)
            {
                poleZakazniku.Pridej(zakazniciVM.Zakaznik);
            }
        }
        private void pridejVypujcku()
        {
            VypujckyViewModel vypujckyVM = new VypujckyViewModel(poleKnihoven, poleKnih, poleZakazniku);
            VypujckyWindow vypujcky = new VypujckyWindow(poleKnihoven, poleKnih, poleZakazniku) { DataContext = vypujckyVM };
            bool? dialogResultVypujcka = vypujcky.ShowDialog();
            if (vypujckyVM.PridaniVypujcky)
            {
                poleVypujcek.Pridej(vypujckyVM.VypujcenaKniha);
                poleZakazniku.Uprav(vypujckyVM.zakaznik);
                poleKnih.Uprav(vypujckyVM.kniha);
            }
        }
        private void upravPobocku(int idPobocky)
        {
            var upravenaPobocka = poleKnihoven.GetAll().FirstOrDefault(p => p.Id == idPobocky);

            if (upravenaPobocka != null)
            {
                PobockyViewModel upravPobockuVM = new PobockyViewModel(upravenaPobocka);
                PobockyWindow updatePobocky = new PobockyWindow { DataContext = upravPobockuVM };
                bool? dialogResultPobocka = updatePobocky.ShowDialog();

                if (upravPobockuVM.PridejPobockuUspesne)
                {
                    upravenaPobocka = upravPobockuVM.Pobocka;
                    upravenaPobocka.Id = idPobocky;
                    foreach (var vypujcka in poleVypujcek.GetAll().Where(p => p.VybranaKnihovna.Id == upravenaPobocka.Id))
                    {
                        vypujcka.VybranaKnihovna.Nazev = upravPobockuVM.NazevPobocky;
                        poleVypujcek.Uprav(vypujcka);
                    }
                    poleKnihoven.Uprav(upravenaPobocka);
                }
            }
        }
        private void upravKnihu(int idKnihy)
        {
            var upravenaKniha = poleKnih.GetAll().FirstOrDefault(p => p.Id == idKnihy);

            if (upravenaKniha != null)
            {
                KnihyViewModel upravKnihuVM = new KnihyViewModel(upravenaKniha);
                KnihyWindow upravKnihu = new KnihyWindow { DataContext = upravKnihuVM };
                bool? dialogResultKniha = upravKnihu.ShowDialog();

                if (upravKnihuVM.PridaniKnihy)
                {
                    upravenaKniha = upravKnihuVM.Kniha;
                    upravenaKniha.Id = idKnihy;
                    foreach (var vypujcka in poleVypujcek.GetAll().Where(p => p.VybranaKniha.Id == upravenaKniha.Id))
                    {
                        vypujcka.VybranaKniha.Nazev = upravKnihuVM.NazevKnihy;
                        poleVypujcek.Uprav(vypujcka);
                    }
                    poleKnih.Uprav(upravenaKniha);
                }
            }
        }
        private void upravZakaznika(int idZakaznika)
        {
            var upravenyZakaznik = poleZakazniku.GetAll().FirstOrDefault(p => p.Id == idZakaznika);

            if (upravenyZakaznik != null)
            {
                ZakazniciViewModel upravZakaznikaVM = new ZakazniciViewModel(upravenyZakaznik);
                ZakazniciWindow upravZakaznika = new ZakazniciWindow { DataContext = upravZakaznikaVM };
                bool? dialogResultKniha = upravZakaznika.ShowDialog();

                if (upravZakaznikaVM.PridaniZakaznika)
                {
                    upravenyZakaznik = upravZakaznikaVM.Zakaznik;
                    upravenyZakaznik.Id = idZakaznika;
                    foreach (var vypujcka in poleVypujcek.GetAll().Where(p => p.VybranyZakaznik.Id == upravenyZakaznik.Id))
                    {
                        vypujcka.VybranyZakaznik.Prijmeni = upravZakaznikaVM.PrijmeniZakaznika;
                        poleVypujcek.Uprav(vypujcka);
                    }
                    poleZakazniku.Uprav(upravenyZakaznik);
                }
            }
        }

        private void ButtonFiltr_Click()
        {
            listBoxInformaceItems.Clear();
            var vypujcky = poleVypujcek.GetAll();
            foreach (var vypujcka in vypujcky)
            {
                bool filtrKnihovna = VybranaKnihovna.Equals(nevybrana) || VybranaKnihovna.Equals(vypujcka.VybranaKnihovna.Nazev);
                bool filtrKniha = VybranaKniha.Equals(nevybrana) || VybranaKniha.Equals(vypujcka.VybranaKniha.Nazev);
                bool filtrZakaznik = VybranyZakaznik.Equals(nevybrana) || VybranyZakaznik.Equals(vypujcka.VybranyZakaznik.Prijmeni);

                if (filtrKnihovna && filtrKniha && filtrZakaznik)
                {
                    listBoxInformaceItems.Add(vypujcka.ToString());
                }
            }
        }
        private void ZobrazFiltr()
        {
            ButtonUpravIsEnabled = false;
            FiltrovaniVisibility = Visibility.Visible;
        }
        private void SkryjFiltr()
        {
            FiltrovaniVisibility = Visibility.Collapsed;
            ButtonUpravIsEnabled = true;
        }
        private void Refresh(MoznostiEnum moznost)
        {
            ListBoxInformaceItems.Clear();
            this.moznost = moznost;

            switch (moznost)
            {
                case MoznostiEnum.Knihovny:
                    LabelNadpis = "Pobočky Knihovny";
                    SkryjFiltr();
                    foreach (var knihovna in poleKnihoven.GetAll())
                    {
                        ListBoxInformaceItems.Add(knihovna.ToString());
                    }

                    break;
                case MoznostiEnum.Knihy:
                    LabelNadpis = "Databáze Knih";
                    foreach (var kniha in poleKnih.GetAll())
                    {
                        ListBoxInformaceItems.Add(kniha.ToString());
                    }
                    SkryjFiltr();
                    break;
                case MoznostiEnum.Zakaznici:
                    LabelNadpis = "Zákazníci";
                    foreach (var zakaznik in poleZakazniku.GetAll())
                    {
                        ListBoxInformaceItems.Add(zakaznik.ToString());
                    }
                    SkryjFiltr();
                    break;
                case MoznostiEnum.Vypujcky:
                    ZobrazFiltr();
                    LabelNadpis = "Aktivní Výpůjčky";
                    foreach (var vypujcka in poleVypujcek.GetAll())
                    {
                        ListBoxInformaceItems.Add(vypujcka.ToString());
                    }
                    break;
            }
            ComboBoxKnihovnyItems = new ObservableCollection<string>();
            ComboBoxKnihyItems = new ObservableCollection<string>();
            ComboBoxZakazniciItems = new ObservableCollection<string>();
            ComboBoxKnihovnyItems.Add(nevybrana);
            ComboBoxKnihyItems.Add(nevybrana);
            ComboBoxZakazniciItems.Add(nevybrana);
            foreach (var knihovna in poleKnihoven.GetAll())
            {
                ComboBoxKnihovnyItems.Add(knihovna.Nazev);
            }
            foreach (var kniha in poleKnih.GetAll())
            {
                ComboBoxKnihyItems.Add(kniha.Nazev);
            }
            foreach (var zakaznik in poleZakazniku.GetAll())
            {
                ComboBoxZakazniciItems.Add(zakaznik.Prijmeni);
            }
            if (ComboBoxKnihovnyItems.Count > 0)
            {
                VybranaKnihovna = ComboBoxKnihovnyItems.FirstOrDefault();
            }
            if (ComboBoxKnihyItems.Count > 0)
            {
                VybranaKniha = ComboBoxKnihyItems.FirstOrDefault();
            }
            if (ComboBoxZakazniciItems.Count > 0)
            {
                VybranyZakaznik = ComboBoxZakazniciItems.FirstOrDefault();
            }
        }
    }
}
