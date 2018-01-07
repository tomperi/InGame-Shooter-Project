using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BonusUI : MonoBehaviour
{
    public bool show;
    public int count;
    public int dieAfterCount;

    //public float alpha;
    //public Player player;

    // Use this for initialization
    void Start()
    {
        //alpha = 0f;
        //if (player.Equals(Player.right))
        //    player = GameObject.Find("HeroLeft").GetComponent<PlayerControl>().player;
        //else
        //    player = GameObject.Find("HeroRight").GetComponent<PlayerControl>().player;
        //GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, alpha);
    }

    // Update is called once per frame
    void Update()
    {
        //if (show)
        //{
        //    //    if (alpha < 1) 
        //    //        alpha = alpha + 0.01f;
        //    //}
        //    //else
        //    //{
        //    //    if (alpha > 0)
        //    //        alpha = alpha - 0.01f;
        //    //}
        //    GetComponent<SpriteRenderer>().enabled = true;
        //}
        //else
        //{
        //    GetComponent<SpriteRenderer>().enabled = false;
        //}
    }

    public void startRoullete()
    {
        GetComponent<SpriteRenderer>().enabled = true;
        show = true;
        count = 0;
    }

    public void dieAfter()
    {
        GameObject go;
        if (show)
        {
            if (dieAfterCount == count)
            {
                GetComponent<SpriteRenderer>().enabled = false;
                show = false;
                go = GameObject.Find("BonusKillAllRight");
                if (go != null)
                    go.GetComponent<SpriteRenderer>().enabled = true;
                go = GameObject.Find("BonusOpenSpaceRight");
                if (go != null)
                    go.GetComponent<SpriteRenderer>().enabled = true;
                go = GameObject.Find("BonusSummonRight");
                if (go != null)
                    go.GetComponent<SpriteRenderer>().enabled = true;
                go = GameObject.Find("BonusKillAllLeft");
                if (go != null)
                    go.GetComponent<SpriteRenderer>().enabled = true;
                go = GameObject.Find("BonusOpenSpaceLeft");
                if (go != null)
                    go.GetComponent<SpriteRenderer>().enabled = true;
                go = GameObject.Find("BonusSummonLeft");
                if (go != null)
                    go.GetComponent<SpriteRenderer>().enabled = true;
                go = GameObject.Find("BonusActivateLeft");
                if ((go != null) && GameObject.Find("HeroLeft").GetComponent<PlayerControl>().hasBonus)
                    go.GetComponent<SpriteRenderer>().enabled = true;
                go = GameObject.Find("BonusActivateRight");
                if ((go != null) && GameObject.Find("HeroRight").GetComponent<PlayerControl>().hasBonus)
                    go.GetComponent<SpriteRenderer>().enabled = true;
            }
            count++;
        }
    }


}
