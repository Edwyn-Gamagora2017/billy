using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bridge : MonoBehaviour {

	[SerializeField]
	bool closed;

	Animator animator;

	public bool Closed {
		get {
			return closed;
		}
		set{
			closed = value;
		}
	}
		
	public void activateBridge(){
		animator.SetTrigger("activate");
	}

	void Awake(){
		animator = GetComponent<Animator>();
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	}
}
