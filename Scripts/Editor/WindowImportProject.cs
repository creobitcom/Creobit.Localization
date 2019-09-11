using System;
using System.Reflection;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Creobit.Localization.Editor
{
    public class WindowImportProject : EditorWindow
    {
        private ILocalizationData _data = null;
        private IBaseParser[] _parserList = null;
        private int _indexCurrentParser = 0;
        private ImportLocalizationData _importData = null;

        private IBaseParser CurrentParser
        {
            get
            {
                return _parserList[_indexCurrentParser];
            }
        }

        internal static void Open(ILocalizationData targetObj)
        {
            var target = targetObj ?? throw new ArgumentNullException(nameof(targetObj));

            var window = GetWindow(typeof(WindowImportProject)) as WindowImportProject;
            window.titleContent = new GUIContent("Import");
            window._data = target;
        }

        private void OnFocus()
        {
            if (_parserList == null)
            {
                var baseType = typeof(IBaseParser);
                var compatibleTypesArray = Assembly.GetExecutingAssembly().GetTypes()
                    .Where(type => baseType.IsAssignableFrom(type) && type.IsClass).ToArray();

                if (compatibleTypesArray.Length > 0)
                {
                    _parserList = new IBaseParser[compatibleTypesArray.Length];

                    for (var i = 0; i < compatibleTypesArray.Length; ++i)
                    {
                        var parser = compatibleTypesArray[i];
                        _parserList[i] = (IBaseParser)Activator.CreateInstance(parser);
                    }
                }
            }
        }

        private void OnGUI()
        {
            EditorGUILayout.Space();
            OnGuiHeader();
            EditorGUILayout.Space();
            _importData?.OnGui();
            EditorGUILayout.Space();
            OnGuiButtons();
        }

        private void OnGuiHeader()
        {
            string[] parserNames = _parserList.Select(parser => parser.Name).ToArray();
            _indexCurrentParser = EditorGUILayout.Popup("Parser", _indexCurrentParser, parserNames);

            EditorGUILayout.Space();

            CurrentParser.OnGUI();
        }

        private void OnGuiButtons()
        {
            EditorGUILayout.BeginHorizontal();

            if (GUILayout.Button("Load"))
            {
                _importData = CurrentParser.Load();
            }

            GUI.enabled = _importData != null;

            if (GUILayout.Button("Import"))
            {
                _data.SetData(_importData.Languages, _importData.Keys);
                _importData = null;
            }

            GUI.enabled = true;

            EditorGUILayout.EndHorizontal();
            EditorGUILayout.Space();
        }
    }
}
