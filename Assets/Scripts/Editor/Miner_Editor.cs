using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;
using UnityEditor.Search;
using System;

[CustomEditor(typeof(Miner))]
public class Miner_Editor : NPC_Editor<Miner>
{
    public VisualTreeAsset minerVisualTree;
    PropertyField minerModeEnumField;
    SerializedProperty minerModeEnumProperty;
    VisualElement minerTargetOreSelection;
    private void OnEnable()
    {
        minerModeEnumProperty = serializedObject.FindProperty("priority");
    }
    public override VisualElement CreateInspectorGUI()
    {
        VisualElement content =  base.CreateInspectorGUI();
        VisualElement add = new VisualElement();
        minerVisualTree.CloneTree(add);
        content.Add(add);

        minerModeEnumField = content.Q<PropertyField>("MinerModeEnumField");
        minerTargetOreSelection = content.Q<VisualElement>("MinerTargetOreSelection");
        minerModeEnumField.RegisterCallback<ChangeEvent<string>>(OnMinerModeEnumChange);
        CheckDisplay();
        return content;
    }
    void OnMinerModeEnumChange(ChangeEvent<string> tmp)
    {
        CheckDisplay();
    }
    void CheckDisplay()
    {
        if (minerModeEnumProperty.enumValueIndex == (int)Miner_OrePriority.preferSetOre)
        {
            minerTargetOreSelection.style.display = DisplayStyle.Flex;
        }
        else
        {
            minerTargetOreSelection.style.display = DisplayStyle.None;
        }
    }
}
