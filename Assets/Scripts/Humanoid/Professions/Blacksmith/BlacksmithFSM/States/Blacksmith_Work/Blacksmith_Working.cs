using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using UnityEngine;

public class Blacksmith_Working : NPC_State<Blacksmith>
{
    public Blacksmith_Working(Layer parent, Blacksmith origin) : base(parent, origin)
    {

    }
    const float workCD = 1.0f;
    float counter = 0.0f;
    public override void OnStateEnter()
    {
        counter = 0.0f;
        character.UnSheathe();
        base.OnStateEnter();
    }
    public override void OnUpdate()
    {
        if (counter < workCD) counter += Time.deltaTime;
        else
        {
            counter = 0.0f;
            if((character.workplace as Smithery).DoWork(character, character.workAmount))
            {
                character.TakeEnergy();
            }
        }
        base.OnUpdate();
    }
    public override void OnStateExit()
    {
        character.Sheathe();
        base.OnStateExit();
    }
}
