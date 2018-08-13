using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floor : MonoBehaviour {


    public MainControl control;

	void OnCollisionEnter (Collision col)
    {
        // print("Ball hit the plane!");
        control.Failure();
    }
}
