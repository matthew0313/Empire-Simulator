using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Events;

public class Layer : State
{
    public Layer(Layer parent) : base(parent)  
    {

    }
    public State currentState;
    protected State defaultState = null;

    public Dictionary<string, State> states = new Dictionary<string, State>();
    public Dictionary<State, string> names = new Dictionary<State, string>();

    //alerter for top layer
    public UnityEvent onStateChange = new UnityEvent();
    protected void AddState(string name, State state)
    {
        states.Add(name, state);
        names.Add(state, name);
    }
    public override void OnStateEnter()
    {
        base.OnStateEnter();
        currentState = defaultState;
        defaultState.OnStateEnter();
    }
    public override void OnUpdate()
    {
        base.OnUpdate();
        currentState.OnUpdate();
    }
    public override void OnStateExit()
    {
        base.OnStateExit();
        currentState.OnStateExit();
    }
    public void SwitchState(string switchTo)
    {
        currentState.OnStateExit();
        currentState = states[switchTo];
        if (currentState == null) Debug.LogError("State not found");
        else
        {
            currentState.OnStateEnter();
            AlertStateChange();
        }
    }
    public void AlertStateChange()
    {
        if(parentLayer != null) parentLayer.AlertStateChange();
        else onStateChange.Invoke();
    }
    public bool LookForState(string lookFor)
    {
        return states.ContainsKey(lookFor);
    }
    public string CurrentState()
    {
        return names[currentState];
    }
    public string GetBottomState()
    {
        if (typeof(Layer).IsAssignableFrom(currentState.GetType()))
        {
            return CurrentState() + currentState.Description() + "->" + ((Layer)currentState).GetBottomState();
        }
        else return CurrentState() + currentState.Description();
    }
}