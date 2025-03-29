using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable][CreateAssetMenu(fileName = "Item Data", menuName = "Scriptables/Item Datas/Item Data", order = 0)]
public class ItemData : ScriptableObject
{
    [TextArea][SerializeField] string m_Description;
    [SerializeField] Sprite m_image;

    public string Name { get { return name; } }
    public string Description { get { return m_Description; } }
    public Sprite image { get { return m_image; } }
    public virtual string Describe()
    {
        return m_Description;
    }
}

