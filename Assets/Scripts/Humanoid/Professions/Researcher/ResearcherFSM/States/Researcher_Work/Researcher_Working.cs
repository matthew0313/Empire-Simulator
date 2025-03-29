using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using UnityEngine;

public class Researcher_Working : NPC_State<Researcher>
{
    public Researcher_Working(Layer parent, Researcher origin) : base(parent, origin)
    {

    }
    const float workCD = 1.0f;
    float counter = 0.0f;
    public override void OnStateEnter()
    {
        counter = 0.0f;
        base.OnStateEnter();
    }
    public override void OnUpdate()
    {
        if (counter < workCD) counter += Time.deltaTime;
        else
        {
            counter = 0.0f;
            if((character.workplace as ResearchStation).DoWork(character, character.workAmount))
            {
                character.TakeEnergy();
            }
        }
        base.OnUpdate();
    }
}
