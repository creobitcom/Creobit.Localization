using UnityEngine;
using UnityEngine.UI;

namespace Creobit.Localization
{
    [DisallowMultipleComponent, RequireComponent(typeof(Text))]
    public sealed class LocalizerText : Localizer
    {
        #region MonoBehaviour

        private void Awake()
        {
            if (_text == null)
            {
                _text = GetComponent<Text>();
            }
        }

        private void Reset()
        {
            _text = GetComponent<Text>();
        }

        #endregion
        #region Localizer

        protected override void UpdateValue(string value)
        {
            _text.text = _isUpperCase ? value.ToUpper() : value;
        }

        #endregion
        #region LocalizerText

        [SerializeField, HideInInspector]
        private Text _text;

        [SerializeField]
        private bool _isUpperCase = false;

        #endregion
    }
}
