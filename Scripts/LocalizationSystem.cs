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
            get => _currentLanguage;
            set
            {
                if (!string.IsNullOrWhiteSpace(value))
                {
                    _currentLanguage = value;
                    LocalizationUpdated?.Invoke(this, EventArgs.Empty);
                }
            }
        }

        public IEnumerable<string> SupportedLanguages => LocalizationData.Languages;

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

        private string _currentLanguage;

        public LocalizationSystem(ILocalizationData localizationData)
        {
            LocalizationData = localizationData ?? throw new ArgumentNullException(nameof(localizationData));
            _defaultLanguage = LocalizationData.Languages.First();
            CurrentLanguage = _defaultLanguage;
        }

        #endregion
    }
}
