using System.Collections.Generic;
using UnityEngine;

namespace Creobit.Localization
{
    [CreateAssetMenu(fileName = "LocalizationData", menuName = "Creobit/Assets/LocalizationData")]
    public class LocalizationData : ScriptableObject
    {
        #region LocalizationData

        [SerializeField]
        private List<string> _languages = new List<string>();

        [SerializeField]
        private List<LanguagesKeyValue> _keys = new List<LanguagesKeyValue>();

        public IEnumerable<string> Languages => _languages;

        public void SetData(IEnumerable<string> languages, IEnumerable<LanguagesKeyValue> keys)
        {
            _languages.Clear();
            _languages.AddRange(languages);

            _keys.Clear();
            _keys.AddRange(keys);
        }

        public string GetString(string key, string language)
        {
            var value = default(string);

            if (!string.IsNullOrEmpty(key))
            {
                if (!string.IsNullOrEmpty(language))
                {
                    var keyValue = _keys.Find(k => k.Id == key);

                    if (keyValue != null)
                    {
                        var index = _languages.IndexOf(language);
                        value = keyValue.GetValue(index);
                    }
                }
            }

            return value;
        }

        #endregion
    }
}
