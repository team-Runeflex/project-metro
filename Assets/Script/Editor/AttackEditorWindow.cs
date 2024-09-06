using System;
using Codice.CM.Common;
using Script.ScriptableDataScript;
using UnityEditor;
using UnityEngine;
using Script.ScriptableDataScript;
using UnityEditor.Experimental;


public class AttackEditorWindow : EditorWindow
{
    private const string IdKeyPrefix = "Id";
    private string attackDataName = "NewAttackData";
    private int lastAssignedID = -1;
    private Sprite icon;

    public static int GetNextID(CharacterEnum charEnum)
    {
        string key = IdKeyPrefix + charEnum.ToString();
        int lastID = EditorPrefs.GetInt(key, 0);
        int newID = lastID + 1;
        EditorPrefs.SetInt(key, newID);
        return newID;
    }

    public static void ResetID(CharacterEnum charEnum)
    {
        string key = IdKeyPrefix + charEnum.ToString();
        EditorPrefs.SetInt(key, 0);
    }

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

        icon = (Sprite)EditorGUILayout.ObjectField("Sprite", icon, typeof(Sprite), false);
        
        
        if (GUILayout.Button("Create AttackData ScriptableObject"))
        {
            // 경로에서 공백을 없애고 생성
            CreateAttackScriptableObject<AttackData>("Assets/ScriptableData/AttackData/");
        }

        if (GUILayout.Button("Reset ID for selected Enum"))
        {
            ResetID(selectedCharacter);
            Debug.Log($"ID for {selectedCharacter} has been reset.");
        }

        if (lastAssignedID != -1)
        {
            GUILayout.Label($"Last Created ID: {lastAssignedID}", EditorStyles.boldLabel);
        }
    }

    private void CreateAttackScriptableObject<T>(string path) where T : ScriptableObject
    {
        // ScriptableObject 생성
        T asset = ScriptableObject.CreateInstance<T>();
        

        if (asset is AttackData attackData)
        {
            lastAssignedID = GetNextID(selectedCharacter);
            attackData.Id = lastAssignedID;
            attackData.AttackName = attackDataName;
            attackData.CharEnum = selectedCharacter;
            attackData.Icon = icon;
            string folderPath = basePath + selectedCharacter + "/";

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
