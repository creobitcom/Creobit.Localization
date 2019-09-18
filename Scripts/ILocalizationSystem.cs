using System;
using System.Collections.Generic;

namespace Creobit.Localization
{
    public interface ILocalizationSystem
    {
        event EventHandler LocalizationUpdated;

        string DefaultLanguage
        {
            get;
            set;
        }

        string CurrentLanguage
        {
            get;
            set;
        }

        IEnumerable<string> SupportedLanguages
        {
            get;
        }

        string GetString(string key, UnityEngine.Object context = null);
    }
}
