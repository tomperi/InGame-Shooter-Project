using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BonusUI : MonoBehaviour {
    public bool show;
    public float alpha;

	// Use this for initialization
	void Start () {
        alpha = 0f;
        GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, alpha);
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (show)
        {
            if (alpha < 1) 
                alpha = alpha + 0.01f;
        }
        else
        {
            if (alpha > 0)
                alpha = alpha - 0.01f;
        }
        GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, alpha);
    }
}
