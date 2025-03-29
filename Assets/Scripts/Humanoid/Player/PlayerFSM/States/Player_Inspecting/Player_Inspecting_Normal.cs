using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Inspecting_Normal : Player_State
{
    public Player_Inspecting_Normal(Layer parent, Player player) : base(parent, player)
    {
        player.onPause += () => { 
            if (highlighting as MonoBehaviour != null)
            {
                highlighting.Unhighlight();
                highlighting = null;
            }
        };
    }
    public override void OnStateEnter()
    {
        base.OnStateEnter();
        InputManager.AddAction(inspect);
    }
    IInspectable highlighting = null;
    readonly MouseClickAction inspect = new MouseClickAction(MouseType.Left, 20);
    public override void OnUpdate()
    {
        if (player.buildMode == true)
        {
            parentLayer.SwitchState("BuildMode");
            return;
        }
        RaycastHit hit;
        if(Physics.Raycast(Camera.main.ScreenToWorldPoint(InputManager.Input_MousePos()), Camera.main.transform.forward, out hit, player.scanDist, LayerMask.GetMask("NPC", "Structure")))
        {
            IInspectable target = hit.transform.root.GetComponent<IInspectable>();
            if(highlighting as MonoBehaviour != null && highlighting != target)
            {
                highlighting.Unhighlight();
            }
            highlighting = target;
            highlighting.Highlight(player.highlightMaterial);
            if (inspect.GetMouseDown())
            {
                highlighting.Unhighlight();
                UIManager.Instance.InspectTab(highlighting);
            }
        }
        else if(highlighting as MonoBehaviour != null)
        {
            highlighting.Unhighlight();
            highlighting = null;
        }
        base.OnUpdate();
    }
    public override void OnStateExit()
    {
        InputManager.RemoveAction(inspect);
        base.OnStateExit();
    }
}
