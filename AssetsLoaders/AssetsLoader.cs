using UnityEngine;

namespace Creobit.Localization
{
    public abstract class AssetsLoader : ScriptableObject
    {
        public abstract T Load<T>(string path) where T : Object;
    }
}
