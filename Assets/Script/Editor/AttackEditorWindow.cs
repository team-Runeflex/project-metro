using System;
using Codice.CM.Common;
using Script.ScriptableDataScript;
using UnityEditor;
using UnityEngine;
using Script.ScriptableDataScript;
using UnityEditor.Experimental;


public class AttackEditorWindow : EditorWindow
{
    private string attackDataName = "NewAttackData";


    string basePath = "Assets/ScriptableData/AttackData/";

    [MenuItem("Tools/AttackEditorWindow")]
    private static void ShowWindow()
    {
        GetWindow<AttackEditorWindow>("AttackData Creator");
    }

    private CharacterEnum selectedCharacter;

    private void OnGUI()
    {
        GUILayout.Label("Create Attack Data ScriptableObject", EditorStyles.boldLabel);

        attackDataName = EditorGUILayout.TextField("Attack Data Name", attackDataName);

        selectedCharacter = (CharacterEnum)EditorGUILayout.EnumPopup("Using Character", selectedCharacter);

        if (GUILayout.Button("Create AttackData ScriptableObject"))
        {
            // 경로에서 공백을 없애고 생성
            CreateAttackScriptableObject<AttackData>("Assets/ScriptableData/AttackData/");
        }
    }

    private void CreateAttackScriptableObject<T>(string path) where T : ScriptableObject
    {
        // ScriptableObject 생성
        T asset = ScriptableObject.CreateInstance<T>();

        if (asset is AttackData attackData)
        {
            attackData.CharEnum = selectedCharacter;

            string folderPath = basePath + selectedCharacter.ToString() + "/";

            folderPath = folderPath.Trim();

            if (!AssetDatabase.IsValidFolder("Assets/ScriptableData"))
            {
                AssetDatabase.CreateFolder("Assets", "ScriptableData");
            }

            if (!AssetDatabase.IsValidFolder("Assets/ScriptableData/AttackData"))
            {
                AssetDatabase.CreateFolder("Assets/ScriptableData", "AttackData");
            }

            if (!AssetDatabase.IsValidFolder(folderPath.TrimEnd('/')))
            {
                AssetDatabase.CreateFolder(basePath.TrimEnd('/'), selectedCharacter.ToString());
            }

            attackDataName = attackDataName.Trim();

            string fullPath = folderPath + attackDataName + ".asset";
            
            AssetDatabase.CreateAsset(asset, fullPath);
            AssetDatabase.SaveAssets();
            
            EditorUtility.FocusProjectWindow();
            Selection.activeObject = asset;
        }
    }
}
