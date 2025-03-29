using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using UnityEngine;

public static class ExtensionMethods
{
    /*public static void AddItem(this Dictionary<ItemData, int> list, List<ItemIntPair> add)
    {
        foreach(ItemIntPair i in add)
        {
            list.AddItem(i);
        }
    }
    public static void AddItem(this Dictionary<ItemData, int> list, ItemIntPair add)
    {
        Debug.Log($"Added {add.item.name}({add.count})");
        if (list.ContainsKey(add.item))
        {
            list[add.item] += add.count;
        }
        else
        {
            list.Add(add.item, add.count);
        }
    }*/
    public static void AddItem(this List<ItemIntPair> list, ItemIntPair add)
    {
        foreach (ItemIntPair i in list)
        {
            if(i.item == add.item)
            {
                i.count += add.count;
                return;
            }
        }
        list.Add(add);
    }
    static Dictionary<GameObject, GameObject> prefabExamples = new Dictionary<GameObject, GameObject>();
    public static GameObject GetPrefabExample(this GameObject obj)
    {
        if (!prefabExamples.ContainsKey(obj)) prefabExamples.Add(obj, MonoBehaviour.Instantiate(obj, new Vector3(9999, -9999, 9999), Quaternion.identity));
        return prefabExamples[obj];
    }
}
[System.Serializable] public class ItemIntPair
{
    public ItemIntPair(ItemData m_item, int m_count)
    {
        item = m_item;
        count = m_count;
    }
    public ItemData item;
    public int count;
}