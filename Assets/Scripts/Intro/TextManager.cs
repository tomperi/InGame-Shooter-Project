using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextManager : MonoBehaviour {

	public float timer;
	public float aValue = 1;
	private CanvasGroup trans;
	private bool show = false;

	void Start()
	{
		trans = GetComponent<CanvasGroup>();
		trans.alpha = aValue;
		GetComponent<Text> ().enabled = false;
		Appear ();
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
				Destroy (gameObject);
			}
		
		}
			
	}

	public void Appear() 
	{
		show = true;
	}
}
