using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
[RequireComponent(typeof(Structure))]
public class Blueprint : Accessibles, IProgressiveWork
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

    public string Name { get { return origin.Name; } }
    ClaimableIsland m_island;
    public ClaimableIsland island { get { return m_island; } set {
            m_island = value;
            if (instaConstruct) Construct();
        }}
    [SerializeField] GameObject buildingModel;
    Structure origin;

    public int experienceGiven;

    [SerializeField] bool instaConstruct = false;
    void Awake()
    {
        origin = GetComponent<Structure>();
        origin.enabled = false;
        Search(transform);
        foreach (var i in colliders) i.enabled = false;
        buildingModel.SetActive(false);
    }
    List<Collider> colliders = new();
    void Search(Transform part)
    {
        Collider collider = part.GetComponent<Collider>();
        if (collider != null) colliders.Add(collider);
        for(int i = 0; i < part.childCount; i++)
        {
            Search(part.GetChild(i));
        }
    }
    void OnDestroy()
    {

    }
    public void OnWorkFinish(Humanoid worker)
    {
        Construct();
    }
    public void Construct()
    {
        foreach (NPC i in workers)
        {
            i.GetExperience(experienceGiven);
            i.workplace = null;
        }
        origin.enabled = true;
        foreach (var i in colliders) i.enabled = true;
        island.accessibles.Add(origin);
        origin.island = island;
        buildingModel.SetActive(true);
        Object.Destroy(this);
    }
}