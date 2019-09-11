using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

namespace Creobit.Localization
{
    public sealed class LocalizationSystem : ILocalizationSystem
    {
        #region ILocalizationSystem

        public event EventHandler LocalizationUpdated = delegate { };

        public string DefaultLanguage
        {
            get => _defaultLanguage;
            set
            {
                if (SupportedLanguages.Contains(value))
                {
                    _defaultLanguage = value;
                }
                else
                {
                    throw new ArgumentException("Language not found!", nameof(value));
                }
            }
        }

        public string CurrentLanguage
        {
            get;
            private set;
        }

        public IEnumerable<string> SupportedLanguages => LocalizationData.Languages;

        public void SetCurrentLanguage(string language)
        {
            if (!string.IsNullOrEmpty(language))
            {
                CurrentLanguage = language;
                LocalizationUpdated?.Invoke(this, EventArgs.Empty);
            }
        }

        public string GetString(string key, UnityEngine.Object context = null)
        {
            var result = LocalizationData.GetString(key, CurrentLanguage);

            if (result == null)
            {
                Debug.LogErrorFormat(context, "Key \"{0}\" not found!", key);
            }
            else if (string.IsNullOrWhiteSpace(result))
            {
                result = LocalizationData.GetString(key, DefaultLanguage);
            }

            return result;
        }

        #endregion
        #region LocalizationSystem

        private readonly ILocalizationData LocalizationData;

        private string _defaultLanguage;

        public LocalizationSystem(ILocalizationData localizationData)
        {
            LocalizationData = localizationData ?? throw new ArgumentNullException(nameof(localizationData));
            _defaultLanguage = LocalizationData.Languages.First();
            CurrentLanguage = _defaultLanguage;
        }

        #endregion
    }
}
