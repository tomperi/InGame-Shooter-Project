using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missle : BasicEnemy {

    public GameObject p_target;
    
    protected override void Start()
    {
        base.Start();
        if (enemyStats.player.Equals(Player.left)) {
           enemyStats.speed *= -1;
        }
    }

    public override void Move()
    {
            transform.position = Vector2.MoveTowards(new Vector2(
                transform.position.x, transform.position.y),
                            p_target.transform.position,
                            enemyStats.speed * Time.deltaTime);
    }
}
