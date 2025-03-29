using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Miner_Work_Working_MoveToOre : NPC_State<Miner>
{
    UnityEvent onArrival = new UnityEvent();
    public Miner_Work_Working_MoveToOre(Layer parent, Miner origin) : base(parent, origin)
    {
        onArrival.AddListener(delegate
        {
            parentLayer.SwitchState("Mining");
        });
    }
    public override void OnStateEnter()
    {
        if(character.targetNode != null)
        {
            character.TravelTo(onArrival, character.targetNode.transform, 3.0f, 10.0f);
        }
        base.OnStateEnter();
    }
    public override void OnUpdate()
    {
        if (character.targetNode == null) parentLayer.SwitchState("LookForOres");
        base.OnUpdate();
    }
}
