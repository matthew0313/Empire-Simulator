using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

public class NPC_Rest_GoHome : NPC_State
{
    UnityEvent onArrival = new UnityEvent();
    public NPC_Rest_GoHome(Layer parent, NPC origin) : base(parent, origin)
    {
        onArrival.AddListener(delegate
        {
            parentLayer.SwitchState("Resting");
        });
    }
    public override void OnStateEnter()
    {
        if (character.home == null) return;
        character.TravelTo(onArrival, character.home.entrance, 1.0f, 30.0f);
        base.OnStateEnter();
    }
}
