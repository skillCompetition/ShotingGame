using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boom : Item
{
    
    protected override void Use()
    {
        CheatManager.Instance.AllEnemyDead(false);
        base.Use();
    }


}
