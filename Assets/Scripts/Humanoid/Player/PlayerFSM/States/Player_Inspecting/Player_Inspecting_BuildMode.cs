using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Player_Inspecting_BuildMode : Player_State
{
    public Player_Inspecting_BuildMode(Layer parent, Player player) : base(parent, player)
    {
        
    }
    GameObject buildingModel = null;
    Structure buildingInfo = null;
    readonly KeyAction buildModeExit = new KeyAction(KeyCode.B, 10);
    readonly KeyAction buildModeRotate = new KeyAction(KeyCode.R, 10);
    readonly KeyAction buildModeContinue = new KeyAction(KeyCode.LeftShift, 10);
    readonly MouseClickAction buildModePlace = new MouseClickAction(MouseType.Left, 10);
    bool added = false;
    public override void OnStateEnter()
    {
        if (!(EmpireManager.Instance.currentIsland is ClaimableIsland) || player.buildMode == false)
        {
            parentLayer.SwitchState("Normal");
            return;
        }
        buildingModel = MonoBehaviour.Instantiate(player.buildingStructure.gameObject);
        buildingInfo = player.buildingStructure;
        if (!added)
        {
            added = true;
            InputManager.AddAction(buildModeExit, buildModeRotate, buildModeContinue, buildModePlace);
        }
        base.OnStateEnter();
    }
    int rotCount = 0;
    public override void OnUpdate()
    {
        if (player.buildMode == false) parentLayer.SwitchState("Normal");
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.ScreenToWorldPoint(InputManager.Input_MousePos()), Camera.main.transform.forward, out hit, player.scanDist, LayerMask.GetMask("BuildArea")))
        {
            buildingModel.transform.position = new Vector3(Mathf.RoundToInt(hit.point.x / player.gridSize) * player.gridSize, hit.transform.position.y + 1.5f, Mathf.RoundToInt(hit.point.z / player.gridSize) * player.gridSize);
            if (buildModeRotate.GetKeyDown())
            {
                rotCount++; rotCount %= 4;
                buildingModel.transform.rotation = Quaternion.Euler(0, rotCount*90, 0);
            }
            if (buildModePlace.GetMouseDown())
            {
                foreach(ItemIntPair i in buildingInfo.ingredients)
                {
                    if (EmpireManager.Instance.Get(i.item) < i.count)
                    {
                        UIManager.Instance.Log("Unable to place", "You do not have enough ingredients to place this building.", Color.red);
                        return;
                    }
                }
                RaycastHit[] tmp = Physics.BoxCastAll(buildingModel.transform.position + new Vector3(0, 1, 0), buildingInfo.foundation.localScale * 0.5f, Vector3.down, Quaternion.Euler(0, rotCount*90, 0), Mathf.Infinity, LayerMask.GetMask("Structure"));
                foreach(var i in tmp)
                {
                    if (!i.collider.transform.IsChildOf(buildingModel.transform))
                    {
                        UIManager.Instance.Log("Unable to place", "There's something in the way.", Color.red);
                        return;
                    }
                }
                foreach (ItemIntPair i in buildingInfo.ingredients)
                {
                    EmpireManager.Instance.RemoveFromStorage(i);
                }
                Blueprint blueprint = buildingModel.GetComponent<Blueprint>();
                blueprint.island = EmpireManager.Instance.currentIsland as ClaimableIsland;
                (EmpireManager.Instance.currentIsland as ClaimableIsland).accessibles.Add(blueprint);
                buildingModel = null;
                if (buildModeContinue.GetKey())
                {
                    OnStateEnter();
                }
                else
                {
                    player.ExitBuildMode();
                    parentLayer.SwitchState("Normal");
                }
            }
        }
        if (buildModeExit.GetKeyDown())
        {
            player.ExitBuildMode();
            parentLayer.SwitchState("Normal");
        }
        base.OnUpdate();
    }
    public override void OnStateExit()
    {
        if (buildingModel != null) MonoBehaviour.Destroy(buildingModel);
        added = false;
        InputManager.RemoveAction(buildModeExit, buildModeRotate, buildModeContinue, buildModePlace);
        base.OnStateExit();
    }
}
