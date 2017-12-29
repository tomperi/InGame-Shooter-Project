using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MediumEnemyControl : MonoBehaviour {


    public int MHP;
    public int EHP;
    public int direction;
    public float movementSpeed;
    public bool down;
    public Player player;

    public GameObject p_easyenemy;


	// Use this for initialization
	void Start () {
       
        Time.timeScale = 1f;
 //       this.time = Time.time;
        InvokeRepeating("SpawnEnemy", 1f, 6f);
        //p_easyenemy.GetComponent<EasyEnemy>().HP = EHP;
        //p_easyenemy.GetComponent<EasyEnemy>().p_target = p_target;
    }
	
	// Update is called once per frame
	void Update () {
        
        Move();
	}

    public void Move()
    {
 //       Vector2 move = null;
        if (down)
        {
            
            if (this.transform.position.y < -2.5f)
            {
                down = false;
                direction = 1;
//                transform.position = new Vector2(0f, movementSpeed * direction);
            }

        }
        else
        {
            if (transform.position.y > 2.5f)
            { 
                down = true;
                direction = -1;
//                transform.position = new Vector3(0f, movementSpeed * direction );
            }
        }
        this.transform.position = new Vector3(transform.position.x, (movementSpeed * direction * Time.deltaTime) + transform.position.y );
        
    }

    //void OnTriggerEnter(Collider other)
    //{
    //    if (other.gameObject.tag == "Enemy")
    //    {
    //        Physics.IgnoreCollision(other.GetComponent<Collider>(), GetComponent<Collider>());
    //    }

    //    Debug.Log(other.tag);
    //    if (other.tag == "bullet") return;
    //    if (other.tag == "Player")
    //    {
    //        //other.GetComponent<PlayerController>().HP -= this.attackDamage;
    //        Destroy(gameObject);
    //    }
    //}

    public void SpawnEnemy()
    {

        GameObject EasyEnemy = Instantiate(
         p_easyenemy, this.transform.position, Quaternion.identity);
        EasyEnemy.GetComponent<BasicEnemy>().enemyStats.player = this.player;
    }

}
