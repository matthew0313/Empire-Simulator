using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

[RequireComponent(typeof(CharacterController))]
public class Player : Humanoid
{
    [Header("General")]
    public bool camYReverse = true;
    public float camSensitivity = 5.0f;
    public float camYRotLimit = 80.0f;
    public Transform camAnchor;

    [Header("Normal")]
    public CinemachineVirtualCamera normalCam;
    public float moveSpeed = 10.0f;
    public float axisBoostSpeed = 3.0f;
    public float runSpeed = 15.0f;
    public float jumpPower = 10.0f;
    public float gravity = 1.0f;

    [Header("Inspecting")]
    public CinemachineVirtualCamera freeCam;
    public float flySpeed = 5.0f;
    public float updownSpeed = 3.0f;

    [Header("BuildMode")]
    public float scanDist = 1000.0f;
    public int gridSize = 1;
    

    Player_TopLayer topLayer;
    public CharacterController controller;

    public delegate void InspectingCallback(bool switchState);
    public event InspectingCallback onInspectChange;

    public delegate void OnBuildModeEnter();
    public event OnBuildModeEnter onBuildModeEnter;

    #region FSMVals
    bool m_inspecting = false;
    public bool inspecting { get { return m_inspecting; } set {
            if (value != m_inspecting) onInspectChange.Invoke(value);
            m_inspecting = value;
        } }
    public bool buildMode { get; private set; }
    public Structure buildingStructure { get; private set; }
    #endregion
    public readonly KeyAction moveW = new KeyAction(KeyCode.W, 0);
    public readonly KeyAction moveA = new KeyAction(KeyCode.A, 0);
    public readonly KeyAction moveS = new KeyAction(KeyCode.S, 0);
    public readonly KeyAction moveD = new KeyAction(KeyCode.D, 0);
    public readonly MouseMovementAction cameraRotation = new MouseMovementAction(false, 0);
    public readonly MouseClickAction cameraRotationClick = new MouseClickAction(MouseType.Right, 10);
    public Action onPause;
    public Material highlightMaterial;

    private void Awake()
    {
        InputManager.AddAction(moveW, moveA, moveS, moveD, cameraRotation, cameraRotationClick);
        topLayer = new Player_TopLayer(this);
        controller = GetComponent<CharacterController>();
    }
    private void Start()
    {
        topLayer.OnStateEnter();
    }
    private void Update()
    {
        if (!updating) return;
        topLayer.OnUpdate();
    }
    bool updating = true;
    CursorLockMode prev;
    bool prevMouseVisible = false;
    public void Pause()
    {
        onPause.Invoke();
        updating = false;
        prev = Cursor.lockState;
        prevMouseVisible = Cursor.visible;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    public void Unpause()
    {
        updating = true;
        Cursor.lockState = prev;
        Cursor.visible = prevMouseVisible;
    }
    public void EnterBuildMode(Structure structure)
    {
        buildingStructure = structure;
        buildMode = true;
        onBuildModeEnter.Invoke();
    }
    public void ExitBuildMode()
    {
        buildMode = false;
    }
}
