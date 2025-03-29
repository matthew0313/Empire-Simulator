using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;
[CustomEditor(typeof(Builder))]
public class Builder_Editor : NPC_Editor<Builder>
{
    public VisualTreeAsset builderVisualTree;
    public override VisualElement CreateInspectorGUI()
    {
        VisualElement content = base.CreateInspectorGUI();
        VisualElement add = new VisualElement();
        builderVisualTree.CloneTree(add);
        content.Add(add);

        return content;
    }
}
