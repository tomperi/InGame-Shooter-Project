using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class HardEnemy : BasicEnemy {

    public float distanceToStopFromWall;
    public float spawnAfter;
    public float spawnRate;
    public GameObject ShotSpawn;
    public GameObject missle;
    GameObject wall;

    protected override void Start()
    {
        base.Start();
        wall = GameObject.FindGameObjectWithTag("Wall");
        if (this.enemyStats.player == Player.left)
        {
            ChangeShotSpawnerPos();
        }
        InvokeRepeating("Shoot", spawnAfter, spawnRate);
    }

    public override void Move()
    {
        float distance = Mathf.Abs(this.transform.position.x - wall.transform.position.x);
        if (distanceToStopFromWall <= distance) { 
            Vector2 position = transform.position;

            position = new Vector2(position.x - enemyStats.speed * Time.deltaTime, position.y);

            transform.position = position;
        }
    }
    void ChangeShotSpawnerPos()
    {
        Transform parentTransform = ShotSpawn.GetComponentInParent<Transform>();
        float x = Mathf.Abs(this.transform.position.x - parentTransform.position.x);
        ShotSpawn.transform.position = new Vector3(ShotSpawn.transform.position.x + 2*x, ShotSpawn.transform.position.y, 0);
    }

    void Shoot()
    {
        string TAG;
        if (enemyStats.player == Player.right)
            TAG = "RightPlayer";
        else
            TAG = "LeftPlayer";

        GameObject m = Instantiate(missle, ShotSpawn.transform.position, Quaternion.identity);
        m.GetComponent<Missle>().p_target = GameObject.FindGameObjectWithTag(TAG);
        m.GetComponent<Missle>().enemyStats.player = this.enemyStats.player;
    }
}