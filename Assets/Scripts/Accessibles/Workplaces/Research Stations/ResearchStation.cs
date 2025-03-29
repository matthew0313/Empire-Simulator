using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class ResearchStation : Structure, IWorkplace
{
    #region interface variables
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
    public int levelRequirement
    {
        get { return m_levelRequirement; }
    }
    public int maxWorkers
    {
        get { return m_maxWorkers; }
    }
    [SerializeField] Transform m_workplace_Marker;
    [SerializeField] List<NPC> m_workers;
    [SerializeField] int m_levelRequirement;
    [SerializeField] int m_maxWorkers;
    #endregion
    [SerializeField] int tier = 1;

    public bool DoWork(Humanoid worker, float workAmount)
    {
        if (EmpireManager.Instance.currentResearch == null || EmpireManager.Instance.currentResearch.tier > tier) return false;
        EmpireManager.Instance.ProgressResearch(workAmount);
        return true;
    }
    public override string Describe()
    {
        return $"{base.Describe()}\n\nMax Workers: {maxWorkers}\nTier: {tier}";
    }
}