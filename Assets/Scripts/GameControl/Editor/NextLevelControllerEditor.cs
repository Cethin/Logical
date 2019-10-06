using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(NextLevelController), true)]
public class NextLevelControllerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        var menuController = target as NextLevelController;
        var oldScene = AssetDatabase.LoadAssetAtPath<SceneAsset>(menuController.nextScene);

        serializedObject.Update();

        EditorGUI.BeginChangeCheck();
        var newScene = EditorGUILayout.ObjectField("scene", oldScene, typeof(SceneAsset), false) as SceneAsset;

        if (EditorGUI.EndChangeCheck())
        {
            var newPath = AssetDatabase.GetAssetPath(newScene);
            var scenePathProperty = serializedObject.FindProperty("nextScene");
            scenePathProperty.stringValue = newPath;
        }
        serializedObject.ApplyModifiedProperties();
    }
}
