using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    public bool p1ready, p2ready;
    public GameObject b1p1, b2p1, b3p1;
    public GameObject b1p2, b2p2, b3p2;
    public bool GameOver = false;

    public GameObject mainMusic;
	// Use this for initialization
	void Start () {
		
	}

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("caps lock"))
        {
            p2ready = true;
            Destroy(GameObject.Find("KeyTutorialLeft"));
        }

        if (Input.GetKey("right ctrl"))
        {
            p1ready = true;
            Destroy(GameObject.Find("KeyTutorialRight"));
        }

        if (p1ready && p2ready)
        {
            GameObject.Find("SpawnManager").GetComponent<WaveSpawner>().startSpawn = true;
            if (GameObject.Find("Intro Music") != null)
                GameObject.Find("Intro Music").SetActive(false);

            mainMusic.SetActive(true);
        }
    }

    public void GiveBonus(Player player)
    {
        if (player.Equals(Player.right))
        {
            GameObject.Find("HeroRight").GetComponent<PlayerControl>().hasBonus = true;
            GameObject.Find("HeroRight").GetComponent<PlayerControl>().bonusType = RandomBonus(player);
            GameObject.Find("RouletteRight").GetComponent<BonusUI>().startRoullete();
        }
        else
        {
            GameObject.Find("HeroLeft").GetComponent<PlayerControl>().hasBonus = true;
            GameObject.Find("HeroLeft").GetComponent<PlayerControl>().bonusType = RandomBonus(player);
            GameObject.Find("RouletteLeft").GetComponent<BonusUI>().startRoullete();
        }
    }

    public BonusTypes RandomBonus(Player player)
    {
        //public enum BonusTypes { spawnLargeEnemy, moveWall, destroyEnemies };
        int bonus = Random.Range(0, 2);
        Debug.Log(bonus);
        if (bonus == 1)
        {
            if (player.Equals(Player.right))
            {
                b1p1.SetActive(true);
            }
            else
            {
                b1p2.SetActive(true);
            }
            return BonusTypes.spawnLargeEnemy;
            
        } 

        else if (bonus == 2)
        {
            if (player.Equals(Player.right))
            {
                b2p1.SetActive(true);
            }
            else
            {
                b2p2.SetActive(true);
            }
            return BonusTypes.moveWall;
        }

        else
        {
            if (player.Equals(Player.right))
            {
                b3p1.SetActive(true);
            }
            else
            {
                b3p2.SetActive(true);
            }
            return BonusTypes.destroyEnemies;
        }
        
    }

    public void PlayerUsedBonus(Player player)
    {
        Debug.Log(player + " used bonus");
        if (player.Equals(Player.right))
        {
            foreach (GameObject go in GameObject.FindGameObjectsWithTag("bonusIconsRight"))
            {
                go.GetComponent<SpriteRenderer>().enabled = false;
                go.SetActive(false);
            }
            if (GameObject.Find("HeroRight").GetComponent<PlayerControl>().firstBonus == true)
            {
                GameObject.Find("BonusActivateRight").SetActive(false);
                GameObject.Find("HeroRight").GetComponent<PlayerControl>().firstBonus = false;
            }
        } else
        {
            foreach (GameObject go in GameObject.FindGameObjectsWithTag("bonusIconsLeft"))
            {
                go.GetComponent<SpriteRenderer>().enabled = false;
                go.SetActive(false);
            }
            if (GameObject.Find("HeroLeft").GetComponent<PlayerControl>().firstBonus == true)
            {
                GameObject.Find("BonusActivateLeft").SetActive(false);
                GameObject.Find("HeroLeft").GetComponent<PlayerControl>().firstBonus = false;
            }
        }
    }
}
