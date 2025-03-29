using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Pool;

public class EmpireManager : MonoBehaviour
{
    public static EmpireManager Instance { get; private set; }
    public EmpireManager()
    {
        Instance = this;
    }

    //island
    public List<ClaimableIsland> claimedIslands = new List<ClaimableIsland>();
    public Island currentIsland;
    [SerializeField] float m_travelDist = 0.0f;
    public float travelDist { get { return m_travelDist; } }

    //time
    public int hour = 6;
    const float hourTime = 30.0f;
    [SerializeField] float counter = 0.0f;
    Transform directionalLight;

    //schedule
    public static int workStart = 6;
    public static int workEnd = 22;

    //storage
    public Dictionary<ItemData, int> storage = new Dictionary<ItemData, int>();
    [SerializeField] ItemData addingItem;
    [SerializeField] int addAmount;
    [SerializeField] ItemIntPair[] startingStorage;

    //Events
    public UnityEvent newdayStart = new UnityEvent();

    //Statistics
    public int money = 0;
    public float npcDamageMultiplier { get; private set; } = 1.0f;
    public float npcWorkMultiplier { get; private set; } = 1.0f;
    [SerializeField] GameObject[] startingStructures;
    public List<Structure> unlockedStructures { get; } = new();

    public delegate void StructureUnlockCallback(Structure structure);
    public event StructureUnlockCallback onStructureUnlock;

    public delegate void IslandUnlockCallback(ClaimableIsland island);
    public event IslandUnlockCallback onIslandUnlock;

    public ResearchNode currentResearch { get; private set; } = null;
    Transform player, freecam;
    private void Awake()
    {
        directionalLight = GameObject.FindGameObjectWithTag("DirectionalLight").transform;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        freecam = GameObject.FindGameObjectWithTag("Freecam").transform;
        foreach (var i in startingStorage)
        {
            AddItemToStorage(i);
        }
    }
    private void Start()
    {
        foreach (var i in startingStructures) UnlockStructure(i);
        UnlockIsland(GameObject.FindGameObjectWithTag("StartingIsland").GetComponent<ClaimableIsland>());
        directionalLight.rotation = Quaternion.Euler(-90 + 15 * hour, -30, 0);
        if (currentIsland is ClaimableIsland) freecam.position = (currentIsland as ClaimableIsland).defaultFreecamPos.position;
    }
    public void UnlockStructure(GameObject structurePrefab)
    {
        Structure tmp = Instantiate(structurePrefab, new Vector3(0.0f, -1000.0f, 0.0f), Quaternion.identity).GetComponent<Structure>();
        unlockedStructures.Add(tmp);
        onStructureUnlock.Invoke(tmp);
    }
    public void UnlockIsland(ClaimableIsland island)
    {
        claimedIslands.Add(island);
        onIslandUnlock.Invoke(island);
    }

    #region StorageFunctions
    public List<T> Search<T>() where T : ItemData
    {
        List<T> list = new List<T>();
        foreach (ItemData item in storage.Keys)
        {
            if (typeof(T).IsAssignableFrom(item.GetType()) && storage[item] > 0)
            {
                list.Add(item as T);
            }
        }
        return list;
    }
    public int Get(ItemData item)
    {
        if (!storage.ContainsKey(item)) return 0;
        else return storage[item];
    }
    public void AddToStorage()
    {
        AddItemToStorage(new ItemIntPair(addingItem, addAmount));
    }
    public void AddItemToStorage(List<ItemIntPair> add)
    {
        foreach (ItemIntPair i in add)
        {
            AddItemToStorage(i);
        }
    }
    public delegate void StorageAddCallbacks(ItemData addedItem, int newAmount);
    public event StorageAddCallbacks onStorageEdit;
    public void AddItemToStorage(ItemIntPair add)
    {
        if (storage.ContainsKey(add.item))
        {
            storage[add.item] += add.count;
        }
        else
        {
            storage.Add(add.item, add.count);
        }
        onStorageEdit.Invoke(add.item, storage[add.item]);
    }
    public void RemoveFromStorage(ItemIntPair remove)
    {
        if (!storage.ContainsKey(remove.item)) return;
        storage[remove.item] -= remove.count;
        onStorageEdit.Invoke(remove.item, storage[remove.item]);
    }
    #endregion

