using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using UnityEngine;

public class Cooker_Working : NPC_State<Cooker>
{
    public Cooker_Working(Layer parent, Cooker origin) : base(parent, origin)
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
            if((character.workplace as CookStation).DoWork(character, character.workAmount))
            {
                character.TakeEnergy();
            }
        }
        base.OnUpdate();
    }
}
