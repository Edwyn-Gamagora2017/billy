using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrierController : MonoBehaviour {

	[SerializeField]
	GameObject barrier;	// Barrier that is controlled by this script

	bool horizontal;	// indicates the orientation of the barrier. Useful to control its position

	public bool Horizontal {
		get {
			return horizontal;
		}
		set {
			horizontal = value;
		}
	}

	Rigidbody rigidBody;

	// Use this for initialization
	void Start () {
		rigidBody = barrier.GetComponent<Rigidbody>();
		/*RigidbodyConstraints constraints = rigidBody.constraints;
		if( horizontal ){
			rigidBody.constraints = constraints | RigidbodyConstraints.FreezePositionZ;
		}
		else{
			rigidBody.constraints = constraints | RigidbodyConstraints.FreezePositionX;
		}*/
	}

	// Update is called once per frame
	void Update () {
		Vector3 localPos = transform.InverseTransformPoint( barrier.transform.position );
		//if( horizontal ){
		//	Debug.Log("eeeeee");
		//	barrier.transform.position = transform.TransformPoint( new Vector3( localPos.x, localPos.y, 0 ) );
		//}
		//else{
		//	Debug.Log("dasdsdas");
			barrier.transform.position = transform.TransformPoint( new Vector3( 0, localPos.y, localPos.z ) );
		//}
	}
}