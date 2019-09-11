using System;
using System.Collections.Generic;

namespace Creobit.Localization.Editor
{
    public sealed class ImportSheet
    {
        public readonly IEnumerable<LanguagesKeyValue> Groups;
        public readonly string Id;

        public bool IsUsed;

        public ImportSheet(string id, IEnumerable<LanguagesKeyValue> groups)
        {
            Id = id;
            Groups = groups ?? throw new ArgumentNullException(nameof(groups));
            IsUsed = true;
        }
    }
}
