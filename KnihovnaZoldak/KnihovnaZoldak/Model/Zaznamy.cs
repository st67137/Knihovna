using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;

namespace KnihovnaZoldak.Model
{
    public class Zaznamy<T>
    {
        private LiteCollection<T> collection;
        public Type RecordType { get; }
        public Zaznamy(LiteCollection<T> collection)
        {
            this.collection = collection;
            RecordType = typeof(T);
        }
        public List<T> GetAll()
        {
            return collection.FindAll().ToList();
        }

        public void Pridej(T prvek)
        {
            collection.Insert(prvek);
        }
        public void Odeber(int index)
        {
            collection.Delete(index);
        }
        public void Uprav(T prvek)
        {
            collection.Update(prvek);
        }
    }
}
