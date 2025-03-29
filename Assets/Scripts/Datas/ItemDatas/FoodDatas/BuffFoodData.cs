using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

[CreateAssetMenu(fileName = "Buff Food Data", menuName = "Scriptables/Item Datas/Food Data/Buff Food", order = 0)]
public class BuffFoodData : FoodData
{
    [SerializeField] BuffData buff;
    [SerializeField] float buffDuration;
    public override void OnEat(NPC eater)
    {
        eater.AddBuff(buff, buffDuration);
        base.OnEat(eater);
    }
    public override string Describe()
    {
        return base.Describe() + "\n" + "Buff: " + buff.Name + "(" + buff.DescribePower() + ") " + Mathf.FloorToInt(buffDuration) + "s";
    }
}
