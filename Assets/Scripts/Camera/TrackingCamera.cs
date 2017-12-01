using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackingCamera : MonoBehaviour {

	[SerializeField]
	Transform target;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (target) {
			this.transform.rotation = Quaternion.LookRotation ( (target.position - this.transform.position).normalized, Vector3.up );
		}
	}
}
