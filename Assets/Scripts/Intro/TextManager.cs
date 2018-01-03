using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextManager : MonoBehaviour {

	public float timer;
	public float aValue = 1;
	float TmStart;
	float TmLen = 5f;
	private CanvasGroup trans;

	void Start()
	{
		TmStart = Time.time;
		trans = GetComponent<CanvasGroup>();
		trans.alpha = aValue;
	}

	void Update () {
		timer += Time.deltaTime;

		// Dissapear after 5 seconds;
		if (Time.time > TmStart + TmLen) {
			GetComponent<Text> ().gameObject.SetActive (true);
		}

		if (timer >= 0.5) {
			GetComponent<Text> ().enabled = true;
		}

		if (timer >= 1) {
			GetComponent<Text> ().enabled = false;
			timer = 0;
		}

		if (Input.anyKeyDown) {
			Destroy (gameObject);
		}
			
	}
}
