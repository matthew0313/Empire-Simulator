using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using System.Runtime.InteropServices;
using UnityEngine;

public class CraftingStation : Structure, IWorkplace
{
    #region interface variables
    public Transform workplace_Marker
    {
        get { return m_workplace_Marker; }
        set { }
    }
    public List<NPC> workers
    {
        get { return m_workers; }
        set { m_workers = value; }
    }
    public int levelRequirement
    {
        get { return m_levelRequirement; }
    }
    public int maxWorkers
    {
        get { return m_maxWorkers; }
    }
    [SerializeField] Transform m_workplace_Marker;
    [SerializeField] List<NPC> m_workers;
    [SerializeField] int m_levelRequirement;
    [SerializeField] int m_maxWorkers;
    #endregion
    [SerializeField] List<Recipe_And_Values> recipes;
    Recipe_And_Values chosenRecipe;
    [SerializeField] float workedAmount;
    void ChooseRecipe()
    {
        recipes.Sort((Recipe_And_Values a, Recipe_And_Values b) => { return a.priority.CompareTo(b.priority); });
        foreach (var i in recipes)
        {
            if (i.targetAmount > EmpireManager.Instance.Get(i.data.result.item) && i.data.Check())
            {
                i.data.TakeIngredients();
                chosenRecipe = i; return;
            }
        }
        chosenRecipe = null;
    }

    public bool DoWork(Humanoid worker, float workAmount)
    {
        if (chosenRecipe == null)
        {
            ChooseRecipe();
            if (chosenRecipe == null) return false;
        }
        workedAmount += workAmount;
        if(workedAmount >= chosenRecipe.data.requiredWorkAmount)
        {
            workedAmount = 0.0f;
            foreach(NPC i in workers)
            {
                i.GetExperience(chosenRecipe.data.experienceReward);
            }
            chosenRecipe.data.Craft();
            ChooseRecipe();
        }
        return true;
    }
    public override string Describe()
    {
        string content = $"{base.Describe()}\n\nRecipes:\n";
        foreach(var i in recipes)
        {
            content += $"{i.data.result.item.name}x{i.data.result.count}(";
            for(int k = 0; k < i.data.ingredients.Count; k++)
            {
                content += $"{i.data.ingredients[k].item.Name}x{i.data.ingredients[k].count}";
                if (k < i.data.ingredients.Count - 1) content += ", ";
            }
            content += ")\n";
        }
        return content;
    }
}
[System.Serializable] public class Recipe_And_Values
{
    public RecipeData data;
    public int priority;
    public int targetAmount;
}
