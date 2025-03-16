using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Ability))]
public class AbilityEditor : Editor
{
    SerializedObject so;
    SerializedProperty propAbilityName;
    SerializedProperty propAbilityType;
    SerializedProperty propTargetingType;

    //Movement only properties
    SerializedProperty propUncollapseDirection;

    //Attack only properties
    SerializedProperty propDamage;

    private void OnEnable()
    {
        so = serializedObject;
        propAbilityName = so.FindProperty("abilityName");
        propAbilityType = so.FindProperty("abilityType");
        propTargetingType = so.FindProperty("targetingType");
        propUncollapseDirection = so.FindProperty("uncollapseDirection");
        propDamage = so.FindProperty("damage");
    }

    public override void OnInspectorGUI()
    {
        //base.OnInspectorGUI();
        so.Update();
        EditorGUILayout.PropertyField(propAbilityName);
        EditorGUILayout.PropertyField(propAbilityType);
        EditorGUILayout.PropertyField(propTargetingType);
        switch (propAbilityType.intValue)
        {
            case (int)AbilityType.MOVEMENT:
                EditorGUILayout.PropertyField(propUncollapseDirection);
                break;
            case (int)AbilityType.ATTACK:
                EditorGUILayout.PropertyField(propDamage);
                break;
        }
        so.ApplyModifiedProperties();
    }
}
