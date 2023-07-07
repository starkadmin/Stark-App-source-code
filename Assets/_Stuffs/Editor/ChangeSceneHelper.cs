using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class ChangeSceneHelper : EditorWindow
{
    Vector2 scrollPos;

    [MenuItem("Editor Helper/ChangeScene")]
    private static void ShowWindow()
    {
        var window = GetWindow<ChangeSceneHelper>();
        window.titleContent = new GUIContent("ChangeScene");
        window.Show();
    }

    private void OnGUI()
    {
        EditorGUILayout.BeginVertical();
        this.scrollPos = EditorGUILayout.BeginScrollView(this.scrollPos, false, false);
        GUILayout.Label("==============================", EditorStyles.boldLabel);
        GUILayout.Label("          Scene List", EditorStyles.boldLabel);
        GUILayout.Label("==============================", EditorStyles.boldLabel);

        var path = $"{Application.dataPath}/Scenes";
        var _directories = Directory.GetDirectories(path);
        for (int i = 0; i < _directories.Length; i++)
        {
            var _str = Path.GetFileNameWithoutExtension(_directories[i]);
            var _dir = Directory.GetFiles(_directories[i]);
            GUILayout.Label(_str, EditorStyles.boldLabel);
            GUILayout.Label("----------", EditorStyles.boldLabel);
            for (int j = 0; j < _dir.Length; j++)
            {
                var extension = Path.GetExtension(_dir[j]);
                if (extension != ".unity") continue;
                var _scene = $"{_directories[i]}/{Path.GetFileName(_dir[j])}";
                var pressed = GUILayout.Button(Path.GetFileNameWithoutExtension(_dir[j]),
                    new GUIStyle(GUI.skin.GetStyle("Button")) {alignment = TextAnchor.MiddleCenter});
                if (!pressed) continue;
                if (EditorApplication.SaveCurrentSceneIfUserWantsTo())
                {
                    EditorApplication.OpenScene(_scene);

                }
            }
            GUILayout.Label("----------", EditorStyles.boldLabel);
        }
        EditorGUILayout.EndScrollView();
        EditorGUILayout.EndVertical();
    }
}
