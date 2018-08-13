using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MainControl : MonoBehaviour {

	public BallLaunch ballLaunch;
	public HandController hand;
	public Plane plane;
	private int iterations;
	public Text iterationsUi;
	public Text scoreUi;
	private float bestTime=0f;
	private float startTime;
	// Use this for initialization
	void Start () {
		this.iterations = 0;
		this.startTime = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void ResetIteration() {
		ballLaunch.ResetIteration();
		hand.ResetIteration();
		this.iterations += 1;
		iterationsUi.text = "Iterations: " + this.iterations.ToString();
		var lastStart = this.startTime;
		this.startTime = Time.time;
		if (this.startTime - lastStart > bestTime) {
			this.bestTime = this.startTime - lastStart;
			scoreUi.text = "Best Score: " + this.bestTime.ToString();	
		}
	}

	public void Failure() {

		this.ResetIteration();
	}
}
