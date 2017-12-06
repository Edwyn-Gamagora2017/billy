using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgeController : MonoBehaviour {

	Animator animator;
		
	public void activateBridge(){
		animator.SetTrigger("activate");
	}
	public void setInitialStatus( bool initialStatusClosed ){
		animator.SetInteger("initialStatus",initialStatusClosed?0:1);
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
