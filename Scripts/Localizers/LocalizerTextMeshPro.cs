using UnityEngine;
using TMPro;

namespace Creobit.Localization
{
    [DisallowMultipleComponent, RequireComponent(typeof(TextMeshPro))]
    public sealed class LocalizerTextMeshPro : Localizer
    {
        #region MonoBehaviour

        private void Awake()
        {
            if (_text == null)
            {
                _text = GetComponent<TextMeshPro>();
            }
        }

        private void Reset()
        {
            _text = GetComponent<TextMeshPro>();
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
        private TextMeshPro _text;

        #endregion
    }
}
