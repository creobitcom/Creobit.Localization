using System;
using System.Collections.Generic;
using UnityEngine;

namespace Creobit.Localization
{
    [Serializable]
    public class LanguagesKeyValue
    {
        #region LanguagesKeyValue

        [SerializeField]
        private string _id;

        [SerializeField]
        private List<string> _values;

        public LanguagesKeyValue(string id, IEnumerable<string> values)
        {
            _id = id;
            _values = new List<string>(values);
        }

        public string Id => _id;

        public IEnumerable<string> Values => _values;

        public string GetValue(int index)
        {
            return _values[index];
        }

        public void RemoveAt(int index)
        {
            _values.RemoveAt(index);
        }

        #endregion
    }
}
