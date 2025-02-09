using KnightsOfHerman.Common.Abstract;
using KnightsOfHerman.Common.Abstract.Modification;
using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace KnightsOfHerman.Common.Collections
{
    /// <summary>
    /// A Dictionary that can be subscribed to be notified of pathed changes
    /// </summary>
    /// <typeparam name="Tkey">Is Unique no matter what</typeparam>
    /// <typeparam name="Tvalue">Value</typeparam>
    public class TrackedDictionary<Tkey, Tvalue> : IDictionary<Tkey, Tvalue>, INotifyModificationPath, IDictionary
        where Tkey : notnull
    {
        /// <summary>
        /// Default Constructor of an empty dictionary
        /// </summary>
        public TrackedDictionary()
        {
            _dictionary = new Dictionary<Tkey, Tvalue>();
        }

        /// <summary>
        /// Constructs 
        /// </summary>
        public TrackedDictionary(IDictionary<Tkey,Tvalue> dict)
        {
            _dictionary = new Dictionary<Tkey, Tvalue>(dict);
            foreach(var kvp in _dictionary)
            {
                if(kvp.Value is INotifyModificationPath tracked)
                    tracked.OnTrackedModification += (x, y) => OnEntryChanged(kvp.Key, y);
            }
        }

        public bool Modified { get; set; }

        public ICollection<Tkey> Keys => _dictionary.Keys;

        public ICollection<Tvalue> Values => _dictionary.Values;
        public int Count => _dictionary.Count;


        public bool IsReadOnly => false;

        public bool IsFixedSize => false;

        ICollection IDictionary.Keys => _dictionary.Keys;

        ICollection IDictionary.Values => _dictionary.Values;

        public bool IsSynchronized => false;

        public object SyncRoot => throw new NotImplementedException();

        public object? this[object key]
        {
            get => _dictionary[(Tkey)key];
            set => _dictionary[(Tkey)key] = (Tvalue)value;
        }

        public Tvalue this[Tkey key]
        {
            get => _dictionary[key];
            set
            {
                if(value is INotifyModificationPath trackedValue)
                {
                    trackedValue.OnTrackedModification += (x, y) => OnEntryChanged(key, y);
                }
                _dictionary[key] = value;
                OnTrackedModification?.Invoke(this, new TrackedModificationEventArgs(key.ToString(), value, EditActions.Add));
            }
        }

        void OnEntryChanged(Tkey key, TrackedModificationEventArgs args)
        {
            OnTrackedModification?.Invoke(this, args.PrependPath(key.ToString()));
            Modified = true;            
        }

        public event TrackedModificationEventHandler? OnTrackedModification;

        Dictionary<Tkey, Tvalue> _dictionary;

        public void Add(Tkey key, Tvalue value)
        {
            this[key] = value;
        }

        public bool ContainsKey(Tkey key) => _dictionary.ContainsKey(key);


        public bool Remove(Tkey key)
        {
            var ret = _dictionary.Remove(key, out var value);
            if (ret)
            {
                OnTrackedModification?.Invoke(this, new TrackedModificationEventArgs(key.ToString(), value, EditActions.Remove));
            }
            return ret;
        }
        

        public bool TryGetValue(Tkey key, [MaybeNullWhen(false)] out Tvalue value) => _dictionary.TryGetValue(key, out value);


        public void Add(KeyValuePair<Tkey, Tvalue> item)
        {
            Add(item.Key, item.Value);
        }

        public void Clear()
        {
            _dictionary.Clear();
            OnTrackedModification?.Invoke(this, new TrackedModificationEventArgs("", null, EditActions.Clear));
        }

        public bool Contains(KeyValuePair<Tkey, Tvalue> item)
        {
            return _dictionary.ContainsKey(item.Key);
        }

        public void CopyTo(KeyValuePair<Tkey, Tvalue>[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public bool Remove(KeyValuePair<Tkey, Tvalue> item)
        {
            return _dictionary.Remove(item.Key);
        }

        public IEnumerator<KeyValuePair<Tkey, Tvalue>> GetEnumerator()
        {
            return _dictionary.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Add(object key, object? value)
        {
            Add((Tkey)key, (Tvalue)value);
        }

        public bool Contains(object key)
        {
            //Infinite Loop...
            return ContainsKey((Tkey)key);
        }

        IDictionaryEnumerator IDictionary.GetEnumerator()
        {
            return (IDictionaryEnumerator)GetEnumerator();
        }

        public void Remove(object key)
        {
            Remove((Tkey)key);
        }

        public void CopyTo(Array array, int index)
        {
            throw new NotImplementedException();
        }
    }
}
