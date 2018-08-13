using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class ReinforcementOneBall {
    public float exploration=.1f;
    public float learningRate=.1f;
    public int strength=2000;
    private Dictionary<string, bool> seen; //Tells you if you've seen a given state before.
    private Dictionary<string, float> actions; //Maps states (with actions) to rewards
    private Dictionary<string, Vector3> vectors; //Just a reference to the possible actions
    private string lastState;
    private string lastAction;
    
    public ReinforcementOneBall() {
        this.seen = new Dictionary<string, bool>(); 
        this.actions = new Dictionary<string, float>();   
        this.vectors = getVectors(); 

    }
    public Vector3 getForce(GameObject[] balls, Transform hand, Rigidbody body) {
        if (balls[0] == null) {
            return new Vector3(0,0,0);
        }
        var random = new System.Random();
        var state = discretizeState(balls[0], hand, body);
        this.seen[state] = true;
        if (this.seen.ContainsKey(state)) {
            Console.WriteLine("I've seen this state before!");
            var bestAction = "";
            var bestActionValue = -9999999f;
            foreach (var action in vectors.Keys) {
                var newState = state + action;
                if (this.actions.ContainsKey(newState)){
                    var value = this.actions[newState];
                    if (value > bestActionValue) {
                        bestActionValue = value;
                        bestAction = action;
                    }
                }
            }
            if (random.NextDouble() > this.exploration && this.vectors.ContainsKey(bestAction)) {
                this.lastState = state;
                this.lastAction = bestAction;
                return this.vectors[bestAction];
            }
        }
        //Either haven't seen this before, or we're exploring.
        var randomAction = vectors.Keys.ElementAt(random.Next(0, vectors.Keys.Count));
        return vectors[randomAction];
    }

    public void processReward(float reward) {
        if (!this.actions.ContainsKey(lastState + lastAction)){
            this.actions[lastState+lastAction] = (float)(reward * .8);
            return;
        }
        this.actions[lastState + lastAction] = (learningRate * reward) + ((1-learningRate) * this.actions[lastState + lastAction]);

    }

    private string discretizeState(GameObject ball, Transform hand, Rigidbody body) {
        // var data = new[]{ball.transform.position.x, ball.transform.position.y, ball.transform.position.z,
        //     hand.position.x, hand.position.y, hand.position.z,
        //     body.velocity.x, body.velocity.y, body.velocity.z};
        var ballRb = ball.GetComponent<Rigidbody>();
        var data = new[]{
            hand.position.x - ball.transform.position.x, 
            hand.position.y - ball.transform.position.y,
            hand.position.z - ball.transform.position.z,
            body.velocity.x - ballRb.velocity.x,
            body.velocity.y - ballRb.velocity.y,
            body.velocity.z - ballRb.velocity.z
        };
        var scaleFactor = 10;
        var state = "";
        foreach (var datum in data) {
            state += ((int)(datum*scaleFactor)).ToString() + "|";
        }
        Debug.Log("State is: " + state);
        return state;
    }

    private Dictionary<string, Vector3> getVectors() {
        var vectors = new Dictionary<string, Vector3>();
        vectors.Add("U", new Vector3(0,-1*strength,0));
        vectors.Add("D", new Vector3(0,1*strength,0));
        vectors.Add("L", new Vector3(1*strength,0,0));//TODO: I don't know if these are right.
        vectors.Add("R", new Vector3(-1*strength,0,0));
        vectors.Add("F", new Vector3(0,0,1*strength));
        vectors.Add("B", new Vector3(0,0,-1*strength));

        return vectors;
    }
}