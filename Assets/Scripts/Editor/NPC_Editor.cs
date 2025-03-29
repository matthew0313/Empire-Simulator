using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;
public class NPC_Editor<T> : Editor where T : NPC
{
    T origin;

    public VisualTreeAsset visualTree;
    Button addEnergyButton, setEnergyButton, levelUpButton;

    private void OnEnable()
    {
        origin = (T)target;
    }

    public override VisualElement CreateInspectorGUI()
    {
        VisualElement root = new VisualElement();

        visualTree.CloneTree(root);

        addEnergyButton = root.Q<Button>("AddEnergyButton");
        addEnergyButton.RegisterCallback<ClickEvent>(AddEnergy);

        setEnergyButton = root.Q<Button>("SetEnergyButton");
        setEnergyButton.RegisterCallback<ClickEvent>(SetEnergy);

        levelUpButton = root.Q<Button>("LevelUpButton");
        levelUpButton.RegisterCallback<ClickEvent>(LevelUp);

        return root;
    }
    void AddEnergy(ClickEvent a)
    {
        origin.energy += 100.0f;
    }
    void SetEnergy(ClickEvent a)
    {
        origin.energy = 0.0f;
    }
    void LevelUp(ClickEvent a)
    {
        origin.LevelUp();
    }
}
