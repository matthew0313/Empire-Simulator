using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Home : Structure
{
    public float energy;

    public int maxResidents;
    public List<NPC> residents = new List<NPC>();

    public Transform entrance;

    public virtual void OnRest(NPC rester)
    {
        rester.energy += energy;
    }
    public virtual void AddResident(NPC resident)
    {
        residents.Add(resident);
    }
    public virtual void RemoveResident(NPC resident)
    {
        if (residents.Contains(resident)) residents.Remove(resident);
        else Debug.LogError("The chosen resident does not live in the selected home.");
    }
    public override string Describe()
    {
        return $"{base.Describe()}\n\nEnergy given: {energy}\nMax residents: {maxResidents}";
    }
}
