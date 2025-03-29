using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Lumberjack_Work_Working_MoveToTree : NPC_State<Lumberjack>
{
    UnityEvent onArrival = new UnityEvent();
    public Lumberjack_Work_Working_MoveToTree(Layer parent, Lumberjack origin) : base(parent, origin)
    {
        onArrival.AddListener(delegate
        {
            parentLayer.SwitchState("Chopping");
        });
    }
    public override void OnStateEnter()
    {
        if(character.targetTree != null)
        {
            character.TravelTo(onArrival, character.targetTree.transform, 3.0f, 10.0f);
        }
        base.OnStateEnter();
    }
    public override void OnUpdate()
    {
        if (character.targetTree == null) parentLayer.SwitchState("LookForTrees");
        base.OnUpdate();
    }
}
