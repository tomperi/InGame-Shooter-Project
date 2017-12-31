using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missle : MonoBehaviour {

    public GameObject p_target;
    public float speed;
    public int HP;

	// Use this for initialization
	void Start () {
        HP = 1;
	}
	
	// Update is called once per frame
	void Update () {
        Move();
        if(HP < 1) Destroy(this.gameObject);
	}

    void Move()
    {
            transform.position = Vector2.MoveTowards(new Vector2(
                transform.position.x, transform.position.y),
                            p_target.transform.position,
                            speed * Time.deltaTime);
    }
}
