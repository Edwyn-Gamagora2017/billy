﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackingCamera : MonoBehaviour {

	[SerializeField]
	Transform target;

	public Transform Target {
		get {
			return target;
		}
		set {
			target = value;
		}
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (target) {
			this.transform.LookAt ( target.position.normalized );
		}
	}
}
