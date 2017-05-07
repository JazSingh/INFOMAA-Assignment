﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace INFOMAA_Assignment
{
    public class ActionSet : IDictionary<int, int>
    {
        Dictionary<int, int> actionPayoff;

        public ActionSet()
        {
            actionPayoff = new Dictionary<int, int>();
        }

        public ActionSet CleanCopy()
        {
            ActionSet cleanCopy = new ActionSet();
            foreach (int action in actionPayoff.Values)
            {
                cleanCopy.Add(action, 0);
            }
            return cleanCopy;
        }

        public KeyValuePair<int, int> GetBestAction()
        {
            throw new NotImplementedException(); //TODO
        }

        public int this[int key]
        {
            get
            {
                return actionPayoff[key];
            }

            set
            {
                actionPayoff[key] = value;
            }
        }

        public int Count
        {
            get
            {
                return actionPayoff.Count;
            }
        }

        public bool IsReadOnly
        {
            get
            {
                return false;
            }
        }

        public ICollection<int> Keys
        {
            get
            {
                return actionPayoff.Keys;
            }
        }

        public ICollection<int> Values
        {
            get
            {
                return actionPayoff.Values;
            }
        }

        public void Add(KeyValuePair<int, int> item)
        {
            Add(item.Key, item.Value);
        }

        public void Add(int key, int value)
        {
            actionPayoff.Add(key, value);
        }

        public void Clear()
        {
            actionPayoff.Clear();
        }

        public bool Contains(KeyValuePair<int, int> item)
        {
            return actionPayoff.Contains(item);
        }

        public bool ContainsKey(int key)
        {
            return actionPayoff.ContainsKey(key);
        }

        public void CopyTo(KeyValuePair<int, int>[] array, int arrayIndex)
        {
            foreach (KeyValuePair<int, int> kvp in actionPayoff)
            {
                array[arrayIndex++] = kvp;
            }
        }

        public IEnumerator<KeyValuePair<int, int>> GetEnumerator()
        {
            return actionPayoff.GetEnumerator();
        }

        public bool Remove(KeyValuePair<int, int> item)
        {
            return Remove(item.Key);
        }

        public bool Remove(int key)
        {
            return actionPayoff.Remove(key);
        }

        public bool TryGetValue(int key, out int value)
        {
            return actionPayoff.TryGetValue(key, out value);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
