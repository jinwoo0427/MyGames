using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class skyEnemy : EnemyMove
{
    void Start()
    {
        
    }

    protected override void Update()
    {
        MoveLimit();
    }
}
