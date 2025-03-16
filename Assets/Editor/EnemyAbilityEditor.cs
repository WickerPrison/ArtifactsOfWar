using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(EnemyAbility))]
public class EnemyAbilityEditor : Editor
{
    SerializedObject so;
    SerializedProperty propAbilityName;
    SerializedProperty propAbilityType;
    SerializedProperty propTargetingType;
    SerializedProperty propDamage;

    private void OnEnable()
    {
        so = serializedObject;
        propAbilityName = so.FindProperty("abilityName");
        propAbilityType = so.FindProperty("abilityType");
        propTargetingType = so.FindProperty("targetingType");
        propDamage = so.FindProperty("damage");
    }

    public override void OnInspectorGUI()
    {
        //base.OnInspectorGUI();
        so.Update();
        EditorGUILayout.PropertyField(propAbilityName);
        EditorGUILayout.PropertyField(propTargetingType);
        EditorGUILayout.PropertyField(propAbilityType);
        if (propAbilityType.intValue == (int)EnemyAbilityType.ATTACK)
        {
            EditorGUILayout.PropertyField(propDamage);
        }
        so.ApplyModifiedProperties();
    }
}