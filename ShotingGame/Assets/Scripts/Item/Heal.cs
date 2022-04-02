using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heal : Item
{
    protected override void Use()
    {
        gameManager.HP += 10;

        base.Use();
    }
}
