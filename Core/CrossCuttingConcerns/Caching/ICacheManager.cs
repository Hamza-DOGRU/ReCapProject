using System;
using System.Collections.Generic;
using System.Text;

namespace Core.CrossCuttingConcerns.Caching
{
    public interface ICacheManager //alternatif cachelere interface
    {
        T Get<T>(string key);
        object Get(string key);//alternatif
        void Add(string key,object value,int duration);
        bool IsAdd(string key);
        void Remove(string key);
        void RemoveByPattern(string pattern);//içinde get olanları cacheden sil vs.

    }
}
