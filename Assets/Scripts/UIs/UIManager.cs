using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }
    public UIManager()
    {
        Instance = this;
    }
    [Header("Others")]
    [SerializeField] Transform canvas;
    [SerializeField] GameObject screenMarker;

    [Header("Menus")]
    [SerializeField] GameObject inventoryMenu;
    [SerializeField] GameObject buildMenu;
    [SerializeField] GameObject researchMenu;
    [SerializeField] GameObject mapMenu;
    [SerializeField] GameObject hireConfirm;
    [SerializeField] GameObject inspectTab;
    GameObject m_currentOpenMenu = null;
    GameObject currentOpenMenu
    {
        get
        {
            return m_currentOpenMenu;
        }
        set
        {
            if (value == null) player.Unpause();
            else player.Pause();
            m_currentOpenMenu = value;
        }
    }
    [Header("Logs")]
    [SerializeField] Transform logAnchor;
    [SerializeField] GameObject logPrefab;
    Pooler logPool;

    [Header("ResearchProgress")]
    [SerializeField] GameObject researchProgress;
    [SerializeField] TMP_Text researchName, researchProgressText;
    [SerializeField] RectTransform researchProgressBar;
    Player player;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        player.onBuildModeEnter += CloseMenu;
        EmpireManager.Instance.onResearchSet += ResearchTab;
        EmpireManager.Instance.onResearchProgress += ResearchTabProgress;
        player.onInspectChange += (bool inspecting) =>
        {
            CloseMenu();
        };
        logPool = new Pooler(logPrefab, 30, 0);
        InputManager.AddAction(inventoryOpen, buildMenuOpen, researchMenuOpen, mapMenuOpen, closeMenu);
    }
    readonly KeyAction inventoryOpen = new KeyAction(KeyCode.E, 0);
    readonly KeyAction buildMenuOpen = new KeyAction(KeyCode.B, 0);
    readonly KeyAction researchMenuOpen = new KeyAction(KeyCode.R, 0);
    readonly KeyAction mapMenuOpen = new KeyAction(KeyCode.M, 0);
    readonly KeyAction closeMenu = new KeyAction(KeyCode.Escape, 0);
    private void Update()
    {
        if (inventoryOpen.GetKeyDown())
        {
            OpenMenu(inventoryMenu);
        }
        if (buildMenuOpen.GetKeyDown() && player.inspecting && EmpireManager.Instance.currentIsland is ClaimableIsland)
        {
            OpenMenu(buildMenu);
        }
        if (researchMenuOpen.GetKeyDown())
        {
            OpenMenu(researchMenu);
        }
        if(mapMenuOpen.GetKeyDown() && !player.inspecting)
        {
            OpenMenu(mapMenu);
        }
        if (closeMenu.GetKeyDown())
        {
            CloseMenu();
        }
        Vector3 camPos = Camera.main.transform.position;
        Quaternion camRot = Camera.main.transform.rotation;
    }
    void OpenMenu(GameObject menu)
    {
        if(currentOpenMenu == menu) 
        {
            menu.SetActive(false);
            currentOpenMenu = null;
        }
        else
        {
            if(currentOpenMenu != null) currentOpenMenu.SetActive(false);
            menu.SetActive(true);
            currentOpenMenu = menu;
        }
    }
    public void CloseMenu()
    {
        if(currentOpenMenu != null) currentOpenMenu.SetActive(false);
        currentOpenMenu = null;
    }
    List<Log> logs = new();
    public void Log(string title, string message, Color titleColor)
    {
        if(logs.Count >= 3)
        {
            logs[0].Delete();
        }
        Log log = logPool.GetObject(logAnchor).GetComponent<Log>();
        log.Set(title, message, titleColor);
        log.transform.localScale = Vector2.one;
        log.onLogDelete.AddListener(() => { logs.Remove(log); logPool.ReleaseObject(log.gameObject); });
        logs.Add(log);
    }
    public GameObject InstantiateInCanvas(GameObject prefab)
    {
        return Instantiate(prefab, canvas);
    }
    void ResearchTab(ResearchNode node)
    {
        researchName.text = node.Name;
        researchProgressBar.localScale = new Vector2(node.workedAmount / node.requiredWorkAmount, 1.0f);
        researchProgressText.text = node.workedAmount + "/" + node.requiredWorkAmount;
        researchProgress.SetActive(true);
    }
    void ResearchTabProgress(float worked, float required)
    {
        if (worked >= required) researchProgress.SetActive(false);
        else
        {
            researchProgressBar.localScale = new Vector2(worked/required, 1.0f);
            researchProgressText.text = Mathf.CeilToInt(worked) + "/" + Mathf.CeilToInt(required);
        }
    }
    public Action<NPCHiringPoint> onHireTabOpen;
    public void HireTab(NPCHiringPoint hiringPoint)
    {
        onHireTabOpen.Invoke(hiringPoint);
        OpenMenu(hireConfirm);
    }
    public void ScreenMarker(bool active)
    {
        screenMarker.SetActive(active);
    }
    public Action<IInspectable> onInspectTabOpen;
    public void InspectTab(IInspectable target)
    {
        onInspectTabOpen.Invoke(target);
        OpenMenu(inspectTab);
    }
}
