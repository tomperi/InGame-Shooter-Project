using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bonus : BasicEnemy
{
    protected override void Start()
    {
        if (enemyStats.player.Equals(Player.left))
        {
            enemyStats.speed *= -1;
        }
    }

    public override void OnTriggerEnter2D(Collider2D other)
    {
        if ((other.tag == "RightPlayer") || (other.tag == "LeftPlayer"))
        {
            if (other.tag == "RightPlayer")
            {
                GameObject.Find("GameManager").GetComponent<GameController>().GiveBonus(Player.right);
            } else
            {
                GameObject.Find("GameManager").GetComponent<GameController>().GiveBonus(Player.left);
            }
            Die();
        }
    }

}

