using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour {

	public enum SpawnState { SPAWNING, WAITING, COUNTING };

	[System.Serializable]
	public class Wave 
	{
		public string name;
		public BasicEnemy[] enemies;
        public int[] enemyCount;
		public float rate;
		public float maxSpeed;
	}

	public Wave[] waves;
	private int nextWave = 0;

	public float timeBetweenWaves = 5f;
	public float waveCountdown;

	private float searchCountdown = 1f;

	private int enemiesSpawned = 0;
    private int totalEnemiesThisWave = 0;

	private SpawnState state = SpawnState.COUNTING;

	void Start()
	{
		waveCountdown = timeBetweenWaves;
	}
		
	void Update()
	{
		if (state == SpawnState.WAITING) {
			if (!EnemyIsAlive ()) {
				//Begin a new round
				WaveCompleted();
			} else {
				return;
			}
		}

		if (waveCountdown <= 0) {
			if (state != SpawnState.SPAWNING) {
				StartCoroutine (SpawnWave (waves [nextWave]));
			}
		} else {
			waveCountdown -= Time.deltaTime;
		}
	}

	void WaveCompleted()
	{
		Debug.Log("Wave completed");

		state = SpawnState.COUNTING;
		waveCountdown = timeBetweenWaves;

		if (nextWave + 1 > waves.Length - 1) {
			nextWave = 0;
			Debug.Log ("ALL WAVES COMPLETE! Looping..");
		} else {
			nextWave++;
		}
	}

	bool EnemyIsAlive()
	{
		searchCountdown -= Time.deltaTime;
		if (searchCountdown <= 0f)
		{
			searchCountdown = 1f;
			if (GameObject.FindGameObjectWithTag ("Enemy") == null) {
				return false;
			}
		}
		return true;
	}

	IEnumerator SpawnWave(Wave _wave)
    {
        int[] rightEnemies = new int[_wave.enemyCount.Length];
        int[] leftEnemies = new int[_wave.enemyCount.Length];
        System.Array.Copy(_wave.enemyCount, rightEnemies, _wave.enemyCount.Length);
        System.Array.Copy(_wave.enemyCount, leftEnemies, _wave.enemyCount.Length);

        totalEnemiesThisWave = countWaveEnemies(_wave);
		enemiesSpawned = 0;
		state = SpawnState.SPAWNING;

        // Spawn a random enemy for both player, within the limit
		for (int i = 0; i < totalEnemiesThisWave; i++) {
            int leftEnemy = Random.Range(0, _wave.enemies.Length);
            int rightEnemy = Random.Range(0, _wave.enemies.Length);

            while (rightEnemies[rightEnemy] == 0)
                rightEnemy = Random.Range(0, _wave.enemies.Length);
            while (leftEnemies[leftEnemy] == 0)
                leftEnemy = Random.Range(0, _wave.enemies.Length);

            rightEnemies[rightEnemy]--;
            leftEnemies[leftEnemy]--;

            BasicEnemy enemyToRightPlayer = _wave.enemies[rightEnemy];
            BasicEnemy enemyToLeftPlayer = _wave.enemies[leftEnemy];

            SpawnEnemy(enemyToRightPlayer, Player.right);
            yield return new WaitForSeconds(1f / 2 * (_wave.rate * enemiesSpawned));
            SpawnEnemy(enemyToLeftPlayer, Player.left);
            enemiesSpawned += 1;
			yield return new WaitForSeconds (1f / 2* (_wave.rate * enemiesSpawned));
		}

		state = SpawnState.WAITING;

		yield break;
	}

    int countWaveEnemies(Wave _wave)
    {
        int sum = 0;
        for (int i = 0; i < _wave.enemyCount.Length; i++)
        {
            sum += _wave.enemyCount[i];
        }
        return sum;
    }

	void SpawnEnemy (BasicEnemy _enemy, Player player)
	{
		BasicEnemy enemy = Instantiate (_enemy);
        enemy.enemyStats.player = player; 
        switch (enemy.enemyStats.type)
        {
            case (enemyType.small):
                enemy.transform.position = getEasyEnemyPosition(player);
                break;
            case (enemyType.medium):
                enemy.transform.position = getMediumEnemyPosition(player);
                break;
            default:
                enemy.transform.position = getHardEnemyPosition(player);
                break;
        }

		//Debug.Log("Spawning Enemy: " + _enemy.name);
	}

    Vector2 getEasyEnemyPosition(Player player)
    {
        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));

        max.y = max.y - 0.285f;
        min.y = min.y + 0.285f;
        
        if (player.Equals(Player.right))
        {
            return new Vector2(max.x, Random.Range(min.y, max.y));
        }
        else
        {
            return new Vector2(min.x, Random.Range(min.y, max.y));
        }
    }

    Vector2 getMediumEnemyPosition(Player player)
    {
        Vector2 min, max;

        max.y = 2.5f;
        min.y = -2.5f;

        max.x = 5.2f;
        min.x = 3.5f;

        if (player.Equals(Player.right))
        {
            return new Vector2(Random.Range(min.x,max.x), Random.Range(min.y, max.y));
        }
        else
        {
            return new Vector2(Random.Range(min.x, max.x) * -1, Random.Range(min.y, max.y));
        }
    }

    Vector2 getHardEnemyPosition(Player player)
    {
        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));

        if (player.Equals(Player.right))
        {
            return new Vector2(max.x + 1, 0);
        }
        else
        {
            return new Vector2(min.x - 1, 0);
        }
    }
}
