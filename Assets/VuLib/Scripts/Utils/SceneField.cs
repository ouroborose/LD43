using System;
using UnityEngine;
using Object = UnityEngine.Object;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace VuLib
{
    [System.Serializable]
    public class SceneField
    {
        [SerializeField] private Object _sceneAsset;
        [SerializeField] private string _scenePath = "";
        [SerializeField] private string _sceneName = "";

        public string ScenePath
        {
            get { return _scenePath; }
        }

        public string SceneName
        {
            get { return _sceneName; }
        }

        // makes it work with the existing Unity methods (LoadLevel/LoadScene)
        public static implicit operator string(SceneField sceneField)
        {
            return sceneField.ScenePath;
        }
    }

#if UNITY_EDITOR
    [CustomPropertyDrawer(typeof(SceneField))]
    public class SceneFieldPropertyDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, GUIContent.none, property);
            var sceneAsset = property.FindPropertyRelative("_sceneAsset");
            var scenePath = property.FindPropertyRelative("_scenePath");
            var sceneName = property.FindPropertyRelative("_sceneName");
            position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);
            if (sceneAsset != null)
            {
                EditorGUI.BeginChangeCheck();
                var value = EditorGUI.ObjectField(position, sceneAsset.objectReferenceValue, typeof(SceneAsset), false);
                if (EditorGUI.EndChangeCheck())
                {
                    sceneAsset.objectReferenceValue = value;
                    if (sceneAsset.objectReferenceValue != null)
                    {
                        scenePath.stringValue = AssetDatabase.GetAssetPath(sceneAsset.objectReferenceValue);
                        sceneName.stringValue = (sceneAsset.objectReferenceValue as SceneAsset).name;
                    }
                }
            }
            EditorGUI.EndProperty();
        }
    }
#endif
}