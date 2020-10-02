using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slug : EnemyController
{
    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        damage = 100;
        hit  = 0;
    }
}
