using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using Unity.Profiling;
using UnityEngine;
using UnityEngine.Events;

public class InputManager : MonoBehaviour
{
    static Dictionary<KeyCode, List<KeyAction>> actions = new();
    public static void AddAction(KeyAction action)
    {
        if(!actions.ContainsKey(action.key))
        {
            actions.Add(action.key, new List<KeyAction>());
        }
        actions[action.key].Add(action);
        actions[action.key].Sort((KeyAction a, KeyAction b) => { return b.priority.CompareTo(a.priority); });
        foreach(KeyAction act in actions[action.key])
        {
            act.keyState = KeyState.None;
        }
    }
    public static void AddAction(params InputAction[] action)
    {
        foreach (var i in action)
        {
            if (i is KeyAction) AddAction(i as KeyAction);
            else if(i is MouseClickAction) AddAction(i as MouseClickAction);
            else if(i is MouseMovementAction) AddAction(i as MouseMovementAction);
        }
    }
    static Dictionary<MouseType, List<MouseClickAction>> mouseClickActions = new();
    static List<MouseMovementAction> mouseMovementActions = new();
    public static void AddAction(MouseClickAction action)
    {
        if (!mouseClickActions.ContainsKey(action.mouseType))
        {
            mouseClickActions.Add(action.mouseType, new List<MouseClickAction>());
        }
        mouseClickActions[action.mouseType].Add(action);
        mouseClickActions[action.mouseType].Sort((MouseClickAction a, MouseClickAction b) => { return b.priority.CompareTo(a.priority); });
        foreach(MouseClickAction act in mouseClickActions[action.mouseType])
        {
            act.keyState = KeyState.None;
        }
    }
    public static void AddAction(MouseMovementAction action)
    {
        mouseMovementActions.Add(action);
        mouseMovementActions.Sort((MouseMovementAction a, MouseMovementAction b) => b.priority.CompareTo(a.priority));
        foreach(MouseMovementAction act in mouseMovementActions)
        {
            act.axisX = 0.0f;
            act.axisY = 0.0f;
        }
    }
    public static void RemoveAction(KeyAction action)
    {
        actions[action.key].Remove(action);
    }
    public static void RemoveAction(params InputAction[] action)
    {
        foreach(var i in action)
        {
            if(i is KeyAction) RemoveAction(i as KeyAction);
            else if (i is MouseClickAction) RemoveAction(i as MouseClickAction);
            else if(i is MouseMovementAction) RemoveAction(i as MouseMovementAction);
        }
    }
    public static void RemoveAction(MouseClickAction action)
    {
        mouseClickActions[action.mouseType].Remove(action);
    }
    public static void RemoveAction(MouseMovementAction action)
    {
        mouseMovementActions.Remove(action);
    }
    void Update()
    {
        foreach(KeyCode key in actions.Keys)
        {
            if (actions[key].Count == 0) continue;
            if (Input.GetKeyDown(key))
            {
                actions[key][0].keyState = KeyState.KeyDown;
                continue;
            }
            if (Input.GetKey(key))
            {
                actions[key][0].keyState = KeyState.KeyPressed;
                continue;
            }
            if (Input.GetKeyUp(key))
            {
                actions[key][0].keyState = KeyState.KeyUp;
                continue;
            }
            actions[key][0].keyState = KeyState.None;
        }
        foreach(MouseType mouse in mouseClickActions.Keys)
        {
            if (mouseClickActions[mouse].Count == 0) continue;
            if (Input.GetMouseButtonDown((int)mouse))
            {
                mouseClickActions[mouse][0].keyState = KeyState.KeyDown;
                continue;
            }
            if (Input.GetMouseButton((int)mouse))
            {
                mouseClickActions[mouse][0].keyState = KeyState.KeyPressed;
                continue;
            }
            if (Input.GetMouseButtonUp((int)mouse))
            {
                mouseClickActions[mouse][0].keyState = KeyState.KeyUp;
                continue;
            }
            mouseClickActions[mouse][0].keyState = KeyState.None;
        }
        if(mouseMovementActions.Count > 0)
        {
            mouseMovementActions[0].axisX = Input.GetAxis("Mouse X");
            mouseMovementActions[0].axisY = Input.GetAxis("Mouse Y");
        }
    }
    public static float Input_HorizontalAxis()
    {
        return Input.GetAxis("Horizontal");
    }
    public static float Input_VerticalAxis()
    {
        return Input.GetAxis("Vertical");
    }
    public static bool Input_IsRunning()
    {
        return Input.GetKey(KeyCode.LeftShift);
    }
    public static bool Input_Jump()
    {
        return Input.GetKeyDown(KeyCode.Space);
    }
    public static bool Input_SwitchToInspect()
    {
        return Input.GetKeyDown(KeyCode.F);
    }
    public static bool Input_CameraUp()
    {
        return Input.GetKey(KeyCode.E);
    }
    public static bool Input_CameraDown()
    {
        return Input.GetKey(KeyCode.Q);
    }
    public static bool Input_InventoryOpen()
    {
        return Input.GetKeyDown(KeyCode.E);
    }
    public static bool Input_BuildMenuOpen()
    {
        return Input.GetKeyDown(KeyCode.Tab);
    }
    public static bool Input_ResearchMenuOpen()
    {
        return Input.GetKeyDown(KeyCode.N);
    }
    public static bool Input_MapMenuOpen()
    {
        return Input.GetKeyDown(KeyCode.M);
    }
    public static bool Input_CloseTab()
    {
        return Input.GetKeyDown(KeyCode.Escape);
    }
    public static bool Input_ExitBuildMode()
    {
        return Input.GetKeyDown(KeyCode.B);
    }
    public static bool Input_BuildModePlace()
    {
        return Input.GetMouseButtonDown(0);
    }
    public static Vector3 Input_MousePos()
    {
        return Input.mousePosition;
    }
    public static bool Input_BuildModeContinue()
    {
        return Input.GetKey(KeyCode.LeftShift);
    }
    public static bool Input_BuildModeRotate()
    {
        return Input.GetKeyDown(KeyCode.R);
    }
}
public class InputAction
{

}
public class KeyAction : InputAction
{
    public KeyAction(KeyCode key, int priority)
    {
        this.priority = priority;
        this.key = key;
    }
    public readonly int priority = 0;
    public readonly KeyCode key = KeyCode.None;
    public KeyState keyState = KeyState.None;

