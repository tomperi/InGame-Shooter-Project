using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class HardEnemy : MonoBehaviour {

    public float speed;
    public GameObject missle;
    public Player player;
    public int HP;


    // Use this for initialization
    void Start () {
        InvokeRepeating("Shoot", 1f, 6f);
        HP = 3;

    }

    // Update is called once per frame
    void Update () {
        Move();
        if (HP < 1) Destroy(this.gameObject);
    }

    void Move()
    {
        Vector2 position = transform.position;

        position = new Vector2(position.x - speed * Time.deltaTime, position.y);

        transform.position = position;
    }

    void Shoot()
    {
        string TAG;
        if (player == Player.right) TAG = "RightPlayer";
        else TAG = "LeftPlayer";

        GameObject m = Instantiate(missle, this.transform.position, Quaternion.identity);
        m.GetComponent<Missle>().p_target = GameObject.FindGameObjectWithTag(TAG);
        m.GetComponent<Missle>().speed = 3 * this.speed;
    }
}