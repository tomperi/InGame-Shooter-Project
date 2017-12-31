using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum enemyType { small, medium, large };

public abstract class BasicEnemy : MonoBehaviour {

	private GameObject sprite;

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
    private SpriteRenderer sr;

    // Use this for initialization
    protected virtual void Start ()
    {
        sr = GetComponent<SpriteRenderer>();
        if (enemyStats.player.Equals(Player.right))
        {
            transform.eulerAngles = new Vector3(0, 0, -90);
        }
        else
        {
            enemyStats.speed *= -1;
            transform.eulerAngles = new Vector3(0, 0, 90);
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
        Destroy(this.gameObject);
    }

}

