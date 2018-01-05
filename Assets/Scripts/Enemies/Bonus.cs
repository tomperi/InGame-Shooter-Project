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
                GameObject.Find("HeroRight").GetComponent<PlayerControl>().hasBonus = true;
                GameObject.Find("BonusRightUI").GetComponent<BonusUI>().show = true;
            } else
            {
                GameObject.Find("HeroLeft").GetComponent<PlayerControl>().hasBonus = true;
                GameObject.Find("BonusLeftUI").GetComponent<BonusUI>().show = true;
            }
            Die();
        }
    }
}

