using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Drop Table", menuName = "Scriptables/Drop Table", order = 0)]
public class DropTable : ScriptableObject
{
    [SerializeField] List<ChanceElement> m_Table;
    public List<ChanceElement> Table { get {  return m_Table; } }

    public List<ItemIntPair> GetDrop(float multiplier)
    {
        List<ItemIntPair> reward = new List<ItemIntPair>();
        for(int i = 0; i < Table.Count; i++)
        {
            float chance = Table[i].chance * multiplier;
            while(chance >= 100.0f)
            {
                chance -= 100.0f;
                reward.AddItem(new ItemIntPair(Table[i].item, 1));
            }
            if (Random.Range(0.0f, 100.0f) <= chance) reward.Add(new ItemIntPair(Table[i].item, 1));
        }
        return reward;
    }
}
[System.Serializable] public class ChanceElement
{
    [Tooltip("n in 100. over 100 will give guranteed loot(etc: 350 = guranteed 3 and chance for 4th)")] public float chance;
    public ItemData item;
}
