﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	float moveStep = 4f;
	float rotationStep = 3f;

	Rigidbody rigidyBody;	// RigidyBody useful to apply forces

	[SerializeField]
	PlayerHitController playerHit;

	void Awake(){
		this.rigidyBody = GetComponent<Rigidbody> ();
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		// Jump
		if( Input.GetKeyDown( KeyCode.Space ) ){
			this.rigidyBody.AddForce( new Vector3( 0,5,0 ), ForceMode.Impulse );
		}
		// Move
		if( Input.GetKey( KeyCode.LeftArrow ) ){
			this.transform.Rotate ( new Vector3(0,-moveStep,0) );
		}
		if( Input.GetKey( KeyCode.RightArrow ) ){
			this.transform.Rotate ( new Vector3(0,moveStep,0) );
		}
		if( Input.GetKey( KeyCode.UpArrow ) ){
			this.rigidyBody.MovePosition( this.transform.position + this.transform.forward*moveStep*Time.deltaTime );
			//this.transform.position += new Vector3 ( 0, 0, moveStep );
		}

		// Hit
		if( Input.GetKeyDown( KeyCode.W ) ){
			playerHit.hit ();
		}
	}
}
