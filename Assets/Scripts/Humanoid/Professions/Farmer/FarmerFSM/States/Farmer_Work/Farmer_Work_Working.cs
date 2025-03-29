using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Farmer_Work_Working : NPC_State<Farmer>
{
    const float workCD = 1.0f;
    float counter = 0.0f;
    public Farmer_Work_Working(Layer parent, Farmer origin) : base(parent, origin)
    {

    }
    public override void OnStateEnter()
    {
        counter = 0.0f;
        character.UnSheathe();
        character.anim.Play("Farming", 1);
        base.OnStateEnter();
    }
    public override void OnUpdate()
    {
        if (counter < workCD) counter += Time.deltaTime;
        else
        {
            character.TakeEnergy();
            (character.workplace as IProgressiveWork).DoWork(character, character.workAmount);
            counter = 0.0f;
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
        if (character.workplace != null) return ":" + character.workplace.name;
        else return null;
    }
}
