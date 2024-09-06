using System;
using Script.ScriptableDataScript;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(AttackData))]
public class AttackCustomData : Editor
{    
    SerializedProperty showPrefabProp;
    SerializedProperty rangedPrefabProp;

    private void OnEnable()
    {
        // 필요한 필드들만 SerializedProperty로 선언
        showPrefabProp = serializedObject.FindProperty("isRanged"); // Is Ranged
        rangedPrefabProp = serializedObject.FindProperty("rangedPrefab"); // Ranged Prefab
    }

    public override void OnInspectorGUI()
    {
        // ScriptableObject 업데이트
        serializedObject.Update();

        // 모든 필드를 반복하면서 조건부로 필드를 그립니다.
        SerializedProperty property = serializedObject.GetIterator();
        property.NextVisible(true); // 첫 번째 필드로 이동 (Script 필드)
        
        do
        {
            // 특정 필드 (rangedPrefabProp)를 제외한 나머지를 자동으로 그립니다.
            if (property.name != "rangedPrefab")
            {
                EditorGUILayout.PropertyField(property, true);
            }
        } while (property.NextVisible(false));

        // showPrefab이 true일 때만 rangedPrefab 필드를 표시
        if (showPrefabProp.boolValue)
        {
            EditorGUILayout.PropertyField(rangedPrefabProp);
        }

        // 변경사항 저장
        serializedObject.ApplyModifiedProperties();
    }
}
