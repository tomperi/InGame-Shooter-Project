using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class HardEnemy : BasicEnemy {

    public float spawnAfter;
    public float spawnRate;
    public GameObject missle;


    protected override void Start()
    {
        base.Start();

        InvokeRepeating("Shoot", spawnAfter, spawnRate);
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