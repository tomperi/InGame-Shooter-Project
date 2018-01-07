using System.Collections;
using UnityEngine;

public class MediumEnemy : BasicEnemy {

    public float spawnAfter;
    public float spawnRate;
    public int maxShots; //Set -1 for infinity
    public GameObject easyEnemy;
    public float missleSpeed;

    private bool down;
    private int direction;

    protected override void Start()
    {
        base.Start();
        down = true;
        direction = -1;
        InvokeRepeating("SpawnEnemy", spawnAfter, spawnRate);

        if (enemyStats.player.Equals(Player.left))
        {
            enemyStats.speed *= -1;
        }
    }

    protected override void Update()
    {
        base.Update();

        if (maxShots == 0)
        {
            Die();
        }

    }

    public override void Move()
    {
        if (down)
        {

            if (this.transform.position.y <= -2.5f)
            {
                down = false;
                direction = 1;
            }

        }
        else
        {
            if (transform.position.y >= 2.5f)
            {
                down = true;
                direction = -1;
            }
        }

        Vector2 position = transform.position;

        position = new Vector2(position.x, (enemyStats.speed * direction * Time.deltaTime) + transform.position.y);

        this.transform.position = position;

    }

    public void SpawnEnemy()
    {
        if (maxShots != 0)
        {
            GameObject EasyEnemy = Instantiate(
            easyEnemy, this.transform.position, Quaternion.identity);
            EasyEnemy.GetComponent<BasicEnemy>().enemyStats.player = this.enemyStats.player;
            EasyEnemy.GetComponent<BasicEnemy>().enemyStats.speed = missleSpeed;

            maxShots--;
        }
        
    }

}
