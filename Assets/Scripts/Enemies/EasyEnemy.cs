using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EasyEnemy : BasicEnemy {
    protected override void Start()
    {
        enemyStats.speed = (Random.Range(1, enemyStats.maxSpeed + 1));

        base.Start();
    }
}
