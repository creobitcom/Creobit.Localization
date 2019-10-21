using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Sheets.v4;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

namespace Creobit.Localization.Editor
{
    [CreateAssetMenu(fileName = "GoogleSheetImporter", menuName = "Creobit/Localization/GoogleSheetImporter")]
    public sealed class GoogleSheetImporter : ScriptableObject
    {
        [SerializeField]
        private string _clientId;

        [SerializeField]
        private string _clientSecret;

        [SerializeField]
        private string _spreadsheetId;

        [SerializeField]
        private string[] _sheetNames;

        [SerializeField]
        private string[] _languages;

        [SerializeField]
        private LocalizationData _localizationData;

        public async Task ImportAsync()
        {
            var entries = new List<(string Language, string Key, string Value)>();

            await ImportAsync();

            async Task ImportAsync()
            {
                var clientSecrets = new ClientSecrets()
                {
                    ClientId = _clientId,
                    ClientSecret = _clientSecret
                };
                var scopes = new[]
                {
                    SheetsService.Scope.SpreadsheetsReadonly
                };

                var userCredential = await GoogleWebAuthorizationBroker.AuthorizeAsync(clientSecrets, scopes, "user", CancellationToken.None);
                var clientServiceInitializer = new BaseClientService.Initializer()
                {
                    HttpClientInitializer = userCredential
                };

                using (var sheetsService = new SheetsService(clientServiceInitializer))
                {
                    var spreadsheets = sheetsService.Spreadsheets;
                    var valuesResource = spreadsheets.Values;

                    foreach (var sheetName in _sheetNames)
                    {
                        var getRequest = valuesResource.Get(_spreadsheetId, sheetName);
                        var valueRange = await getRequest.ExecuteAsync();
                        var values = valueRange.Values;

                        UpdateEntries(values);
                    }

                    UpdateData();
                }
            }

            void UpdateEntries(IList<IList<object>> values)
            {
                for (var rowIndex = 1; rowIndex < values.Count; ++rowIndex)
                {
                    var key = Convert.ToString(values[rowIndex][0]).Trim();

                    if (string.IsNullOrWhiteSpace(key) || key.StartsWith("["))
                    {
                        continue;
                    }

                    for (var columnIndex = 1; columnIndex < values[rowIndex].Count; ++columnIndex)
                    {
                        var language = Convert.ToString(values[0][columnIndex]).Trim();

                        if (!Array.Exists(_languages, x => x == language))
                        {
                            continue;
                        }

                        var value = Convert.ToString(values[rowIndex][columnIndex]).Trim();

                        entries.Add((language, key, value));
                    }
                }
            }

            void UpdateData()
            {
                var languages = GetLanguages();
                var keyValues = GetKeyValues();

                _localizationData.SetData(languages, keyValues);

                EditorUtility.SetDirty(_localizationData);

                IEnumerable<string> GetLanguages()
                {
                    return entries
                        .Select(x => x.Language)
                        .Distinct();
                }

                IEnumerable<LanguagesKeyValue> GetKeyValues()
                {
                    var groups = entries
                        .Select(x => (x.Key, x.Value))
                        .GroupBy(x => x.Key);

                    foreach (var group in groups)
                    {
                        var values = group
                            .AsEnumerable()
                            .Select(x => x.Value);

                        yield return new LanguagesKeyValue(group.Key, values);
                    }
                }
            }
        }
    }
}
