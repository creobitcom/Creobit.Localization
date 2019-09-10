using System.Collections.Generic;
using UnityEngine;

namespace Assets.Creobit.Localization.Editor
{
    public sealed class ImportKeyValue
    {
        #region ImportKeyValue

        [SerializeField]
        private readonly string _id;

        [SerializeField]
        private readonly List<string> _values;

        public ImportKeyValue(string id, IEnumerable<string> values)
        {
            _id = id;
            _values = new List<string>(values);
        }

        public string Id => _id;

        public IEnumerable<string> Values => _values;

        public void RemoveAt(int index)
        {
            _values.RemoveAt(index);
        }

        #endregion
    }
}
