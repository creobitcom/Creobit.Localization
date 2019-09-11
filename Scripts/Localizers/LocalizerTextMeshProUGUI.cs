using UnityEngine;
using TMPro;

namespace Creobit.Localization
{
    [DisallowMultipleComponent, RequireComponent(typeof(TextMeshProUGUI))]
    public sealed class LocalizerTextMeshProUGUI : Localizer
    {
        #region MonoBehaviour

        private void Awake()
        {
            if (_text == null)
            {
                _text = GetComponent<TextMeshProUGUI>();
            }
        }

        private void Reset()
        {
            _text = GetComponent<TextMeshProUGUI>();
        }

        #endregion
        #region Localizer

        protected override void UpdateValue(string value)
        {
            _text.text = value;
        }

        #endregion
        #region LocalizerText

        [SerializeField, HideInInspector]
        private TextMeshProUGUI _text;

        #endregion
    }
}
