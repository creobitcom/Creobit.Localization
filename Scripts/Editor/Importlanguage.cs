namespace Creobit.Localization.Editor
{
    public sealed class ImportLanguage
    {
        public readonly string Value;
        public bool IsUsed;

        public ImportLanguage(string value)
        {
            Value = value;
            IsUsed = true;
        }
    }
}
