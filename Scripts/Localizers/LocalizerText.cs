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
            _text.text = value;
        }

        #endregion
        #region LocalizerText

        [SerializeField, HideInInspector]
        private Text _text;

        #endregion
    }
}
