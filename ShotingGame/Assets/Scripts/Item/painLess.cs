using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class painLess : Item
{
    protected override void Use()
    {
        gameManager.Pain -= 10;

        base.Use();
    }
}
