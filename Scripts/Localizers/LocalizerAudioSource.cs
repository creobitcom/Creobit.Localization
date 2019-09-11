using UnityEngine;

namespace Creobit.Localization
{
    [DisallowMultipleComponent, RequireComponent(typeof(AudioSource))]
    public sealed class LocalizerAudioSource : Localizer
    {
        #region MonoBehaviour

        private void Awake()
        {
            if (_audioSource == null)
            {
                _audioSource = GetComponent<AudioSource>();
            }
        }

        private void Reset()
        {
            _audioSource = GetComponent<AudioSource>();
        }

        #endregion
        #region Localizer

        protected override void UpdateValue(string value)
        {
            Debug.Assert(_assetsLoader != null, "Not installed AssetsLoader!", this);

            var audioClip = _assetsLoader.Load<AudioClip>(value);

            Debug.AssertFormat(audioClip != null, this, "Failed to load asset \"{0}\"", value);

            bool isPlaying = _audioSource.isPlaying;
            _audioSource.Stop();
            _audioSource.clip = audioClip;

            if (isPlaying)
            {
                _audioSource.Play();
            }
        }

        #endregion
        #region LocalizerText

        [SerializeField, HideInInspector]
        private AudioSource _audioSource;

        [SerializeField]
        private AssetLoader _assetsLoader;

        #endregion
    }
}
