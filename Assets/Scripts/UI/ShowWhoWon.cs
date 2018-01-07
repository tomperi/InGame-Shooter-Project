using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowWhoWon : MonoBehaviour {

	public int PlayerWon;
	public GameObject RightPlayer;
	public GameObject LeftPlayer;


	// Use this for initialization
	void Start () {

        PlayerWon = GlobalControl.Instance.PlayerWon;

        Debug.Log (PlayerWon);

		if (PlayerWon == 0) {
			RightPlayer.SetActive (true);
		} else {
			LeftPlayer.SetActive (true);
		}

	}

		
}
