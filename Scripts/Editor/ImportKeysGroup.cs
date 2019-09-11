using System.Collections.Generic;

namespace Creobit.Localization.Editor
{
    public class ImportKeysGroup
    {
        List<LanguagesKeyValue> m_keys = new List<LanguagesKeyValue>();

        public static bool IsNullOrEmpty(ImportKeysGroup _value)
        {
            return _value == null || _value.Count <= 0;
        }

        public ImportKeysGroup(string _id)
        {
            Id = _id;
            IsUsed = true;
        }

        public string Id
        {
            get;
            private set;
        }

        public int Count
        {
            get
            {
                return m_keys.Count;
            }
        }

        public bool IsUsed
        {
            get;
            set;
        }

        public LanguagesKeyValue[] Keys
        {
            get
            {
                return m_keys.ToArray();
            }
        }

        public void Add(LanguagesKeyValue _key)
        {
            if (_key != null)
                m_keys.Add(_key);
        }
    }
}
