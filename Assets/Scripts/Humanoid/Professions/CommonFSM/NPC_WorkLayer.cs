using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class NPC_WorkLayer<T> : NPC_Layer<T> where T : NPC
{
    public NPC_WorkLayer(Layer parent, T origin) : base(parent, origin)
    {
        defaultState = new NPC_LookForWork(this, origin);
        AddState("LookForWork", defaultState);
        AddState("HeadToWork", new NPC_HeadToWork(this, origin));
    }
    public override void OnStateEnter()
    {
        base.OnStateEnter();
        character.energy = 0;
        character.home.OnRest(character);
        List<FoodData> food = EmpireManager.Instance.Search<FoodData>();
        if (food.Count == 0)
        {
            character.energy = 10;
        }
        else
        {
            food.Sort((FoodData a, FoodData b) => a.energy.CompareTo(b.energy));
            EmpireManager.Instance.RemoveFromStorage(new ItemIntPair(food[0], 1));
            food[0].OnEat(character);
        }
    }
    public override void OnUpdate()
    {
        if (character.workplace == null)
        {
            if(CurrentState() != "FindGear")
            {
                SwitchState("LookForWork");
                return;
            }
        }
        if (character.energy <= 0)
        {
            character.energy = 0;
            parentLayer.SwitchState("Rest"); return;
        }
        if (EmpireManager.Instance.hour >= EmpireManager.workEnd)
        {
            parentLayer.SwitchState("Rest"); return;
        }
        base.OnUpdate();
    }
}
