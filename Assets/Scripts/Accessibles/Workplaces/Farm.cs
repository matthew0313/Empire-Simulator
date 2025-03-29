using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Farm : Structure, IProgressiveWork
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
    [SerializeField] Transform m_workplace_Marker;
    [SerializeField] List<NPC> m_workers;

    public float requiredWorkAmount
    {
        get { return m_requiredWorkAmount; }
    }
    public float workedAmount
    {
        get { return m_workedAmount; }
        set { m_workedAmount = value; }

    }
    [SerializeField] float m_requiredWorkAmount;
    [SerializeField] float m_workedAmount;
    #endregion
    public DropTable harvest;

    public int maxWorkers;
    public int experienceGiven;

    [SerializeField] GameObject[] stages;
    GameObject prevStage;

    protected override void Awake()
    {
        if (stages.Length > 0) prevStage = stages[0];
    }

    public void DoWork(Humanoid worker, float workAmount)
    {
        workedAmount += workAmount;
        if (workedAmount >= requiredWorkAmount)
        {
            OnWorkFinish(worker);
            workedAmount = 0.0f;
        }
        for (int i = stages.Length-1; i >= 0; i--)
        {
            if(workedAmount >= (requiredWorkAmount / (stages.Length)) * i)
            {
                prevStage.SetActive(false);
                stages[i].SetActive(true);
                prevStage = stages[i];
                break;
            }
        }
    }
    public void OnWorkFinish(Humanoid worker)
    {
        foreach(NPC i in workers)
        {
            i.GetExperience(experienceGiven);
        }
        EmpireManager.Instance.AddItemToStorage(harvest.GetDrop(worker.Luck()));
    }
    public override string Describe()
    {
        string content = $"{base.Describe()}\n\nMax Workers: {maxWorkers}\nWork needed: {requiredWorkAmount}\nHarvest:\n";
        for(int i = 0; i < harvest.Table.Count; i++)
        {
            content += $"{harvest.Table[i].item.name}({harvest.Table[i].chance}%)";
            if (i < harvest.Table.Count - 1) content += "\n";
        }
        return content;
    }
}
