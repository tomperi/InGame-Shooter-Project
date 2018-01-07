using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WaveSpawner : MonoBehaviour {

	public enum SpawnState { SPAWNING, WAITING, COUNTING };

    public bool startSpawn, killAll, pause;

	[System.Serializable]
	public class Wave 
	{
		public string name;
		public BasicEnemy[] enemies;
        public int[] enemyCount;
        public WaveParameters waveInfo;
	}

    [System.Serializable]
    public class WaveParameters
    {
        public float rate;
        public float minSpeed = 1f;
        public float maxSpeed = 1f;
        public float HPFactor;
        public float mediumFireRate = 3f;
        public float LargeFireRate = 3f;
        public bool bonus;
    }

    public Wave[] waves;
    public BasicEnemy bonus;
	private int nextWave = 0;

	public float timeBetweenWaves = 5f;
	public float waveCountdown;

	private float searchCountdown = 1f;

	private int enemiesSpawned = 0;
    private int totalEnemiesThisWave = 0;
    private int waveCount = 0;
    private int loopFactor = 1;

	public SpawnState state = SpawnState.COUNTING;

    private bool uiPaused;
    
	void Start()
	{
		waveCountdown = timeBetweenWaves;
        GameObject.Find("HeroLeft").GetComponent<PlayerControl>().dock = true;
        GameObject.Find("HeroRight").GetComponent<PlayerControl>().dock = true;
    }
		
	void Update()
    {
        pausePlayGame();

        if (startSpawn)
        {
            if (state == SpawnState.WAITING)
            {
                if (!EnemyIsAlive())
                {
                    //Begin a new round
                    WaveCompleted();
                }
                else
                {
                    return;
                }
            }

            if (waveCountdown <= 0)
            {
                if (state != SpawnState.SPAWNING)
                {
                    StartCoroutine(SpawnWave(waves[nextWave]));
                }
            }
            else
            {
                waveCountdown -= Time.deltaTime;
            }
        }

        if (killAll)
        {
            killAllEnemies();
        }
	}

	void WaveCompleted()
    {
        waveCount++;
        Debug.Log("Wave completed. Finished wave " + waveCount);
        if (waveCount == 1)
        {
            undock();
        }
        state = SpawnState.COUNTING;
		waveCountdown = timeBetweenWaves;
		if (nextWave + 1 > waves.Length - 1) {
			nextWave = 2;
            loopFactor += 1;
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

        int bonusTimingRight = -1;
        int bonusTimingLeft = -1;

        if (_wave.waveInfo.bonus)
        {
            bonusTimingRight = Random.Range(5, totalEnemiesThisWave - 2);
            bonusTimingLeft = Random.Range(5, totalEnemiesThisWave - 2);
        }

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

            SpawnEnemy(enemyToRightPlayer, Player.right, _wave);
            if (rightEnemy == 0)
                //yield return new WaitForSeconds(0.2f);
                yield return new WaitForSeconds(1f / (_wave.waveInfo.rate * enemiesSpawned + 1));

            SpawnEnemy(enemyToLeftPlayer, Player.left, _wave);
            if (leftEnemy == 0)
                //yield return new WaitForSeconds(0.2f);
                yield return new WaitForSeconds(1f / (_wave.waveInfo.rate * enemiesSpawned + 1));
            enemiesSpawned += 1;

            // Spawning bonuses (only if the player does not have a bonus when bonus is due)
            if ((i == bonusTimingRight) && (GameObject.Find("HeroRight").GetComponent<PlayerControl>().hasBonus == false))
            {
                SpawnEnemy(bonus, Player.right, _wave);
            }

            if ((i == bonusTimingLeft) && (GameObject.Find("HeroLeft").GetComponent<PlayerControl>().hasBonus == false))
            {
                SpawnEnemy(bonus, Player.left, _wave);
            }
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

	public void SpawnEnemy (BasicEnemy _enemy, Player player, Wave _wave)
	{
        if (startSpawn)
        {
            BasicEnemy enemy = Instantiate(_enemy);
            enemy.enemyStats.player = player;
            switch (enemy.enemyStats.type)
            {
                case (enemyType.small):
                case (enemyType.bonus):
                    enemy.transform.position = getEasyEnemyPosition(player);
                    break;
                case (enemyType.medium):
                    enemy.transform.position = getMediumEnemyPosition(player);
                    enemy.GetComponent<MediumEnemy>().spawnRate = (0.5f * loopFactor * _wave.waveInfo.mediumFireRate);
                    enemy.GetComponent<MediumEnemy>().enemyStats.hp *= (int) (loopFactor * _wave.waveInfo.HPFactor);
                    break;
                case (enemyType.large):
                    enemy.transform.position = getHardEnemyPosition(player);
                    enemy.GetComponent<HardEnemy>().spawnRate = (0.5f * loopFactor * _wave.waveInfo.LargeFireRate);
                    enemy.GetComponent<HardEnemy>().enemyStats.hp *= (int)(loopFactor * _wave.waveInfo.HPFactor);
                    break;
                default:
                    enemy.transform.position = getHardEnemyPosition(player);
                    break;
            }
        }

		//Debug.Log("Spawning Enemy: " + _enemy.name);
	}

    public void SpawnEnemy(BasicEnemy _enemy, Player player)
    {
        if (startSpawn)
        {
            BasicEnemy enemy = Instantiate(_enemy);
            enemy.enemyStats.player = player;
            switch (enemy.enemyStats.type)
            {
                case (enemyType.small):
                case (enemyType.bonus):
                    enemy.transform.position = getEasyEnemyPosition(player);
                    break;
                case (enemyType.medium):
                    enemy.transform.position = getMediumEnemyPosition(player);
                    break;
                case (enemyType.large):
                    enemy.transform.position = getHardEnemyPosition(player);
                    break;
                default:
                    enemy.transform.position = getHardEnemyPosition(player);
                    break;
            }
        }

        //Debug.Log("Spawning Enemy: " + _enemy.name);
    }

    Vector2 getEasyEnemyPosition(Player player)
    {
        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));

        max.y = max.y - 0.15f;
        min.y = min.y + 0.65f;
        
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
            return new Vector2(max.x + 1, Random.Range(-1.5f, 1.7f));
        }
        else
        {
            return new Vector2(min.x - 1, Random.Range(-1.5f, 1.7f));
        }
    }

    void killAllEnemies()
    {
        GameObject[] allEnemies;

        allEnemies = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (GameObject enemy in allEnemies)
        {
            enemy.GetComponent<BasicEnemy>().Die();
            Debug.Log("Killed an enemy");
        }
    }

    void pausePlayGame()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (pause)
            {
                SceneManager.LoadScene(0);
            } else
            {
                Debug.Log("Pause the scene");
                pause = true;
            }
        }

        if (pause && Input.GetKeyDown(KeyCode.Space))
        {
            pause = false;
            Debug.Log("Upause the scene");
        }

        if (pause)
        {
            Time.timeScale = 0;
            GameObject.Find("PauseBG").GetComponent<CanvasGroup>().alpha = 0.3f;
            GameObject.Find("PauseText").GetComponent<CanvasGroup>().alpha = 1f;
            uiPaused = true;
        }
        else if (uiPaused)
        {
            uiPaused = false;
            GameObject.Find("PauseBG").GetComponent<CanvasGroup>().alpha = 0f;
            GameObject.Find("PauseText").GetComponent<CanvasGroup>().alpha = 0f;
            Time.timeScale = 1;
        }
    }

    private void undock()
    {
        GameObject.Find("HeroLeft").GetComponent<PlayerControl>().dock = false;
        GameObject.Find("HeroRight").GetComponent<PlayerControl>().dock = false;
        GameObject.Find("GameManager").GetComponent<TutorialObjects>().glideLeft.SetActive(true);
        GameObject.Find("GameManager").GetComponent<TutorialObjects>().glideRight.SetActive(true);
    }
}

