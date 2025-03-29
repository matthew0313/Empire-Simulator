using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

[RequireComponent(typeof(NavMeshAgent), typeof(Animator))]public class NPC : Humanoid, IInspectable
{
    public NPC(int maxLv)
    {
        maxLevel = maxLv;
    }

    public virtual string Name { get; }

    //stats
    public float energy;
    const float baseEnergyCost = 1.0f;
    public int level = 1;
    public int experience = 0;
    [SerializeField] int requiredExp;
    public float expFilled = 0.0f;
    int maxLevel;

    //buffs
    public List<Buff> buffs = new List<Buff>();
    public UnityEvent buffUpdater = new UnityEvent();

    //highlight
    List<(MeshRenderer, Material)> parts = new();
    List<T> FindBuffs<T>() where T : BuffData
    {
        List<T> found = new List<T>();
        foreach(Buff i in buffs)
        {
            if (typeof(T).IsAssignableFrom(i.buff.GetType()))
            {
                found.Add(i.buff as T);
            }
        }
        return found;
    }
    public void AddBuff(BuffData buff, float duration)
    {
        foreach(Buff i in buffs)
        {
            if(i.buff == buff)
            {
                i.timeLeft = Mathf.Max(duration, i.timeLeft);
                return;
            }
        }
        Buff adding;
        if (Buff.pool.Count > 0)
        {
            adding = Buff.pool[0];
            Buff.pool.RemoveAt(0);
        }
        else adding = new Buff();
        adding.Set(buff, duration, this);
        buffs.Add(adding);
    }

    //Assignments
    public ClaimableIsland island;
    public Home home
    {
        get 
        {
            if (m_home == null)
            {
                FindAndAssignHome();
            }
            return m_home;
        }
        set
        {
            m_home = value;
        }
    }
    [SerializeField] Home m_home;
    public Accessibles workplace;

    public Equipment workGear = null;
    [SerializeField] Transform equippedAnchor, sheatheAnchor;

    public NavMeshAgent agent
    {
        get { return m_agent; }
    }
    NavMeshAgent m_agent;
    IEnumerator currentNavigation;

    [SerializeField] Animator m_anim;
    public Animator anim { get { return m_anim; } }
    

    protected virtual void Awake()
    {
        m_agent = GetComponent<NavMeshAgent>();
        m_anim = GetComponent<Animator>();
        requiredExp = GetNextExpRequirement();
        for(int i = 0; i < transform.childCount; i++)
        {
            SearchChildren(transform.GetChild(i));
        }
    }
    void SearchChildren(Transform part)
    {
        MeshRenderer tmp = part.GetComponent<MeshRenderer>();
        if (tmp != null)
        {
            parts.Add((tmp, tmp.material));
        }
        for(int i = 0; i < part.childCount; i++)
        {
            SearchChildren(part.GetChild(i));
        }
    }
    public virtual List<IWorkplace> availableJobs()
    {
        return null;
    }
    public void TravelTo(UnityEvent onArrival, Transform travelTo, float maxRemainingDist, float maxTravelTime)
    {
        if (currentNavigation != null) StopCoroutine(currentNavigation);
        currentNavigation = Travel(onArrival, travelTo, maxRemainingDist, maxTravelTime);
        StartCoroutine(currentNavigation);
    }
    const int frameDelay = 5;
    IEnumerator Travel(UnityEvent onArrival, Transform travelTo, float maxRemainingDist, float maxTravelTime)
    {
        if (agent.SetDestination(travelTo.position))
        {
            for(int i = 0; i < frameDelay; i++)
            {
                yield return null;
            }
            agent.isStopped = false;
            anim.SetBool("Walking", true);
            float counter = 0.0f;
            while(agent.remainingDistance > maxRemainingDist)
            {
                yield return null;
                counter += Time.deltaTime;
                if (counter > maxTravelTime)
                {
                    Debug.Log("Could not arrive to the destination in time");
                    transform.position = travelTo.position;
                    break;
                }
            }
        }
        else
        {
            Debug.Log("Failed to set destination");
            transform.position = travelTo.position;
        }
        onArrival.Invoke();
        agent.isStopped = true;
        anim.SetBool("Walking", false);
    }
    protected Layer topLayer;
    public string stateRoute;
    private void Start()
    {
        topLayer.OnStateEnter();
        stateRoute = GetBottomState();
        topLayer.onStateChange.AddListener(delegate {
            stateRoute = GetBottomState();
        });
    }
    private void Update()
    {
        topLayer.OnUpdate();
        foreach(Buff i in buffs)
        {
            i.OnUpdate();
        }
    }
    public string GetBottomState()
    {
        return "top->" + topLayer.GetBottomState();
    }
    public void GetExperience(int exp)
    {
        if (level >= maxLevel) return;
        experience += exp;
        while (experience >= requiredExp)
        {
            experience -= requiredExp;
            LevelUp();
        }
        expFilled = experience / (float)requiredExp;
    }
    public void TakeEnergy()
    {
        energy-=EnergyCost();
    }
    public float EnergyCost()
    {
        float value = baseEnergyCost;
        List<EnergyCostReduction> reduction = FindBuffs<EnergyCostReduction>();
        foreach(var i in reduction)
        {
            value = i.ModifyEnergyCost(value);
        }
        return value;
    }
    public virtual void LevelUp()
    {
        if (level >= maxLevel) return;
        level++;
        requiredExp = GetNextExpRequirement();
        expFilled = experience / (float)requiredExp;
    }

    const int baseExpReq = 100;
    public virtual int GetNextExpRequirement()
    {
        return baseExpReq * level;
    }

    //Finds and assigns this npc a home.
    public void FindAndAssignHome()
    {
        List<Home> homes = island.GetAccessibles<Home>();
        foreach (Home i in homes)
        {
            if (i.maxResidents > i.residents.Count)
            {
                home = i;
                i.AddResident(this);
                return;
            }
        }
    }
    
    //Wields the npc with an equipment.
    public void Equip(Equipment equipment)
    {
        workGear = equipment;
        Sheathe();
    }
    public void Sheathe()
    {
        if (workGear == null) return;
        workGear.model.transform.parent = sheatheAnchor;
        workGear.model.transform.localPosition = Vector3.zero;
        workGear.model.transform.localRotation = Quaternion.identity;
    }
    public void UnSheathe()
    {
        if (workGear == null) return;
        workGear.model.transform.parent = equippedAnchor;
        workGear.model.transform.localPosition = Vector3.zero;
        workGear.model.transform.localRotation = Quaternion.identity;
    }
    public void UseEquipment()
    {
        if (workGear == null) return;
        if(--workGear.durability <= 0)
        {
            Destroy(workGear.model);
            workGear = null;
        }
    }
    public void Highlight(Material material)
    {
        foreach(var i in parts)
        {
            i.Item1.material = material;
        }
    }
    public void Unhighlight()
    {
        foreach(var i in parts)
        {
            i.Item1.material = i.Item2;
        }
    }
    public void Fire()
    {
        if(workplace != null) (workplace as IWorkplace).workers.Remove(this);
        if (home != null) home.RemoveResident(this);
        StopAllCoroutines();
        Destroy(gameObject);
    }
}
