using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextManager : MonoBehaviour {

	public float timer;
	public float aValue = 1;
	private CanvasGroup trans;
	private bool show = true;
	public GameObject Fadeout;
	public GameObject Wall;

	void Start()
	{
		trans = GetComponent<CanvasGroup>();
		trans.alpha = aValue;
		GetComponent<Text> ().enabled = false;
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

			if (Input.anyKeyDown) {
				this.GetComponent<Text> ().enabled = false;
				show = false;
				Destroy (GameObject.Find ("LogoFadeIn"));

				Fadeout.SetActive (true);
				Debug.Log (Fadeout.GetComponent<CanvasGroup> ().alpha);

			}
		
		}

		if (Fadeout.GetComponent<CanvasGroup> ().alpha == 0f) {
			Wall.SetActive (true);
		}
			
			
	}

	public void Appear() 
	{
		show = true;
	}
}
