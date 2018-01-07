using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallControl : MonoBehaviour {
	public float move;
	public GameObject rightPlayer, leftPlayer;


    Vector2 pos, posPlayerRight, posPlayerLeft;

    void OnTriggerEnter2D(Collider2D other) {
        
        if (other.name != "MediumEnemyFire(Clone)")
        {
            Player player = other.GetComponent<BasicEnemy>().enemyStats.player;
            if (player.Equals(Player.right))
            {
                moveWallRight();
            }
            else
            {
                moveWallLeft();
            }
        }
        Debug.Log(other);
        other.GetComponent<BasicEnemy>().Die();
		//Destroy(other.gameObject);
	}

    public void moveWallRight()
    {
        pos = transform.position;
        posPlayerRight = rightPlayer.transform.position;
        posPlayerLeft = leftPlayer.transform.position;

        pos.x += move;
        posPlayerLeft.x += move;
        posPlayerRight.x += move;

        transform.position = pos;
        rightPlayer.transform.position = posPlayerRight;
        leftPlayer.transform.position = posPlayerLeft;
    }

    public void moveWallLeft()
    {
        pos = transform.position;
        posPlayerRight = rightPlayer.transform.position;
        posPlayerLeft = leftPlayer.transform.position;

        pos.x -= move;
        posPlayerLeft.x -= move;
        posPlayerRight.x -= move;

        transform.position = pos;
        rightPlayer.transform.position = posPlayerRight;
        leftPlayer.transform.position = posPlayerLeft;
    }
}
