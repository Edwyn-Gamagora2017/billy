using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnAroundItself : MonoBehaviour {

	public enum Axis{x,y,z};

	[SerializeField]
	float rotationStep = 15f;

	[SerializeField]
	Axis axis = Axis.y;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		float step = rotationStep * Time.deltaTime;
		this.transform.Rotate (new Vector3 ( (axis==Axis.x?step:0), (axis==Axis.y?step:0), (axis==Axis.z?step:0) ));
	}
}
