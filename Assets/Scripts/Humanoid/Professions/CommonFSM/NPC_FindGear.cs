using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class NPC_FindGear<T> : NPC_State where T : EquipmentData
{
    public NPC_FindGear(Layer parent, NPC origin) : base(parent, origin)
    {

    }
    const float searchCD = 5.0f;
    float counter = 0.0f;
    public override void OnStateEnter()
    {
        Search();
        counter = 0.0f;
        base.OnStateEnter();
    }
    public override void OnUpdate()
    {
        if (counter < searchCD) counter += Time.deltaTime;
        else
        {
            counter = 0.0f;
            Search();
        }
        base.OnUpdate();
    }
    void Search()
    {
        List<T> list = EmpireManager.Instance.Search<T>();
        if (list.Count > 0)
        {
            list.Sort((T a, T b) => b.priority.CompareTo(a.priority));
            if(character.workGear != null)
            {
                if (list[0].priority > character.workGear.data.priority)
                {
                    EmpireManager.Instance.RemoveFromStorage(new ItemIntPair(list[0], 1));
                    character.Equip(new Equipment(list[0]));
                }
            }
            else
            {
                EmpireManager.Instance.RemoveFromStorage(new ItemIntPair(list[0], 1));
                character.Equip(new Equipment(list[0]));
            }
            parentLayer.SwitchState("LookForWork");
        }
        else
        {
            if (character.workGear != null) parentLayer.SwitchState("LookForWork");
        }
    }
}