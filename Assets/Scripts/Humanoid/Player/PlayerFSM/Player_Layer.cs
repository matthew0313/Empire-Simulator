using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Layer : Layer
{
    protected Player player;
    public Player_Layer(Layer parent, Player pl) : base(parent)
    {
        player = pl;
    }
}
