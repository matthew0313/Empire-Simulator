using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MapMenuUI : MonoBehaviour
{
    [SerializeField] TMP_Text islandName, islandDesc, travelDistDesc;
    Island selectedIsland;
    private void Awake()
    {
        gameObject.SetActive(false);
    }
    private void OnEnable()
    {
        travelDistDesc.text = $"Your Travel Distance: {EmpireManager.Instance.travelDist}m";
    }
    public void SelectIsland(Island island)
    {
        selectedIsland = island;
        islandName.text = island.name;
        islandDesc.text = $"{island.distance}m\n\n{island.Describe()}";
    }
    public void TravelToIsland()
    {
        if (selectedIsland == null) return;
        EmpireManager.Instance.MoveToIsland(selectedIsland);
    }
}