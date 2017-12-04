using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bridge : MonoBehaviour {

	Vector3 closedPosition;
	Quaternion closedRotation;
	Vector3 openedPosition;
	Quaternion openedRotation;

	private bool closed;

	public bool Closed {
		get {
			return closed;
		}
		set {
			closed = value;
		}
	}
		
	public void finishClose(){
		closedPosition = this.transform.position;
		closedRotation = this.transform.rotation;
	}

	// Use this for initialization
	void Start () {
		closedPosition = this.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		
		this.transform.position = closedPosition;
	}
}