    private void Update()
    {
        if (counter < hourTime) counter += Time.deltaTime;
        else
        {
            PassHour();
        }
        directionalLight.rotation = Quaternion.Euler(-90 + 15 * (hour+counter/hourTime), -30, 0);
    }
    public void PassHour()
    {
        if (++hour == 24)
        {
            hour = 0;
            newdayStart.Invoke();
        }
        counter = 0.0f;
    }
    public void SubtractHour()
    {
        if (--hour < 0) hour = 0;
        counter = 0.0f;
    }
    public delegate void ResearchSetCallback(ResearchNode node);
    public event ResearchSetCallback onResearchSet;
    public void SetResearch(ResearchNode node)
    {
        if (!node.available || node.completed) return;
        if (currentResearch == node)
        {
            UIManager.Instance.Log("Research already set", "You already have this research set as active research.", Color.yellow);
            return;
        }
        if (!node.paid)
        {
            bool can = true;
            foreach (var i in node.ingredients)
            {
                if (Get(i.item) < i.count)
                {
                    can = false; break;
                }
            }
            if (!can)
            {
                UIManager.Instance.Log("Not enough ingredients", "You do not have enough ingredients to start this research.", Color.red);
                return;
            }
            foreach (var i in node.ingredients)
            {
                RemoveFromStorage(i);
            }
            node.paid = true;
        }
        currentResearch = node;
        onResearchSet.Invoke(node);
    }
    public delegate void ResearchProgressCallback(float worked, float required);
    public event ResearchProgressCallback onResearchProgress; 
    public void ProgressResearch(float workAmount)
    {
        currentResearch.Progress(workAmount);
        onResearchProgress.Invoke(currentResearch.workedAmount, currentResearch.requiredWorkAmount);
        if (currentResearch.workedAmount >= currentResearch.requiredWorkAmount)
        {
            currentResearch.OnResearchComplete();
            currentResearch = null;
        }
    }
    public void TravelDistEdit(float newDist)
    {
        if (newDist < travelDist) return;
        m_travelDist = newDist;
    }
    public void MoveToIsland(Island isle)
    {
        if(travelDist < isle.distance)
        {
            UIManager.Instance.Log("Unable to travel", "Your empire does not have enough technology to travel this far. Try researching for better ships.", Color.red);
            return;
        }
        if(isle is ClaimableIsland && !claimedIslands.Contains(isle as ClaimableIsland))
        {
            if(money >= (isle as ClaimableIsland).unlockPrice)
            {
                money -= (isle as ClaimableIsland).unlockPrice;
                UnlockIsland(isle as ClaimableIsland);
                UIManager.Instance.Log("Island Unlocked", $"You unlocked {isle.name} for {(isle as ClaimableIsland).unlockPrice}$.", Color.green);
            }
            else
            {
                UIManager.Instance.Log("Not enough money", "You do not have enough money to unlock this island.", Color.red);
            }
        }
        currentIsland = isle;
        player.position = isle.spawnPoint.position;
        if (isle is ClaimableIsland) freecam.position = (isle as ClaimableIsland).defaultFreecamPos.position;
    }
    public delegate void NPCHireCallback(NPC npc, ClaimableIsland island);
    public event NPCHireCallback onNPCHire;
    public void HireNPC(NPCHiringPoint point, ClaimableIsland island)
    {
        if (money < point.cost)
        {
            UIManager.Instance.Log("Not enough money", "You do not have enough money to hire this NPC.", Color.red);
            return;
        }
        money -= point.cost;
        NPC npc = Instantiate(point.hiringNPC).GetComponent<NPC>();
        npc.island = island;
        npc.level = point.NPCLevel;
        //npc.transform.position = island.spawnPoint.position;
        npc.agent.Warp(island.spawnPoint.position);
        if(onNPCHire != null) onNPCHire.Invoke(npc, island);
        UIManager.Instance.Log("NPC Hired", $"You successfully hired a {point.hiringNPC.name}(level {point.NPCLevel}) into {island.name}.", Color.green);
    }
}
