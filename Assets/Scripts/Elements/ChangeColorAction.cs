using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeColorAction : HitColliderAction {

	public override void action(){
		GetComponent<ChangeColor>().change();
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
