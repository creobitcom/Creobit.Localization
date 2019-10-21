using UnityEditor;
using UnityEngine;

namespace Creobit.Localization.Editor
{
    [CustomEditor(typeof(GoogleSheetImporter))]
    public sealed class GoogleSheetImporterEditor : UnityEditor.Editor
    {
        #region Editor

        public override async void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            EditorGUILayout.Separator();

            if (GUILayout.Button("Import"))
            {
                EditorUtility.DisplayProgressBar("Importing Localization", "Please wait...", 0.5f);

                var googleSheetImporter = (GoogleSheetImporter)target;

                await googleSheetImporter.ImportAsync();

                EditorUtility.ClearProgressBar();
            }
        }

        #endregion
    }
}
