using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
// using BallLaunch;

public class HandController : MonoBehaviour {

	private ReinforcementOneBall brain; //TODO make this generic
	public BallLaunch ballLaunch;
	private Vector3 initialPosition;
	// Use this for initialization
	void Start () {
		this.brain = new ReinforcementOneBall();
		// Console.WriteLine("Constructed");
		this.initialPosition = this.transform.position;

	}

	
	// Update is called once per frame
	void Update () {
		if (this.brain == null) {
			return;
		}
		var reward = getReward();
		this.brain.processReward(reward);
		var rb = this.GetComponent<Rigidbody>();
		var force = this.brain.getForce(ballLaunch.getBalls(), this.transform, rb);
		rb.AddForce(force);
	}

	//TODO: This isn't right and shouldn't live here.
	float getReward() {
		GameObject[] balls = this.ballLaunch.getBalls();
		var score = 0f;
		foreach (var ball in balls) {
			if (ball != null) {
				score += ball.transform.position.y;
			}
		}
		return score;
	}
	public void ResetIteration() {
		this.transform.position = this.initialPosition;
		var rb = this.GetComponent<Rigidbody>();
		rb.velocity = Vector3.zero;
		rb.rotation = Quaternion.identity;
		rb.angularVelocity = Vector3.zero;

	}
}
