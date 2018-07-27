using UnityEditor;
using UnityEngine;
using System;

[CustomPropertyDrawer(typeof(EntityDataInfo))]
public class EntityDataInfoDrawer : PropertyDrawer
{
    int selectedId = -1;
    int selectedTypeIndex = -1;
    string[] typesNames;
    int[] ids;
    string[] idStrings;

    Rect newValuePos;
    Rect newLabelPos;
    private readonly Vector2 propertyShift = new Vector2(0f, 20f);

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        propertyHeight = 0f;

        if (selectedTypeIndex < 0) selectedTypeIndex = 0;
        if (selectedId < 0)   selectedId = 0;

        newValuePos = new Rect(position.x + position.width * 0.5f, position.y, position.width * 0.5f, 20f);
        newLabelPos = new Rect(position.x, position.y, position.width * 0.5f, 20f);

        EditorGUI.LabelField(position, ":::::::::: ENTITY DATA INFO ::::::::::::");
        Shift();

        EditorGUI.BeginProperty(newValuePos, label, property);

        // types
        EditorGUI.LabelField(newLabelPos, "DATA TYPE:");
        typesNames = DataController.Instance.GetStoragesTypes();
        selectedTypeIndex = EditorGUI.Popup(newValuePos, selectedTypeIndex, typesNames);
        Shift();

        // ids
        EditorGUI.LabelField(newLabelPos, "DATA ID:");
        ids = DataController.Instance.Storage(typesNames[selectedTypeIndex]).GetIds();
        idStrings = new string[ids.Length];
        for(int i = 0; i < ids.Length; ++i)
        {
            idStrings[i] = ids[i].ToString();
        }

        selectedId = EditorGUI.Popup(newValuePos, selectedId, idStrings);
        Shift();
        //

        property.FindPropertyRelative("id").intValue = ids[selectedId];
        property.FindPropertyRelative("storageType").stringValue = typesNames[selectedTypeIndex];

        EditorGUI.EndProperty();
    }

    void Shift()
    {
        propertyHeight += propertyShift.y;
        newValuePos.y += propertyShift.y;
        newLabelPos.y += propertyShift.y;
    }

    float propertyHeight;
    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return propertyHeight;
    }

}
