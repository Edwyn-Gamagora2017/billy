using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHitController : MonoBehaviour {

	[SerializeField]
	PlayerController player;

	protected bool hitActivate;		// indicates that the hit was activated

	public PlayerController Player {
		get {
			return player;
		}
	}

	public bool HitActivate {
		get {
			return hitActivate;
		}
	}

	public void hit(){
		this.hitActivate = true;
		this.GetComponent<PlayerHitView> ().hit();
	}

	public void finishHit(){
		this.hitActivate = false;
	}

	// Use this for initialization
	void Start () {
		this.hitActivate = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
