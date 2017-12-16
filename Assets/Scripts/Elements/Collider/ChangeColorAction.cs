using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeColorAction : HitColliderAction {

	protected override bool action(){
		ChangeColor cc = GetComponent<ChangeColor> ();
		if (cc == null) {
			return false;
		} else {
			GetComponent<ChangeColor>().change();
			return true;
		}
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
