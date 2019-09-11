using System.Collections.Generic;

namespace Creobit.Localization
{
    public interface ILocalizationData
    {
        IEnumerable<string> Languages
        {
            get;
        }

        void SetData(IEnumerable<string> languages, IEnumerable<LanguagesKeyValue> keys);

        string GetString(string key, string language);
    }
}
