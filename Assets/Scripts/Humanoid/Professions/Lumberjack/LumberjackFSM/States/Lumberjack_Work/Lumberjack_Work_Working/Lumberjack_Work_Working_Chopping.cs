using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lumberjack_Work_Working_Chopping : NPC_State<Lumberjack>
{
    const float miningCD = 1.0f;
    float counter = 0.0f;
    public Lumberjack_Work_Working_Chopping(Layer parent, Lumberjack origin) : base(parent, origin)
    {
        
    }
    public override void OnStateEnter()
    {
        counter = 0.0f;
        character.UnSheathe();
        character.anim.Play("Chopping", 1);
        base.OnStateEnter();
    }
    public override void OnUpdate()
    {
        if (character.targetTree == null || character.targetTree.occupant != character)
        {
            parentLayer.SwitchState("LookForTrees");
        }
        if (counter < miningCD) counter += Time.deltaTime;
        else
        {
            character.TakeEnergy();
            character.targetTree.GetDamage(character, character.pickDamage);
            character.UseEquipment();
            counter = 0.0f;
            if (character.targetTree.treeSpawned == false) character.targetTree = null;
        }
        base.OnUpdate();
    }
    public override void OnStateExit()
    {
        character.Sheathe();
        character.anim.Play("Idle", 1);
        base.OnStateExit();
    }
    public override string Description()
    {
        if (character.targetTree != null) return ":" + character.targetTree.name;
        else return null;
    }
}
