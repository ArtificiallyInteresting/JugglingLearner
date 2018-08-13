using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowLowestBall{
    public FollowLowestBall() {

    }
    public Vector3 getForce(GameObject[] balls, Transform hand, Rigidbody body) {
        if (balls[0] == null) {
            return new Vector3(0,0,0);
        }
        var lowestY = 10000000f;
        Transform lowestBall = hand; //Just for the compiler

        foreach (var ball in balls) {
            if (ball != null && ball.transform.position.y < lowestY) {
                lowestY = ball.transform.position.y;
                lowestBall = ball.transform;
            }
        }
        var xdiff = hand.position.x - lowestBall.position.x;
        var zdiff = hand.position.z - lowestBall.position.z;

        var moveConstant = -10;
        return new Vector3(xdiff * moveConstant, 1, zdiff * moveConstant);
    }
}