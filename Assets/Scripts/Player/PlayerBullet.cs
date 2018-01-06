using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour {
    public float speed;
    Rigidbody2D rb;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody2D>();
        float direction = transform.eulerAngles.z + 180;
        float xSpeed = Mathf.Sin((direction * -1 * Mathf.PI) / 180);
        float ySpeed = Mathf.Cos((direction * -1 * Mathf.PI) / 180);
        rb.velocity = new Vector2(xSpeed, ySpeed).normalized * speed * -1;
    }
	
	// Update is called once per frame
	void Update ()
    {
        
        Vector2 position = transform.position;
        //position = new Vector2(position.x + speed * Time.deltaTime, position.y);

        //transform.position = position;

        // Check boundries and destroy 
        Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));
        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));

        if ((transform.position.x > max.x) || (transform.position.x < min.x) || (transform.position.y > max.y) || (transform.position.y < min.y))
        {
            Destroy(gameObject);
        }

        
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if ((other.tag != "RightPlayer") && (other.tag != "LeftPlayer") && (other.tag != "Shot") && (other.tag != "Bonus"))
        {
            other.GetComponent<BasicEnemy>().enemyStats.hp--;
            Destroy(this.gameObject);
            //if other.
        }
        
    }
}