    public bool GetKeyDown()
    {
        return keyState == KeyState.KeyDown;
    }
    public bool GetKey()
    {
        return (keyState == KeyState.KeyDown || keyState == KeyState.KeyPressed);
    }
    public bool GetKeyUp()
    {
        return keyState == KeyState.KeyUp;
    }
}
public enum KeyState
{
    None,
    KeyDown,
    KeyPressed,
    KeyUp
}
public class MouseMovementAction : InputAction
{
    public MouseMovementAction(bool raw, int priority)
    {
        this.raw = raw;
        this.priority = priority;
    }
    public readonly int priority;
    public readonly bool raw = false;
    public float axisX = 0.0f, axisY = 0.0f;
    public float GetAxisX()
    {
        return raw ? (axisX > 0.0f ? 1 : axisX < 0.0f ? -1 : 0) : axisX;
    }
    public float GetAxisY()
    {
        return raw ? (axisY > 0.0f ? 1 : axisY < 0.0f ? -1 : 0) : axisY;
    }
}
public class MouseClickAction : InputAction
{
    public MouseClickAction(MouseType mouseType, int priority)
    {
        this.mouseType = mouseType;
        this.priority = priority;
    }
    public readonly int priority;
    public readonly MouseType mouseType = 0;
    public KeyState keyState = KeyState.None;
    public bool GetMouseDown()
    {
        return keyState == KeyState.KeyDown;
    }
    public bool GetMouse()
    {
        return (keyState == KeyState.KeyDown || keyState == KeyState.KeyPressed);
    }
    public bool GetMouseUp()
    {
        return keyState == KeyState.KeyUp;
    }
}
public enum MouseType
{
    Left = 0,
    Right = 1
}