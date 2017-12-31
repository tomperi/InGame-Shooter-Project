using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallControl : MonoBehaviour {
	public float move;
	public GameObject rightPlayer, leftPlayer;


    Vector2 pos, posPlayerRight, posPlayerLeft;

    void OnTriggerEnter2D(Collider2D other) {

        pos = transform.position;
        posPlayerRight = rightPlayer.transform.position;
        posPlayerLeft = leftPlayer.transform.position;

        //Player player = other.GetComponent<EnemyControl> ().player;
        Player player = other.GetComponent<BasicEnemy>().enemyStats.player;
		if (player.Equals(Player.right))
        {
            moveWallRight();
        } else
        {
            moveWallLeft();
        }
				
		Destroy(other.gameObject);
	}

    void moveWallRight()
    {
        pos.x += move;
        posPlayerLeft.x += move;
        posPlayerRight.x += move;

        transform.position = pos;
        rightPlayer.transform.position = posPlayerRight;
        leftPlayer.transform.position = posPlayerLeft;
        Debug.Log("Right player position is " + rightPlayer.transform.position);
    }

    void moveWallLeft()
    {
        pos.x -= move;
        posPlayerLeft.x -= move;
        posPlayerRight.x -= move;

        transform.position = pos;
        rightPlayer.transform.position = posPlayerRight;
        leftPlayer.transform.position = posPlayerLeft;
        Debug.Log("Moving right");
    }
}
