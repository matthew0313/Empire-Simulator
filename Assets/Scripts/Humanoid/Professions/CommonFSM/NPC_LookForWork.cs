using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class NPC_LookForWork : NPC_State
{
    public NPC_LookForWork(Layer parent, NPC origin) : base(parent, origin)
    {

    }
    const float searchCD = 5.0f;
    float counter = 0.0f;
    public override void OnStateEnter()
    {
        base.OnStateEnter();
        if (character.workplace != null)
        {
            parentLayer.SwitchState("HeadToWork");
        }
        else
        {
            Search();
        }
        counter = 0.0f;
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
        List<IWorkplace> jobs = character.availableJobs();
        jobs.Sort((IWorkplace a, IWorkplace b) => { return b.levelRequirement.CompareTo(a.levelRequirement); });
        if (jobs.Count > 0)
        {
            character.workplace = jobs[Random.Range(0, jobs.Count)] as Accessibles;
            (character.workplace as IWorkplace).workers.Add(character);
            parentLayer.SwitchState("HeadToWork");
        }
    }
}
