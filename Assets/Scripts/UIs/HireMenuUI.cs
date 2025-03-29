using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HireMenuUI : MonoBehaviour
{
    [SerializeField] TMP_Text jobName, jobLevel;
    [SerializeField] Dropdown islandOptions;
    NPCHiringPoint hiringPoint;
    void Set(NPCHiringPoint hiringPoint)
    {
        this.hiringPoint = hiringPoint;
        jobName.text = hiringPoint.hiringNPC.name;
        jobLevel.text = $"Level {hiringPoint.NPCLevel}, {hiringPoint.cost}$";
    }
    private void Awake()
    {
        EmpireManager.Instance.onIslandUnlock += (ClaimableIsland island) =>
        {
            islandOptions.AddOptions(new List<string>() { island.name });
        };
        UIManager.Instance.onHireTabOpen += Set;
        gameObject.SetActive(false);
    }
    public void Confirm()
    {
        EmpireManager.Instance.HireNPC(hiringPoint, EmpireManager.Instance.claimedIslands[islandOptions.value]);
    }
}
