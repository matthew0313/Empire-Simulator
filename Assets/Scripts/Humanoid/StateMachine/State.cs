using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State
{
    public Layer parentLayer;

    public State(Layer parent)
    {
        parentLayer = parent;
    }
    public virtual void OnStateEnter()
    {

    }
    public virtual void OnUpdate()
    {

    }
    public virtual void OnStateExit()
    {

    }
    public virtual string Description()
    {
        return null;
    }
}
