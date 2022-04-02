using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class God : Item 
{
    protected override void Use()
    {
        if (player.godCoroutine != null)
        {
            player.StopCoroutine(player.godCoroutine);
        }
        player.godCoroutine = player.StartCoroutine(player.God(1.5f, 3f));

        base.Use();
    }
}
