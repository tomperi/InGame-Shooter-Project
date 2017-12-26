using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum enemyType { small, medium, large };

public class BasicEnemy : MonoBehaviour {

	public GameObject sprite;

    [System.Serializable]
	public class EnemyStats
	{
		public float maxSpeed;
		public int hp = 1;
		public float speedMod;
		public float speed;
        public Player player;
        public enemyType type; 
	}
	public EnemyStats enemyStats = new EnemyStats();

	// Use this for initialization
	void Start ()
    {
		enemyStats.speed = (Random.Range(1, enemyStats.maxSpeed + 1) * enemyStats.speedMod);
        if (enemyStats.player.Equals(Player.right))
        {
            transform.eulerAngles = new Vector3(0, 0, -90);
        }
        else
        {
            enemyStats.speed *= -1;
            transform.eulerAngles = new Vector3(0, 0, 90);
        }
        
    }
	
	// Update is called once per frame
	void Update () {
        Vector2 position = transform.position;

        position = new Vector2(position.x - enemyStats.speed * Time.deltaTime, position.y);

        transform.position = position;
    }

}

