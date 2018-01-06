using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Check : MonoBehaviour {

	public int PlayerWon;

	// Use this for initialization
	void Start () {

		GlobalControl.Instance.PlayerWon = PlayerWon;
		Debug.Log (PlayerWon);
	
	}

}
