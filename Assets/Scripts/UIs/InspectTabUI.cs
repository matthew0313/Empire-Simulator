using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using JetBrains.Annotations;
using System;

public class InspectTabUI : MonoBehaviour
{
    [Header("General")]
    [SerializeField] TMP_Text title;

    [Header("NPC")]
    [SerializeField] GameObject NPCTab;
    [SerializeField] TMP_Text level, expProgress;
    [SerializeField] Transform expProgressBar;

    [Header("Miner")]
    [SerializeField] GameObject MinerTab;
    [SerializeField] Dropdown priority;
    [SerializeField] Dropdown oreSelect;
    Dictionary<int, OreNodeData> oreNodeDatas = new();
    private void Awake()
    {
        priority.onValueChanged.AddListener(MinerPriorityChange);
        UIManager.Instance.onInspectTabOpen += Set;
        List<string> oreOptions = new();
        for(int i = 0; i < OreNodeData.oreNodeDatas.Count; i++)
        {
            oreOptions.Add(OreNodeData.oreNodeDatas[i].name);
            this.oreNodeDatas.Add(i, OreNodeData.oreNodeDatas[i]);
        }
        oreSelect.AddOptions(oreOptions);
        oreSelect.onValueChanged.AddListener((int oreNum) => { (inspecting as Miner).targetOreType = oreNodeDatas[oreNum]; });
        gameObject.SetActive(false);
    }
    IInspectable inspecting;
    void Set(IInspectable target)
    {
        inspecting = target;
        if(target is NPC)
        {
            title.text = (target as NPC).Name;
            NPCTab.SetActive(true);
            level.text = $"Level {(target as NPC).level}";
            expProgress.text = $"Experience: {(target as NPC).experience}/{(target as NPC).GetNextExpRequirement()}";
            expProgressBar.localScale = new Vector2((target as NPC).expFilled, 1.0f);
            if(target is Miner)
            {
                MinerTab.SetActive(true);
            }
            else
            {
                MinerTab.SetActive(false);
            }
        }
        else
        {
            NPCTab.SetActive(false);
        }
        if(target is Structure)
        {
            title.text = (target as Structure).Name;
        }
    }
    void MinerPriorityChange(int priority)
    {
        (inspecting as Miner).ChangePriority((Miner_OrePriority)priority);
        if((Miner_OrePriority)priority == Miner_OrePriority.preferSetOre)
        {
            oreSelect.gameObject.SetActive(true);
        }
        else
        {
            oreSelect.gameObject.SetActive(false);
        }
    }
    public void Remove()
    {
        if (inspecting == null) return;
        if (inspecting is NPC) (inspecting as NPC).Fire();
        else if (inspecting is Structure) (inspecting as Structure).RemoveStructure();
        UIManager.Instance.CloseMenu();
    }
}
