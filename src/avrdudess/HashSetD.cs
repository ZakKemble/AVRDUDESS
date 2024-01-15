// AVRDUDESS - A GUI for AVRDUDE
// https://blog.zakkemble.net/avrdudess-a-gui-for-avrdude/
// https://github.com/ZakKemble/AVRDUDESS
// Copyright (C) 2024, Zak Kemble
// GNU GPL v3 (see License.txt)

using System.Collections;
using System.Collections.Generic;

namespace avrdudess
{
    // .NET 2.0 doesn't have HashSet
    public class HashSetD<T> : IEnumerable<T>
    {
        private readonly Dictionary<T, bool> dict = new Dictionary<T, bool>();
        
        public Dictionary<T, bool>.KeyCollection Keys { get => dict.Keys; }

        public HashSetD() { }

        public void Add(T item) { dict[item] = true; }
        public void Add(List<T> items)
        {
            items.ForEach(x =>
            {
                try { Add(x); }
                catch { }
            });
        }

        public void Clear() { dict.Clear(); }

        public bool Contains(T item) {  return dict.ContainsKey(item); }

        public IEnumerator<T> GetEnumerator() { return dict.Keys.GetEnumerator(); }

        IEnumerator IEnumerable.GetEnumerator() { return GetEnumerator(); }
    }
}
