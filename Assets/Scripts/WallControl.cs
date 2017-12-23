using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallControl : MonoBehaviour {
	public float move;
	public GameObject rightPlayer, leftPlayer;


	void OnTriggerEnter2D(Collider2D other) {
		Vector2 pos = transform.position;
		Vector2 posPlayerRight = rightPlayer.transform.position;
		Vector2 posPlayerLeft = leftPlayer.transform.position;

		Player player = other.GetComponent<EnemyControl> ().player;
		if (player.Equals(Player.right))
		{
			pos.x -= move;
			posPlayerLeft.x -= move;
			posPlayerRight.x -= move;
			} else {
			pos.x += move;
			posPlayerLeft.x += move;
			posPlayerRight.x += move;
		}
			
		transform.position = pos;
		rightPlayer.transform.position = posPlayerRight;
		leftPlayer.transform.position = posPlayerLeft;
				
		Destroy(other.gameObject);
	}
}
