using UnityEngine;
using UnityEditor;

namespace Creobit.Localization.Editor
{
    [CustomEditor(typeof(LocalizationData))]
    public class LocalizationDataEditor : UnityEditor.Editor
    {
        private LocalizationData _target = null;
        private SerializedObject _targetObj;
        private SerializedProperty _languagesProperty;

        public void OnEnable()
        {
            _target = target as LocalizationData;
            _targetObj = new SerializedObject(target);
            _languagesProperty = _targetObj.FindProperty("_languages");
        }

        public override void OnInspectorGUI()
        {
            _targetObj.Update();

            GUI.enabled = false;
            EditorGUILayout.PropertyField(_languagesProperty, true);
            GUI.enabled = true;

            if (GUILayout.Button("Import"))
            {
                WindowImportProject.Open(_target);
            }

            _targetObj.ApplyModifiedProperties();

            if (GUI.changed)
            {
                EditorUtility.SetDirty(_target);
            }
        }
    }
}
