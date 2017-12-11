using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectiblePlayerColliderAction : PlayerColliderAction {

	CollectibleController controller;

	public CollectibleController Controller {
		set {
			controller = value;
		}
	}

	protected override bool action(){
		Destroy ( this.gameObject );
		return true;
	}

	protected override bool validateAction (PlayerController player)
	{
		switch( controller.Collectible.Collectibletype ){
		case CollectibleElement.CollectibleType.Score:
			return player.collectScore ( 1 );
		case CollectibleElement.CollectibleType.Star:
		default:
			return player.collectStar ( 2 );
		}
	}

	// Use this for initialization
	void Start () {
		
	}

	// Update is called once per frame
	void Update () {
		
	}
}
