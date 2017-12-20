﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHitView : MonoBehaviour {

	[SerializeField]
	Animator hitAnimator;

	public void hit(){
		hitAnimator.SetTrigger ( "hit" );
	}
	public void walk(){
		hitAnimator.SetTrigger ( "walk" );
	}
	public void walk_stop(){
		hitAnimator.SetTrigger ( "idle" );
	}

	public void finishHitView(){
		this.GetComponent<PlayerHitController> ().finishHit();
	}

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

	}
}
