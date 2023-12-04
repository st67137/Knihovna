using LiteDB;
using System;

namespace KnihovnaZoldak.Model
{
    public class Kniha
    {
        [BsonId] public int Id { get; set; }
        public string Nazev { get; set; }
        public int PocetStran { get; set; }
        public int PocetKnih { get; set; }

        public Kniha(string nazev, int pocetStran, int pocetKnih)
        {
            Nazev = nazev;
            PocetStran = pocetStran;
            PocetKnih = pocetKnih;
        }
        public override string? ToString()
        {
            return string.Format("{0,-30} | stran: {1,-8} | počet knih: {2,10} | ID: {3}", Nazev, PocetStran, PocetKnih, Id);
        }
    }
}
