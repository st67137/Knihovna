using LiteDB;
using System;
using System.Text.RegularExpressions;

namespace KnihovnaZoldak.Model
{
    public class Zakaznik
    {
        [BsonId] public int Id { get; set; }
        public string Jmeno { get; set; }
        public string Prijmeni { get; set; }
        public string Telefon { get; set; }
        public int PocetVypujcek { get; set; }

        public Zakaznik(string jmeno, string prijmeni, string telefon)
        {
            Jmeno = jmeno;
            Prijmeni = prijmeni;
            Telefon = telefon;
            PocetVypujcek = 0;
        }
        public override string? ToString()
        {
            return string.Format("{0,-12} | {1,-12} | telefon: {2,10} | počet výpůjček: {3,3} | ID: {4}", Jmeno, Prijmeni, Telefon, PocetVypujcek, Id);
        }
    }
}
