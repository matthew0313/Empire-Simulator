using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

public class NPC_HeadToWork : NPC_State
{
    UnityEvent onArrival = new UnityEvent();
    public NPC_HeadToWork(Layer parent, NPC origin) : base(parent, origin)
    {
        onArrival.AddListener(delegate
        {
            if (character.workplace == null)
            {
                parentLayer.SwitchState("LookForWork");
                return;
            }
            parentLayer.SwitchState("Working");
        });
    }
    public override void OnStateEnter()
    {
        character.TravelTo(onArrival, (character.workplace as IWorkplace).workplace_Marker, 0.5f, 30.0f);
        base.OnStateEnter();
    }
}
