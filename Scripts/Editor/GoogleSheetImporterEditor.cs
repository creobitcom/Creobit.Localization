#if CREOBIT_LOCALIZATION_GOOGLEDOCS
using System.Threading;
using System.Threading.Tasks;
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
                if (_importTask == null)
                {
                    try
                    {
                        var googleSheetImporter = (GoogleSheetImporter)target;

                        _cancellationTokenSource = new CancellationTokenSource();
                        _importTask = googleSheetImporter.ImportAsync(_cancellationTokenSource.Token);
                        _progressBar = true;

                        await _importTask;
                    }
                    finally
                    {
                        _cancellationTokenSource.Dispose();
                        _cancellationTokenSource = null;
                        _importTask = null;
                        _progressBar = false;
                    }
                }
            }

            if (_progressBar.HasValue)
            {
                if (_progressBar.Value)
                {
                    if (EditorUtility.DisplayCancelableProgressBar("Importing Localization", "Please wait...", 0.5f))
                    {
                        if (!_cancellationTokenSource.IsCancellationRequested)
                        {
                            _cancellationTokenSource.Cancel();
                        }
                    }
                }
                else
                {
                    _progressBar = null;

                    EditorUtility.ClearProgressBar();
                }
            }
        }

        public override bool RequiresConstantRepaint()
        {
            return true;
        }

        #endregion
        #region GoogleSheetImporterEditor

        private CancellationTokenSource _cancellationTokenSource;
        private Task _importTask;
        private bool? _progressBar;

        #endregion
    }
}
#endif
