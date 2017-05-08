﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace INFOMAA_Assignment
{
    public class ActionSet : IDictionary<int, int>
    {
        Dictionary<int, int> _actionPayoff;
        int _numActions;

        public ActionSet(int numActions)
        {
            _numActions = numActions;
            _actionPayoff = new Dictionary<int, int>();
            int direction = 0;
            int step = 360 / numActions;
            while (direction < 360)
            {
                _actionPayoff.Add(direction, 0);
                direction += step;
            }
        }

        public int NumActions => _numActions;

        public ActionSet CleanCopy()
        {
            ActionSet cleanCopy = new ActionSet(_numActions);
            return cleanCopy;
        }

        public ActionSet Clone()
        {
            ActionSet clone = CleanCopy();
            foreach (KeyValuePair<int, int> kvp in _actionPayoff)
            {
                clone.Add(kvp);
            }
            return clone;
        }

        public KeyValuePair<int, int> GetBestAction()
        {
            var best = new KeyValuePair<int, int>(-1, int.MinValue);
            foreach (KeyValuePair<int, int> kvp in _actionPayoff)
            {
                if (kvp.Value > best.Value)
                    best = kvp;
            }
            return best;
        }

        public int this[int key]
        {
            get
            {
                return _actionPayoff[key];
            }

            set
            {
                _actionPayoff[key] = value;
            }
        }

        public int Count => _actionPayoff.Count;

        public bool IsReadOnly => false;

        public ICollection<int> Keys => _actionPayoff.Keys;

        public ICollection<int> Values => _actionPayoff.Values;

        public void Add(KeyValuePair<int, int> item)
        {
            Add(item.Key, item.Value);
        }

        public void Add(int key, int value)
        {
            _actionPayoff.Add(key, value);
        }

        public void Clear()
        {
            _actionPayoff.Clear();
        }

        public bool Contains(KeyValuePair<int, int> item)
        {
            return _actionPayoff.Contains(item);
        }

        public bool ContainsKey(int key)
        {
            return _actionPayoff.ContainsKey(key);
        }

        public void CopyTo(KeyValuePair<int, int>[] array, int arrayIndex)
        {
            foreach (KeyValuePair<int, int> kvp in _actionPayoff)
            {
                array[arrayIndex++] = kvp;
            }
        }

        public IEnumerator<KeyValuePair<int, int>> GetEnumerator()
        {
            return _actionPayoff.GetEnumerator();
        }

        public bool Remove(KeyValuePair<int, int> item)
        {
            return Remove(item.Key);
        }

        public bool Remove(int key)
        {
            return _actionPayoff.Remove(key);
        }

        public bool TryGetValue(int key, out int value)
        {
            return _actionPayoff.TryGetValue(key, out value);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public override string ToString()
        {
            string s = "[ActionSet: \n";
            foreach (KeyValuePair<int, int> kvp in _actionPayoff)
            {
                s += $"\t[action: {kvp.Key}, value: {kvp.Value}]\n";
            }
            return s += "]";
        }
    }
}
