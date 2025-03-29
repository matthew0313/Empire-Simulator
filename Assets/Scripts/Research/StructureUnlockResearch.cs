using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StructureUnlockResearch : ResearchNode
{
    [SerializeField] List<GameObject> unlockingStructure;
    public override void OnResearchComplete()
    {
        foreach(var i in unlockingStructure)
        {
            EmpireManager.Instance.UnlockStructure(i);
        }
        base.OnResearchComplete();
    }
    public override string DescribeUnlock()
    {
        string content = base.DescribeUnlock() + "\n";
        foreach(var i in unlockingStructure)
        {
            content += i.name + "\n";
        }
        return content;
    }
}
