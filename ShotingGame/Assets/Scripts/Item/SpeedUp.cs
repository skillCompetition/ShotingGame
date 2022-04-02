using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedUp: Item
{
    protected override void Use()
    {
        if(player.speedUpCoroutine != null)
        {
            player.speed = (int)player.temp;
            player.StopCoroutine(player.speedUpCoroutine);
        }

        player.speedUpCoroutine = player.StartCoroutine(player.SpeedUP());
            

        base.Use();
    }
}
