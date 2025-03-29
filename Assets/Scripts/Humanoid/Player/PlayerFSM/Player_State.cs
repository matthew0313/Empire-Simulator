using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_State : State
{
    protected Player player;
    public Player_State(Layer parent, Player pl) : base(parent)
    {
        player = pl;
    }
}
