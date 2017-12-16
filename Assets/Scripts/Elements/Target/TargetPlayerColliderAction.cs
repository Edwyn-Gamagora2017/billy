using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetPlayerColliderAction : PlayerColliderAction {

	protected override bool action(){
		return true;
	}

	protected override bool validateAction (PlayerController player)
	{
		return player.reachTarget ();
	}

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

	}
}
