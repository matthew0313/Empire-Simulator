using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Structure : Accessibles, IInspectable
{
    public string Name;
    [TextArea] public string Description;

    public Transform foundation;
    public List<ItemIntPair> ingredients = new List<ItemIntPair>();
    public int cost;
    public ClaimableIsland island;

    List<(MeshRenderer, Material)> parts = new();
    protected virtual void Awake()
    {
        for(int i = 0; i < transform.childCount; i++)
        {
            SearchChildren(transform.GetChild(i));
        }
    }
    public virtual string Describe()
    {
        return Description;
    }
    public void Highlight(Material material)
    {
        foreach(var i in parts)
        {
            i.Item1.material = material;
        }
    }
    void SearchChildren(Transform part)
    {
        MeshRenderer tmp = part.GetComponent<MeshRenderer>();
        if (tmp != null)
        {
            parts.Add((tmp, tmp.material));
        }
        for (int i = 0; i < part.childCount; i++)
        {
            SearchChildren(part.GetChild(i));
        }
    }
    public void Unhighlight()
    {
        foreach(var i in parts)
        {
            i.Item1.material = i.Item2;
        }
    }
    public void RemoveStructure()
    {
        island.accessibles.Remove(this);
        Destroy(gameObject);
    }
}
