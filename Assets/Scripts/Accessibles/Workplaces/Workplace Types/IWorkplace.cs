using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public interface IWorkplace
{
    public Transform workplace_Marker
    {
        get; set;
    }

    public List<NPC> workers
    {
        get; set;
    }

    public int levelRequirement
    {
        get { return 0; }
    }
}
