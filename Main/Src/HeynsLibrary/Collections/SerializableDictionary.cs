using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.Collections.Specialized;
using System.Collections;

namespace HeynsLibrary.Collections
{
    [Serializable]
    [XmlRoot(ElementName = "SerializableDictionary")]
    public class SerializableDictionary<K, V> : List<AssignableKeyValue<K, V>>
    {
        /// <summary>
        /// Synchronizes the threads when adding to the collection
        /// </summary>
        [XmlIgnore]
        private readonly Object _syncLockAdd = new Object();
        /// <summary>
        /// Synchronizes the threads when removing from the collection
        /// </summary>
        [XmlIgnore]
        private readonly Object _syncLockRemove = new Object();
        /// <summary>
        /// Allows for Generic Dictionary behavior
        /// </summary>
        [XmlIgnore]
        private HybridDictionary _dictionary { get; set; }

        /// <summary>
        /// Gets all the Keys associated with this collection
        /// </summary>
        [XmlIgnore]
        public ICollection Keys
        {
            get
            {
                return _dictionary.Keys;
            }
        }

        /// <summary>
        /// Gets all the Values associated with this collection
        /// </summary>
        [XmlIgnore]
        public ICollection Values
        {
            get
            {
                return _dictionary.Values;
            }
        }

        public SerializableDictionary()
        {
            _dictionary = new HybridDictionary();
            if (!typeof(K).CanSerialize())
            {
                throw new ArgumentException(
                    string.Format("Key type {0} is not a Serializable type.",
                    typeof(K).Name));
            }
            if (!typeof(V).CanSerialize())
            {
                throw new ArgumentException(
                    string.Format("Value type {0} is not a Serializable type.",
                    typeof(V).Name));
            }

        }

        public AssignableKeyValue<K, V> this[K key]
        {
            get
            {
                lock (_syncLockAdd)
                {
                    var keyValue = this.Find(x => x.Key.Equals(key));
                    return keyValue;
                }
            }
            set
            {
                lock (_syncLockRemove)
                {
                    if (this[value.Key] != null)
                    {
                        this[value.Key].Value = value.Value;
                        _dictionary.Add(value.Key, value.Value);
                        return;
                    }
                    base.Add(value);
                    _dictionary.Add(value.Key, value.Value);
                }
            }
        }

        public void Add(K key, V value)
        {
            this[key] = new AssignableKeyValue<K, V>
            {
                Key = key,
                Value = value
            };
        }

        public bool Remove(K key, V value)
        {
            var keyValue = new AssignableKeyValue<K, V>
            {
                Key = key,
                Value = value
            };
            if (base.Contains(keyValue))
            {
                _dictionary.Remove(key);
                return base.Remove(keyValue);
            }
            return false;
        }
    }
}