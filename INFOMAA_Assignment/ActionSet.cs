using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace INFOMAA_Assignment
{
    public class ActionSet : IDictionary<int, int>
    {
        Dictionary<int, int> _actionPayoff;
        int _numActions;
        int _bestAction = 0;

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

        public int NumActions { get { return _numActions; } }

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

        public int GetBestAction()
        {
            return _bestAction;
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
                if (value > _actionPayoff[_bestAction])
                    _bestAction = key;
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
