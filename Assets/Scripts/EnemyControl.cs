using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControl : MonoBehaviour {

    public float speed;
    public Player player;
	// Use this for initialization
	void Start ()
    {
        if (player.Equals(Player.right))
        {

        }
        else
        {
            speed *= -1;
            transform.rotation *= Quaternion.Euler(0, 0, 180f);
        }
    }
	
	// Update is called once per frame
	void Update () {
        Vector2 position = transform.position;

        position = new Vector2(position.x - speed * Time.deltaTime, position.y);

        transform.position = position;

        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));

        if ((transform.position.x < min.x) || (transform.position.x > max.x))
        {
            Destroy(gameObject);
        }
	}
}
