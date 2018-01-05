using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.UI;

public enum FadeOption {FadeIn, FadeOut};

[Serializable]
public class FadeOperation{


	//If first operation -> delay from enable script
	//If not -> delay from last fade operation
	public float delay = 0;  
	public float duration = 2f; //Transition duration
	public FadeOption fadeOption = FadeOption.FadeIn;


	private delegate void FadeFunction();
	private FadeFunction fade;

	public bool fadeDone {get; private set;}
	private float endTime;

	private CanvasGroup canvasGroup;

	public FadeOperation(FadeOption fadeOption, float delay, float duration)
	{
		this.fadeOption = fadeOption;
		this.delay = delay;
		this.duration = duration;
	}

	public FadeOperation(FadeOption fadeOption, float duration)
	{
		this.fadeOption = fadeOption;
		this.delay = 0;
		this.duration = duration;
	}

	//set canvas, alpha and the time that the operation will finish
	public void ConfigureFade(CanvasGroup canvasGroup)
	{
		this.canvasGroup = canvasGroup;
		fadeDone = false;

		switch (fadeOption) {
			case FadeOption.FadeIn: fade = FadeIn; canvasGroup.alpha = 0; break;
			default: fade = FadeOut; canvasGroup.alpha = 1;	break;
		}

		endTime = Time.time + delay; //update the delay to the end time

	}

	// Update is called once per frame
	public void Update () {
		

		if (!fadeDone) {
			fade ();
		}
	}

	private void FadeIn()
	{
		if (canvasGroup.alpha < 1 && Time.time > endTime) {
			canvasGroup.alpha += Time.deltaTime/duration;
			return ;
		}else if(canvasGroup.alpha >=1){
			canvasGroup.interactable = true;
			fadeDone = true;
		}
		
		return ;
	}
	
	private void FadeOut()
	{

		if (canvasGroup.alpha > 0 && Time.time > endTime) {
			canvasGroup.alpha -= Time.deltaTime/duration;
			return ;
		}else if(canvasGroup.alpha <= 0){
			canvasGroup.interactable = false;
			fadeDone = true;
		}
		
		
		
		return ;
	}

};


[RequireComponent(typeof (CanvasGroup) )]
public class UIFade : MonoBehaviour {


	public List<FadeOperation> fadeOperations;
	public bool OpsHasFinished {get; private set;} //Operations are finished
	public bool isFadeIn { get; private set; }


	private CanvasGroup canvasGroup;
	

	void Start () {
		canvasGroup = GetComponent<CanvasGroup> ();

		if (fadeOperations.Count >0) {

			fadeOperations[0].ConfigureFade(canvasGroup);
		}

		//Check if it's alpha by default
		if (canvasGroup.alpha == 0) {
			isFadeIn = false;
		} else {
			isFadeIn = true;
		}

	}

	void Update () {

		if (fadeOperations.Count == 0) {
			OpsHasFinished = true;
			return;

		}

		OpsHasFinished = false;
		FadeOperation fadeOp = fadeOperations [0];
		fadeOp.Update ();

		if (fadeOp.fadeDone) {
			UpdateFadeStatus(fadeOp);
			fadeOperations.Remove(fadeOp);

			//if there are more operations go to the next otherwise stop
			if(fadeOperations.Count >0)
			{
				fadeOperations[0].ConfigureFade(canvasGroup);
			}
		}
	}

	void UpdateFadeStatus(FadeOperation fadeOp){
		if(fadeOp.fadeOption == FadeOption.FadeIn)
			isFadeIn = true;
		else
			isFadeIn = false;
	}

	//Add a fade in operation by method
	public void AddFadeIn(float delay, float duration){
		FadeOperation fadeIn = new FadeOperation (FadeOption.FadeIn, delay, duration);
		fadeIn.ConfigureFade (canvasGroup);
		fadeOperations.Add (fadeIn);
	}

	//Add a fade in operation by method
	public void AddFadeOut(float delay, float duration){
		FadeOperation fadeOut = new FadeOperation (FadeOption.FadeOut, delay, duration);
		fadeOut.ConfigureFade (canvasGroup);
		fadeOperations.Add (fadeOut);
	}

	
	//Add a fade in operation by method
	public void AddFadeIn(float duration){
		FadeOperation fadeIn = new FadeOperation (FadeOption.FadeIn, duration);
		fadeIn.ConfigureFade (canvasGroup);
		fadeOperations.Add (fadeIn);
	}
	
	//Add a fade in operation by method
	public void AddFadeOut(float duration){
		FadeOperation fadeOut = new FadeOperation (FadeOption.FadeOut, duration);
		fadeOut.ConfigureFade (canvasGroup);
		fadeOperations.Add (fadeOut);
	}
}
