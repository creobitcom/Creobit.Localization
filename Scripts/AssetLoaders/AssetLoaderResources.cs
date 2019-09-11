using UnityEngine;

namespace Creobit.Localization
{
    [CreateAssetMenu(fileName = "AssetLoaderResources", menuName = "Creobit/Assets/AssetLoaderResources")]
    public sealed class AssetLoaderResources : AssetLoader
    {
        #region AssetLoader

        public override T Load<T>(string path)
        {
            var fullPath = _prefixPath + path;
            var asset = Resources.Load<T>(fullPath);
            return asset;
        }

        #endregion
        #region AssetLoaderResources

        [SerializeField]
        private string _prefixPath = string.Empty;

        #endregion
    }
}
