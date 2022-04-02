using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : Item
{
    protected override void Use()
    {
        if (player.BulletLevel >= 4)
            return;
        player.BulletLevel++;
        base.Use();
    }
}
