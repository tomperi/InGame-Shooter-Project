using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    public GameObject Enemy;
    public float maxSpawnRate;
    public bool startSpawn;

    private float lastEnemyTimeRight, lastEnemyTimeLeft;
    // Use this for initialization
    void Start () {
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (startSpawn)
        {
            if (Time.time >= lastEnemyTimeRight)
            {
                SpawnEnemy(Player.right);
                lastEnemyTimeRight = Time.time + Random.Range(0.5f, maxSpawnRate);
            }
            if (Time.time >= lastEnemyTimeLeft)
            {
                SpawnEnemy(Player.left);
                lastEnemyTimeLeft = Time.time + Random.Range(0.5f, maxSpawnRate);
            }
        }
	}

    void SpawnEnemy(Player player)
    {
        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));

        max.y = max.y - 0.285f;
        min.y = min.y + 0.285f;

        GameObject enemyUnit = (GameObject)Instantiate(Enemy);
        if (player.Equals(Player.right))
        {
            enemyUnit.transform.position = new Vector2(max.x, Random.Range(min.y, max.y));
            enemyUnit.GetComponent<EnemyControl>().player = Player.right;
        } else
        {
            enemyUnit.transform.position = new Vector2(min.x, Random.Range(min.y, max.y));
            enemyUnit.GetComponent<EnemyControl>().player = Player.left;
        }

    }
}
