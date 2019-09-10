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
        }

        IEnumerable<string> SupportedLanguages
        {
            get;
        }

        void SetCurrentLanguage(string language, bool isInvakeEvent = true);

        string GetString(string key, UnityEngine.Object context = null);
    }
}
