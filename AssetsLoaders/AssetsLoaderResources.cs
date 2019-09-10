using UnityEngine;

namespace Creobit.Localization
{
    [CreateAssetMenu(fileName = "AssetsLoaderResources", menuName = "Creobit/Assets/AssetsLoaderResources")]
    public sealed class AssetsLoaderResources : AssetsLoader
    {
        #region AssetsLoader

        public override T Load<T>(string path)
        {
            var fullPath = _prefixPath + path;
            var asset = Resources.Load<T>(fullPath);
            return asset;
        }

        #endregion
        #region AssetsLoaderResources

        [SerializeField]
        private string _prefixPath;

        #endregion
    }
}
