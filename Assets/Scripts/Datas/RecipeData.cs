using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Recipe Data", menuName = "Scriptables/Recipe Data", order = 0)]
public class RecipeData : ScriptableObject
{
    [SerializeField] ItemIntPair m_result;
    [SerializeField] List<ItemIntPair> m_ingredients;
    [SerializeField] float m_requiredWorkAmount;
    [SerializeField] int m_experienceReward;

    public ItemIntPair result { get { return m_result; } }
    public List<ItemIntPair> ingredients { get {  return m_ingredients; } }
    public float requiredWorkAmount { get {  return m_requiredWorkAmount; } }
    public int experienceReward { get { return m_experienceReward; } }

    public bool Check()
    {
        bool possible = true;
        foreach(ItemIntPair i in m_ingredients)
        {
            if (i.count == 0)
            {
                Debug.LogError("A recipe costs 0 ingredient of an item!");
                return false;
            }
            if (EmpireManager.Instance.Get(i.item) < i.count)
            {
                possible = false;
                break;
            }
        }
        return possible;
    }
    public void TakeIngredients()
    {
        foreach (var i in ingredients)
        {
            EmpireManager.Instance.RemoveFromStorage(i);
        }
    }
    public void Craft()
    {
        EmpireManager.Instance.AddItemToStorage(result);
    }
}
