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
        private Text _text = null;

        #endregion
    }
}
