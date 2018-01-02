﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum enemyType { small, medium, large };

public abstract class BasicEnemy : MonoBehaviour {


    [System.Serializable]
	public class EnemyStats
	{
		public float maxSpeed;
		public int hp = 1;
		public float speed;
        public Player player;
        public enemyType type; 
	}
    
	public EnemyStats enemyStats = new EnemyStats();
    public GameObject explosion;

    private SpriteRenderer sr;
    private GameObject sprite;

    // Use this for initialization
    protected virtual void Start ()
    {
        sr = GetComponent<SpriteRenderer>();
        if (enemyStats.player.Equals(Player.right))
        {
        }
        else
        {
            enemyStats.speed *= -1;
            sr.flipX = true;
            
        }
        
    }

    // Update is called once per frame
    protected virtual void Update () {
        Move();

        if (enemyStats.hp < 1)
            Die();
    }

    public virtual void Move()
    {
        Vector2 position = transform.position;

        position = new Vector2(position.x - enemyStats.speed * Time.deltaTime, position.y);

        transform.position = position;
    }

    public virtual void Die()
    {
        GameObject exp = Instantiate(explosion);
        exp.transform.position = this.transform.position;
        Destroy(this.gameObject);
    }

}

