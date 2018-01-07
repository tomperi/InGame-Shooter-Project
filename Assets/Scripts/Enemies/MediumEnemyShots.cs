using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MediumEnemyShots : BasicEnemy {
    
    public override void OnTriggerEnter2D(Collider2D other)
    {
        if ((other.tag == "RightPlayer") || (other.tag == "LeftPlayer"))
        {
            GameObject wall = GameObject.FindGameObjectWithTag("Wall");
            if (other.tag == "RightPlayer")
                wall.GetComponent<WallControl>().moveWallRight();
            else
                wall.GetComponent<WallControl>().moveWallLeft();
            Die();
        }
    }

}
