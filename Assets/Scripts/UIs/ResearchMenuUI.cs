using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ResearchMenuUI : MonoBehaviour
{
    [SerializeField] TMP_Text researchName, researchIngredients, researchUnlocks;
    ResearchNode selected;
    private void Awake()
    {
        gameObject.SetActive(false);
    }
    public void SelectResearch(ResearchNode node)
    {
        researchName.text = node.Name;
        string content = "Ingredients:\n";
        foreach(var i in node.ingredients)
        {
            content += i.item.Name + "(x" + i.count + ")\n";
        }
        researchIngredients.text = content;
        researchUnlocks.text = node.DescribeUnlock();
        selected = node;
    }
    public void SetActiveResearch()
    {
        if (selected == null) return;
        EmpireManager.Instance.SetResearch(selected);
    }
}