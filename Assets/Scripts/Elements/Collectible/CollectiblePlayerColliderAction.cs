using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectiblePlayerColliderAction : PlayerColliderAction {

	protected override bool action(){
		Destroy ( this.gameObject );
		return true;
	}

	protected override bool validateAction (PlayerController player)
	{
		return player.collect ();
	}

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

	}

}
