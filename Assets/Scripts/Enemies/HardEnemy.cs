using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class HardEnemy : BasicEnemy {

    public float distanceToStopFromWall;
    public float spawnAfter;
    public float spawnRate;
    public GameObject missle;
    GameObject wall;

    protected override void Start()
    {
        base.Start();
        wall = GameObject.FindGameObjectWithTag("Wall");
        InvokeRepeating("Shoot", spawnAfter, spawnRate);
    }

    public override void Move()
    {
        float distance = Mathf.Abs(this.transform.position.x - wall.transform.position.x);
        if (distanceToStopFromWall < distance) { 
            Vector2 position = transform.position;

            position = new Vector2(position.x - enemyStats.speed * Time.deltaTime, position.y);

            transform.position = position;
        }
    }

    void Shoot()
    {
        string TAG;
        if (enemyStats.player == Player.right)
            TAG = "RightPlayer";
        else
            TAG = "LeftPlayer";

        GameObject m = Instantiate(missle, this.transform.position, Quaternion.identity);
        m.GetComponent<Missle>().p_target = GameObject.FindGameObjectWithTag(TAG);
        m.GetComponent<Missle>().enemyStats.player = this.enemyStats.player;
    }
}