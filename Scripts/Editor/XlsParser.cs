using System;
using System.IO;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using CSharpJExcel.Jxl;

namespace Creobit.Localization.Editor
{
    public class XlsParser : IBaseParser
    {
        #region IBaseParser

        public string Name => "XlsParser";

        public void OnGUI()
        {
            var text = _loadPath != string.Empty ? _loadPath : "Path...";
            var textAreaStyle = new GUIStyle(EditorStyles.textArea);
            textAreaStyle.wordWrap = true;

            GUILayout.Label(text, textAreaStyle);
        }

        public ImportLocalizationData Load()
        {
            _loadPath = EditorUtility.OpenFilePanel("Import file", Application.dataPath,
                string.Join(";*.", FileExtensions));

            var result = default(ImportLocalizationData);

            var workbook = OpenWorkbook();

            if (workbook != null)
            {
                var languages = GetLanguages(workbook);
                var sheets = GetSheetsList(workbook);

                result = new ImportLocalizationData(languages, sheets);

                workbook.close();
            }

            return result;
        }

        #endregion
        #region XlsParser

        private readonly string[] FileExtensions = new string[1]
        {
            "xls"
        };

        private string _loadPath = string.Empty;

        private Workbook OpenWorkbook()
        {
            var workbook = default(Workbook);

            if (!string.IsNullOrEmpty(_loadPath))
            {
                var fileInfo = new FileInfo(_loadPath);

                try
                {
                    var workbookSettings = new WorkbookSettings();
                    workbookSettings.setEncoding("UTF-8");

                    workbook = Workbook.getWorkbook(fileInfo, workbookSettings);
                }
                catch (Exception ex)
                {
                    Debug.LogErrorFormat("Exception: {0}", ex.Message);
                }
            }

            return workbook;
        }

        private IEnumerable<ImportLanguage> GetLanguages(Workbook workbook)
        {
            Sheet[] sheets = workbook.getSheets();

            if (sheets.Length <= 0)
            {
                return null;
            }

            var sheet = sheets[0];
            var cells = sheet.getRow(0);

            var result = new List<ImportLanguage>();

            // Пропускаем первый столбец, т.к. он зарезервирован под ключ локализации.
            for (var i = 1; i < cells.Length; ++i)
            {
                var cell = cells[i];
                var value = cell.getContents();

                if (string.IsNullOrWhiteSpace(value))
                {
                    value = string.Format("[Empty-{0}]", i);
                }

                if (!string.IsNullOrEmpty(value))
                {
                    var importLanguage = new ImportLanguage(value);
                    result.Add(importLanguage);
                }
            }

            return result;
        }

        private IEnumerable<ImportSheet> GetSheetsList(Workbook workbook)
        {
            var resultSheets = new List<ImportSheet>();

            Sheet[] sheets = workbook.getSheets();

            foreach (var sheet in sheets)
            {
                var importSheet = GetSheet(sheet);

                if (importSheet != null)
                {
                    resultSheets.Add(importSheet);
                }
            }

            return resultSheets;
        }

        private ImportSheet GetSheet(Sheet sheet)
        {
            var groups = GetGroups(sheet);
            var importSheet = new ImportSheet(sheet.getName(), groups);

            return importSheet;
        }

        private IEnumerable<LanguagesKeyValue> GetGroups(Sheet sheet)
        {
            var result = new List<LanguagesKeyValue>();
            var cellsLength = sheet.getRow(0).Length;
            var rowCount = sheet.getRows();

            for (var i = 1 ; i < rowCount; ++i)
            {
                var cells = sheet.getRow(i);

                if (cells.Length <= 0)
                {
                    continue;
                }

                var key = cells[0].getContents();

                if (string.IsNullOrWhiteSpace(key) ||
                    key.StartsWith("["))
                {
                    continue;
                }

                char[] charsToTrim = { ' ' };
                key = key.Trim(charsToTrim);

                var localizationList = new List<string>();

                for (var j = 1; j < cellsLength; ++j)
                {
                    var value = string.Empty;

                    if (cells.Length > j)
                    {
                        var cell = cells[j];
                        value = cell.getContents();
                    }

                    localizationList.Add(value);
                }

                var languagesKeyValue = new LanguagesKeyValue(key, localizationList);
                result.Add(languagesKeyValue);
            }

            return result;
        }

        #endregion
    }
}
