using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClaimableIsland : Island
{
    [SerializeField] Transform m_defaultFreecamPos;
    public Transform defaultFreecamPos { get { return m_defaultFreecamPos; } }
    public List<Accessibles> accessibles = new List<Accessibles>();

    [SerializeField] int m_unlockPrice = 0;
    public int unlockPrice { get { return m_unlockPrice; } }
    public List<T> GetAccessibles<T>() where T : Accessibles
    {
        List<T> list = new List<T>();
        for(int i = 0; i < accessibles.Count; i++)
        {
            if (accessibles[i] == null) accessibles.RemoveAt(i);
            else if (typeof(T).IsAssignableFrom(accessibles[i].GetType()))
            {
                list.Add(accessibles[i] as T);
            }
        }
        return list;
    }
    public override string Describe()
    {
        return $"Claimable\n\n{base.Describe()}";
    }
}
