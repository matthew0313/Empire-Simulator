using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class NPC_Rest : NPC_Layer
{
    bool earlyQuit = false;
    bool added = false;
    public NPC_Rest(Layer parent, NPC origin) : base(parent, origin)
    {
        defaultState = new NPC_Rest_GoHome(this, origin);
        AddState("GoHome", defaultState);
        AddState("Resting", new NPC_Rest_Resting(this, origin));
    }
    public override void OnStateEnter()
    {
        if (EmpireManager.Instance.hour < EmpireManager.workEnd && EmpireManager.Instance.hour >= EmpireManager.workStart)
        {
            earlyQuit = true;
            if (!added)
            {
                EmpireManager.Instance.newdayStart.AddListener(delegate
                {
                    earlyQuit = false;
                });
                added = true;
            }
        }
        if(character.workplace != null) (character.workplace as IWorkplace).workers.Remove(character);
        character.workplace = null;
        base.OnStateEnter();
    }
    public override void OnUpdate()
    {
        if (EmpireManager.Instance.hour >= EmpireManager.workStart && EmpireManager.Instance.hour < EmpireManager.workEnd && earlyQuit == false)
        {
            parentLayer.SwitchState("Work");
        }
        base.OnUpdate();
    }
}
