using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;
[CustomEditor(typeof(EmpireManager))]
public class EmpireManager_Editor : Editor
{
    EmpireManager origin;

    public VisualTreeAsset visualTree;
    Button addToStorageButton, clearStorageButton;
    Button addTimeButton, subtractTimeButton;
    PropertyField addToStorageItemField;
    PropertyField claimedIslandsField;
    SerializedProperty addingItem;
    SerializedProperty claimedIslands;

    private void OnEnable()
    {
        origin = (EmpireManager)target;
        addingItem = serializedObject.FindProperty("addingItem");
        claimedIslands = serializedObject.FindProperty("claimedIslands");
    }

    public override VisualElement CreateInspectorGUI()
    {
        VisualElement root = new VisualElement();

        visualTree.CloneTree(root);

        addToStorageButton = root.Q<Button>("AddToStorageButton");
        addToStorageButton.RegisterCallback<ClickEvent>(AddToStorage);

        clearStorageButton = root.Q<Button>("ClearStorageButton");
        clearStorageButton.RegisterCallback<ClickEvent>(ClearStorage);

        addToStorageItemField = root.Q<PropertyField>("AddToStorageItemField");
        addToStorageItemField.BindProperty(addingItem);

        claimedIslandsField = root.Q<PropertyField>("ClaimedIslandsField");
        claimedIslandsField.BindProperty(claimedIslands);

        addTimeButton = root.Q<Button>("AddTimeButton");
        addTimeButton.RegisterCallback<ClickEvent>(AddTime);
        subtractTimeButton = root.Q<Button>("SubtractTimeButton");
        subtractTimeButton.RegisterCallback<ClickEvent>(SubtractTime);

        return root;
    }
    void AddToStorage(ClickEvent a)
    {
        origin.AddToStorage();
    }
    void ClearStorage(ClickEvent a)
    {
        origin.storage.Clear();
    }
    void AddTime(ClickEvent a)
    {
        origin.PassHour();
    }
    void SubtractTime(ClickEvent a)
    {
        origin.SubtractHour();
    }
}
