using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Define data types
public enum Player { right, left };
public enum BonusTypes { spawnLargeEnemy, moveWall, destroyEnemies };

public class PlayerControl : MonoBehaviour
{
    public GameObject PlayerBullet;
    public GameObject ShotSpawn1;
    public BasicEnemy largeEnemy;
    //public GameObject ShotSpawn2;
    public Player player;
    public float speed, rotateSpeed;
    public float maxAngle;
    public bool dock;
    public bool hasBonus, firstBonus;
    public BonusTypes bonusType;
    public float fireRate;


    private string verticalAxis, horizontalAxis, fire, bonusKey;
    private Vector2 min, max;
    private Vector3 shotDirection;
    private float maxAngleUp, maxAngleDown, defaultAngle;
    private float nextFire, nextBlink;


    public float StartBlink;
    private bool isBlink = false;
    public float Die;
    public Renderer rend;

    // Use this for initialization
    void Start () {
		if (player.Equals(Player.right))
        {
            verticalAxis = "RightVertical";
            horizontalAxis = "RightHorizontal";
            fire = "right ctrl";
            defaultAngle = 0;
            maxAngleUp = defaultAngle - maxAngle;
            maxAngleDown = defaultAngle + maxAngle;
            shotDirection = new Vector3(0, 0, -90);
            bonusKey = "right shift";
        } else
        {
            verticalAxis = "LeftVertical";
            horizontalAxis = "LeftHorizontal";
            fire = "caps lock";
            defaultAngle = 0;
            maxAngleUp = defaultAngle - maxAngle;
            maxAngleDown = defaultAngle + maxAngle;
            shotDirection = new Vector3(0, 0, 90);
            bonusKey = "tab";
        }

        rend = GetComponent<Renderer>();
        rend.enabled = true;
    }
	
	// Update is called once per frame
	void Update () {
        // Move along the y axis
        if (GameObject.Find("GameManager").GetComponent<GameController>().GameOver == false)
        {
            if (!dock)
            {
                float y = Input.GetAxisRaw(verticalAxis);
                Vector2 yAxisMovement = new Vector2(0, y * speed);
                Move(yAxisMovement);
            }

            // Rotate
            float rotate = Input.GetAxisRaw(horizontalAxis);
            Rotate(rotate);

            if (Input.GetKeyDown(fire) && Time.time > nextFire)
            {
                nextFire = Time.time + fireRate;
                Shot();
            }
            if (Input.GetKeyDown(bonusKey))
            {
                UseBonus();
            }

            CheckHP();
        }
            
    }

    void Move(Vector2 direction)
    {
        // Limit the player movement, including the sprite size 
        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector3(0, 0));
        Vector2 max = Camera.main.ViewportToWorldPoint(new Vector3(1, 1));

        max.x = max.x - 0.225f;
        min.x = min.x + 0.225f;

        max.y = max.y - 0.4f;
        min.y = min.y + 0.8f;

        // Get the player current position
        Vector2 pos = transform.position;

        pos += direction * speed * Time.deltaTime;
        pos.x = Mathf.Clamp(pos.x, min.x, max.x);
        pos.y = Mathf.Clamp(pos.y, min.y, max.y);
        
