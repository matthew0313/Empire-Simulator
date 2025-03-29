using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Forest : Accessibles, IWorkplace
{
    public Transform workplace_Marker
    {
        get { return m_workplace_Marker; }
        set { }
    }
    public List<NPC> workers
    {
        get { return m_workers; }
        set { m_workers = value; }
    }
    [SerializeField] Transform m_workplace_Marker;
    [SerializeField] List<NPC> m_workers;
    public List<TreeSpawn> trees = new List<TreeSpawn>();
}
