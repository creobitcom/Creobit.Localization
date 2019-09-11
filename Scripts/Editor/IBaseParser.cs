
namespace Creobit.Localization.Editor
{
    public interface IBaseParser
    {
        string Name
        {
            get;
        }

        void OnGUI();

        ImportLocalizationData Load();
    }
}
