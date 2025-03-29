using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;
[CustomEditor(typeof(Blacksmith))]
public class Blacksmith_Editor : NPC_Editor<Blacksmith>
{
    public VisualTreeAsset blacksmithVisualTree;
    public override VisualElement CreateInspectorGUI()
    {
        VisualElement content = base.CreateInspectorGUI();
        VisualElement add = new VisualElement();
        blacksmithVisualTree.CloneTree(add);
        content.Add(add);

        return content;
    }
}
