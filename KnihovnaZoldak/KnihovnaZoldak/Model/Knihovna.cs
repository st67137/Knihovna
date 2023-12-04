using LiteDB;
using System;
using System.Text.RegularExpressions;

namespace KnihovnaZoldak.Model
{
    public class Knihovna
    {
        [BsonId] public int Id { get; set; }
        public string Nazev { get; set; }
        public string Adresa { get; set; }
        public string Telefon { get; set; }

        public Knihovna(string nazev, string adresa, string telefon)
        {
            Nazev = nazev;
            Adresa = adresa;
            Telefon = telefon;
        }

        public override string? ToString()
        {
            return string.Format("{0,-25}|Adresa: {1,-20}|telefon: {2,10} | Id: {3}", Nazev, Adresa, Telefon, Id);
        }
    }
}
