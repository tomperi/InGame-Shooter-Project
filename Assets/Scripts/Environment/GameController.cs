using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    public bool p1ready, p2ready;
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
        }
    }
}
