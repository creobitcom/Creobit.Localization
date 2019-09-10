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

                if (_text == null)
                {
                    Debug.LogError("UI.Text component not found!", this);
                }
            }
        }

        #endregion
        #region Localizer

        protected override void UpdateValue(string value)
        {
            if (_text != null)
            {
                _text.text = value;
            }
        }

        #endregion
        #region LocalizerText

        [SerializeField]
        private TextMeshProUGUI _text = null;

        #endregion
    }
}
