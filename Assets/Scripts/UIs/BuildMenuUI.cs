using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BuildMenuUI : MonoBehaviour
{
    Player player;
    [SerializeField] GameObject buildMenuButton;
    [SerializeField] Transform anchor;
    [SerializeField] TMP_Text structureName, structureDescription, structureIngredients;
    List<BuildMenuButton> buildMenuButtons = new();

    Structure setStructure = null;
    private void Awake()
    {
        EmpireManager.Instance.onStructureUnlock += AddButton;
        if (player == null) player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        gameObject.SetActive(false);
    }
    void AddButton(Structure structure)
    {
        BuildMenuButton button = Instantiate(buildMenuButton, anchor).GetComponent<BuildMenuButton>();
        button.onBuildMenuButtonClick += StructureDescription;
        button.Set(structure);
        buildMenuButtons.Add(button);
        buildMenuButtons.Sort((BuildMenuButton a, BuildMenuButton b) =>
        {
            return a.structure.name.CompareTo(b.structure.name);
        });
        foreach(var i in buildMenuButtons)
        {
            i.transform.SetSiblingIndex(buildMenuButtons.IndexOf(i));
        }
    }
    void StructureDescription(Structure structure)
    {
        setStructure = structure;
        structureName.text = structure.Name;
        structureIngredients.text = "Ingredients:\n";
        foreach(var i in structure.ingredients)
        {
            structureIngredients.text += $"{i.item.Name}x{i.count}\n";
        }
        structureDescription.text = structure.Describe();
    }
    public void EnterBuildMode()
    {
        if (setStructure == null) return;
        player.EnterBuildMode(setStructure);
    }

}