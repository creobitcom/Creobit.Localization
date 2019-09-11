using UnityEngine;
using UnityEngine.UI;

namespace Creobit.Localization
{
    [DisallowMultipleComponent, RequireComponent(typeof(Image))]
    public sealed class LocalizerImage : Localizer
    {
        #region MonoBehaviour

        private void Awake()
        {
            if (_image == null)
            {
                _image = GetComponent<Image>();
            }
        }

        private void Reset()
        {
            _image = GetComponent<Image>();
        }

        #endregion
        #region Localizer

        protected override void UpdateValue(string value)
        {
            Debug.Assert(_assetsLoader != null, "Not installed AssetsLoader!", this);

            var texture = _assetsLoader.Load<Texture2D>(value);

            Debug.AssertFormat(texture != null, this, "Failed to load asset \"{0}\"", value);

            var rect = new Rect(0, 0, texture.width, texture.height);
            var pivot = Vector2.one * 0.5f;
            var sprite = Sprite.Create(texture, rect, pivot);
            _image.sprite = sprite;
        }

        #endregion
        #region LocalizerImage

        [SerializeField, HideInInspector]
        private Image _image;

        [SerializeField]
        private AssetLoader _assetsLoader;

        #endregion
    }
}
