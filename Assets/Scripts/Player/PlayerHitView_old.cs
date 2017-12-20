using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHitView_old : MonoBehaviour {

	[SerializeField]
	Animator hitAnimator;

	public void hit(){
		hitAnimator.SetTrigger ( "activate" );
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
