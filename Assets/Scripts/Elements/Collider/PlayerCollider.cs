using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollider : MonoBehaviour {
	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

	}

	void OnTriggerEnter( Collider col ){
		PlayerController player = col.GetComponent<PlayerController> ();
		if (player != null) {
			this.GetComponentInParent<PlayerColliderAction> ().actionPlayer ( player );
		}
	}
}
