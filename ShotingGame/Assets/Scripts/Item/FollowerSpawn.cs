using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowerSpawn : Item
{
    protected override void Use()
    {
        player.ShowFollower();
        base.Use();
    }
}
