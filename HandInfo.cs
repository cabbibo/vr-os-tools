﻿
using UnityEngine;
using System.Collections;

public class HandInfo : MonoBehaviour {


	public Structs.Hand hand;

	public Vector3 debug;

	public Vector3 velocity;
	public float trigger;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		
		hand.localToWorld = transform.localToWorldMatrix;
		hand.worldToLocal = transform.worldToLocalMatrix;
		hand.pos = transform.position;
		hand.vel = velocity;
		hand.trigger = trigger;
		hand.debug = debug;

	}

	
}