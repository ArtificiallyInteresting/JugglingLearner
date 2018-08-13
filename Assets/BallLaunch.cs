using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallLaunch : MonoBehaviour {

	public GameObject ball;
	public int frames;
	public int totalBalls;
	private GameObject[] balls;
	private int frameNo;
	private int ballsOut;
	// Use this for initialization
	void Start () {
		this.frameNo = 0;
		this.ballsOut = 0;
		this.balls = new GameObject[totalBalls];
	}

	public GameObject[] getBalls() {
		return balls;
	}
	public void ResetIteration() {
		foreach (var ball in this.balls) {
			Object.Destroy(ball);
		}
		this.Start();
	}
	
	// Update is called once per frame
	void Update () {
		if (totalBalls == ballsOut) {
			return;
		}
		this.frameNo += 1;
		if (this.frameNo % this.frames == 0) {
			GameObject newBall = (GameObject)GameObject.Instantiate(ball);
			newBall.transform.position = this.transform.position;
			balls[this.ballsOut] = newBall;
			this.ballsOut += 1;
		}
	}
}
