using KnihovnaZoldak.Model;
using LiteDB;
using System;
using System.Windows;

namespace KnihovnaZoldak.ViewModel
{
    internal class DatabaseHelper
    {
        private static LiteDatabase database;

        private DatabaseHelper() {

        }

        public static LiteDatabase GetDatabase()
        {
            if (database == null)
            {
                try
                {
                    string databasePath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "KnihovnaData.db");
                    var mapper = BsonMapper.Global;
                    mapper.Entity<Knihovna>().Id(h => h.Id);
                    mapper.Entity<Kniha>().Id(h => h.Id);
                    mapper.Entity<Zakaznik>().Id(h => h.Id);
                    mapper.Entity<Vypujcka>().Id(h => h.Id);

                    database = new LiteDatabase(databasePath);

                    return database;
                }
                catch (Exception e)
                {
                    MessageBox.Show("Chyba při vytváření databáze: " + e.Message);
                }
            }
            return database;
        }
    }
}
