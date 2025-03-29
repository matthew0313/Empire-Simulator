using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lumberjack_Work_Working_LookForTrees : NPC_State<Lumberjack>
{
    public Lumberjack_Work_Working_LookForTrees(Layer parent, Lumberjack origin) : base(parent, origin)
    {
        
    }
    public override void OnUpdate()
    {
        if (character.targetTree == null)
        {
            List<TreeSpawn> available = new List<TreeSpawn>();
            foreach (TreeSpawn tree in (character.workplace as Forest).trees)
            {
                if (tree.treeSpawned && tree.occupant == null)
                {
                    available.Add(tree);
                }
            }
            if(available.Count > 0)
            {
                switch (character.priority)
                {
                    case Lumberjack_TreePriority.random:
                        TreeSpawn selected = available[Random.Range(0, available.Count)];
                        character.targetTree = selected;
                        character.targetTree.occupant = character; break;

                }
                parentLayer.SwitchState("MoveToTree");
            }
        }
        else parentLayer.SwitchState("MoveToTree");
        base.OnUpdate();
    }
}
[System.Serializable] public enum Lumberjack_TreePriority
{
    random
}