        transform.position = pos;
    }

    void Rotate(float rotate)
    {
        Transform staffRotation; 
        defaultAngle += rotate * rotateSpeed *-1;
        defaultAngle = Mathf.Clamp(defaultAngle, maxAngleUp, maxAngleDown);
        staffRotation = transform.GetChild(0);
        staffRotation.localEulerAngles = new Vector3(0, 0, defaultAngle);
    }

    void Shot()
    {
        GameObject bullet1 = (GameObject)Instantiate(PlayerBullet);
        bullet1.transform.position = ShotSpawn1.transform.position;
        bullet1.transform.rotation = ShotSpawn1.transform.rotation;
        bullet1.transform.eulerAngles += shotDirection;
        
    }
    // Clearly the enemies on the side of the using player.
    void UseBonus()
    {
        if (hasBonus)
        {
            switch (bonusType)
            {
                case BonusTypes.destroyEnemies:
                    {
                        Debug.Log(player + " used destroy enemy bonus");
                        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
                        foreach (GameObject enemy in enemies)
                        {
                            if (enemy.GetComponent<BasicEnemy>().enemyStats.player == this.player)
                            {
                                enemy.GetComponent<BasicEnemy>().Die();
                            }
                        }
                        this.hasBonus = false;
                        break;
                    }
                case BonusTypes.moveWall:
                    {
                        Debug.Log(player + " used move wall bonus");
                        GameObject wall = GameObject.FindGameObjectWithTag("Wall");
                        if (player == Player.left)
                        {
                            wall.GetComponent<WallControl>().moveWallRight();
                            wall.GetComponent<WallControl>().moveWallRight();
                            wall.GetComponent<WallControl>().moveWallRight();
                            wall.GetComponent<WallControl>().moveWallRight();
                            wall.GetComponent<WallControl>().moveWallRight();
                        }
                        else
                        {
                            wall.GetComponent<WallControl>().moveWallLeft();
                            wall.GetComponent<WallControl>().moveWallLeft();
                            wall.GetComponent<WallControl>().moveWallLeft();
                            wall.GetComponent<WallControl>().moveWallLeft();
                            wall.GetComponent<WallControl>().moveWallLeft();
                        }
                        this.hasBonus = false;
                        break;
                    }
                case BonusTypes.spawnLargeEnemy:
                    {
                        Debug.Log(player + " used spawn large enemy bonus");
                        if (player == Player.left)
                        {
                            GameObject.Find("SpawnManager").GetComponent<WaveSpawner>().SpawnEnemy(largeEnemy, Player.right);
                        } else
                        {
                            GameObject.Find("SpawnManager").GetComponent<WaveSpawner>().SpawnEnemy(largeEnemy, Player.left);
                        }
                        this.hasBonus = false;
                        break;
                    }
                default:
                    {
                        Debug.Log("Bonus function called. Bad type");
                        break;
                    }



            }

            if (player == Player.left)
            {
                Debug.Log(player);
                GameObject.Find("GameManager").GetComponent<GameController>().PlayerUsedBonus(Player.left);
            }
            else
            {
                Debug.Log(player);
                GameObject.Find("GameManager").GetComponent<GameController>().PlayerUsedBonus(Player.right);
            }
        }
       
    }

    void CheckHP()
    {
        if (player == Player.left)
        {
            if (transform.localPosition.x < 1)
            {

                nextBlink += Time.deltaTime;

                if (nextBlink >= 0.4)
                {
                    GetComponent<SpriteRenderer>().enabled = true;
                }

                if (nextBlink >= 1)
                {
                    GetComponent<SpriteRenderer>().enabled = false;
                    nextBlink = 0;
                }

            }
            else if (isBlink == true)
            {
                GetComponent<SpriteRenderer>().enabled = true;
            }

            if (transform.localPosition.x < -0.4)
            {
                if (GameObject.Find("GameManager").GetComponent<GameController>().GameOver == false)
                {
                    Debug.Log("Die");
                    GlobalControl.Instance.PlayerWon = 1;
                    GameObject.Find("GameManager").GetComponent<GameController>().GameOver = true;
                    GameObject.Find("SpawnManager").GetComponent<WaveSpawner>().killAll = true;
                    GameObject.Find("SpawnManager").GetComponent<WaveSpawner>().startSpawn = false;
                    StartCoroutine("Wait");
                }
            }
        } else
        {
            if (transform.localPosition.x > 7.5)
            {

                nextBlink += Time.deltaTime;

                if (nextBlink >= 0.5)
                {
                    GetComponent<SpriteRenderer>().enabled = true;
                }

                if (nextBlink >= 1)
                {
                    GetComponent<SpriteRenderer>().enabled = false;
                    nextBlink = 0;
                }

            }
            else if (isBlink == true)
            {
                GetComponent<SpriteRenderer>().enabled = true;
            }

            if (transform.localPosition.x > 8.62)
            {
                if (GameObject.Find("GameManager").GetComponent<GameController>().GameOver == false)
                {
                    Debug.Log("Die");
                    GlobalControl.Instance.PlayerWon = 0;
                    GameObject.Find("GameManager").GetComponent<GameController>().GameOver = true;
                    GameObject.Find("SpawnManager").GetComponent<WaveSpawner>().killAll = true;
                    GameObject.Find("SpawnManager").GetComponent<WaveSpawner>().startSpawn = false;
                    StartCoroutine("Wait");
                }
            }
        }
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene(2); 
    }

}

