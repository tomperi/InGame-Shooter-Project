using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlickeringText : MonoBehaviour {

    public float timer;
    public float aValue = 1;
    private int flickCount = 0;
    private CanvasGroup trans;
    private bool show = false;

    void Start ()
    {
        trans = GetComponent<CanvasGroup>();
        trans.alpha = aValue;
    }
	
    void Update()
    {
        timer += Time.deltaTime;

        if (show)
        {
            if (timer >= 0.5)
            {
                GetComponent<Text>().enabled = true;
            }

            if (timer >= 1)
            {
                GetComponent<Text>().enabled = false;
                timer = 0;
                flickCount++;
            }

        }

        if (flickCount == 5)
        {
            show = false;
        }

    }

    public void appear()
    {
        show = true;
    }

}
