using LiteDB;
using System;

namespace KnihovnaZoldak.Model
{
    public class Vypujcka
    {
        [BsonId] public int Id { get; set; }
        public Knihovna VybranaKnihovna { get; set; }
        public Kniha VybranaKniha { get; set; }
        public Zakaznik VybranyZakaznik { get; set; }
        public Vypujcka() { }
        public Vypujcka(Knihovna vybranaKnihovna, Kniha vybranaKniha, Zakaznik vybranyZakaznik)
        {
            VybranaKnihovna = vybranaKnihovna;
            VybranaKniha = vybranaKniha;
            VybranyZakaznik = vybranyZakaznik;
            vybranyZakaznik.PocetVypujcek++;
            vybranaKniha.PocetKnih--;
        }

        public override string? ToString()
        {
            return string.Format("{0,-20} | {1,-27} |Zákazník: {2,12} | ID: {3}", VybranaKnihovna.Nazev, VybranaKniha.Nazev, VybranyZakaznik.Prijmeni, Id);
        }
    }
}
