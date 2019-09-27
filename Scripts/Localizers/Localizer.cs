using System;
using UnityEngine;

namespace Creobit.Localization
{
    public abstract class Localizer : MonoBehaviour
    {
        #region MonoBehaviour

        private void Start()
        {
            LocalizationSystem.LocalizationUpdated += OnLocalizationUpdated;

            OnLocalizationUpdated(this, EventArgs.Empty);
        }

        private void OnDestroy()
        {
            LocalizationSystem.LocalizationUpdated -= OnLocalizationUpdated;
        }

        #endregion
        #region Localizer

        public static ILocalizationSystem LocalizationSystem;

        [SerializeField]
        private string _key;

        public string Key
        {
            get => _key;
            set
            {
                _key = value;

                OnLocalizationUpdated(this, EventArgs.Empty);
            }
        }

        protected abstract void UpdateValue(string value);

        private void OnLocalizationUpdated(object sender, EventArgs eventArgs)
        {
            if (string.IsNullOrWhiteSpace(_key))
            {
                Debug.LogError("The localization key is not installed.", this);
            }
            else
            {
                var value = LocalizationSystem.GetString(_key, this);

                UpdateValue(value);
            }
        }

        #endregion
    }
}
