using System;
using System.Collections.Generic;
using UnityEngine;

namespace Creobit.Localization
{
    public sealed class LocalizationSystemDummy : ILocalizationSystem
    {
        #region ILocalizationSystem

        public event EventHandler LocalizationUpdated = delegate { };

        public string DefaultLanguage
        {
            get;
            set;
        }

        public string CurrentLanguage
        {
            get;
            private set;
        }

        public IEnumerable<string> SupportedLanguages
        {
            get;
        }

        public void SetCurrentLanguage(string language)
        {
        }

        public string GetString(string key, UnityEngine.Object context = null)
        {
            return null;
        }

        #endregion
        #region LocalizationSystem

        public LocalizationSystemDummy(ILocalizationData localizationData)
        {
            Debug.LogWarningFormat("Used [0]!", nameof(LocalizationSystemDummy));
        }

        #endregion
    }
}
