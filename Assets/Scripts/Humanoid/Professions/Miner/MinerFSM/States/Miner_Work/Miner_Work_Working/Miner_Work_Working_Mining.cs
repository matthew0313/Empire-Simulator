using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;

public class Miner_Work_Working_Mining : NPC_State<Miner>
{
    const float miningCD = 1.0f;
    float counter = 0.0f;
    public Miner_Work_Working_Mining(Layer parent, Miner origin) : base(parent, origin)
    {
        
    }
    public override void OnStateEnter()
    {
        counter = 0.0f;
        character.UnSheathe();
        character.anim.Play("Mining", 1);
        base.OnStateEnter();
    }
    public override void OnUpdate()
    {
        if (character.targetNode == null || character.targetNode.occupant != character)
        {
            parentLayer.SwitchState("LookForOres");
        }
        if (counter < miningCD) counter += Time.deltaTime;
        else
        {
            character.TakeEnergy();
            character.targetNode.GetDamage(character, character.pickDamage);
            character.UseEquipment();
            counter = 0.0f;
            if (character.targetNode.oreSpawned == false) character.targetNode = null;
        }
        base.OnUpdate();
    }
    public override void OnStateExit()
    {
        character.Sheathe();
        if (character.targetNode != null) character.targetNode.occupant = null;
        character.anim.Play("Idle", 1);
        base.OnStateExit();
    }
    public override string Description()
    {
        if (character.targetNode != null) return ":" + character.targetNode.name;
        else return null;
    }
}
