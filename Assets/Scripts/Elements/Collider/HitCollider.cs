using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitCollider : MonoBehaviour {

	private PlayerController hitByPlayer = null;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter( Collider col ){
		PlayerHitController playerHit = col.GetComponent<PlayerHitController> ();
		if (playerHit != null && playerHit.HitActivate) {
			hitByPlayer = playerHit.Player;
			playerHit.finishHit ();

			this.GetComponentInParent<HitColliderAction> ().hitAction ();
		}
	}

	void OnTriggerExit( Collider col ){
		PlayerHitController playerHit = col.GetComponent<PlayerHitController> ();
		if (playerHit != null) {
			hitByPlayer = null;
		}
	}
}
