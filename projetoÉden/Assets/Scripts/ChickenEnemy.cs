using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChickenEnemy : EnemyController
{
    private Renderer mainRenderer;
    // Start is called before the first frame update
    void Start()
    {
        mainRenderer = GetComponent<Renderer>();

        damage = 10; 
        hit = 50;
    }
}
