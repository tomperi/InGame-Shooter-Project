using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndingScene : MonoBehaviour {

	public float timer;
	public float aValue = 1;
	private CanvasGroup trans;
	private bool show = true;
	public GameObject Wall;
	public int PlayerWon;


	void Start()
	{
		trans = GetComponent<CanvasGroup>();
		trans.alpha = aValue;
		GetComponent<Text> ().enabled = false;
        PlayerWon = GlobalControl.Instance.PlayerWon;
    }                                        

	void Update () {
		timer += Time.deltaTime;

		if (show) {
			if (timer >= 0.5) {
				GetComponent<Text> ().enabled = true;
			}

			if (timer >= 1) {
				GetComponent<Text> ().enabled = false;
				timer = 0;
			}

			if (Input.GetKeyDown(KeyCode.Space)) {
				this.GetComponent<Text> ().enabled = false;
				show = false;
				Destroy (GameObject.Find ("PlayerWin"));

				Wall.SetActive (true);

//
//				if (PlayerWon == 0) {
//					FadeoutR.SetActive (true);
//				} else {
//					FadeoutL.SetActive (true);
//				}

			}

		}

//		if (FadeoutL.GetComponent<CanvasGroup> ().alpha == 0f) {
//			Wall.SetActive (true);
//		}


	}

	public void Appear() 
	{
		show = true;
	}
}

