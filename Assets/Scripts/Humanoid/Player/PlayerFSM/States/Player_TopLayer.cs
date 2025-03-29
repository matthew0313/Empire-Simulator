using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_TopLayer : Player_Layer
{
    public Player_TopLayer(Player player) : base(null, player)
    {
        defaultState = new Player_Normal(this, player);
        AddState("Normal", defaultState);
        AddState("Inspecting", new Player_Inspecting(this, player));
    }
}
