using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    public GameObject Enemy;
    public GameObject MediumEnemy;
    public float maxSpawnRate;
    public bool startSpawn;

    private float lastEnemyTimeRight, lastEnemyTimeLeft, lastMediumEnemyTimeRight, lastMediumEnemyTimeLeft;
    // Use this for initialization
    void Start () {
        this.lastMediumEnemyTimeLeft = 0;
        this.lastMediumEnemyTimeRight = 0;
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
            if (Time.time >= lastMediumEnemyTimeRight)
            {
                SpawnMediumEnemy(Player.right);
                this.lastMediumEnemyTimeRight = Time.time + 10;
            }
            if (Time.time >= lastMediumEnemyTimeLeft)
            {
                SpawnMediumEnemy(Player.left);
                this.lastMediumEnemyTimeLeft = Time.time + 10;
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
        }
        else
        {
            enemyUnit.transform.position = new Vector2(min.x, Random.Range(min.y, max.y));
            enemyUnit.GetComponent<EnemyControl>().player = Player.left;
        }

    }

    void SpawnMediumEnemy(Player player)
    {

        GameObject enemyMediumUnit = Instantiate(MediumEnemy);
        if (player.Equals(Player.right))
        {
            enemyMediumUnit.transform.position = new Vector2(5, Random.Range(-2.5f, 2.5f));
            enemyMediumUnit.GetComponent<MediumEnemyControl>().player = Player.right;
        }
        else
        {
            enemyMediumUnit.transform.position = new Vector2(-5, Random.Range(-2.5f, 2.5f));
            enemyMediumUnit.GetComponent<MediumEnemyControl>().player = Player.left;
        }

    }
}
