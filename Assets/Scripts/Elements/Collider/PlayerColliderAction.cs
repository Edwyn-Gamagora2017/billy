using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerColliderAction : ColliderAction {

	// Check if the player allows the collision (and update the player based on the collision)
	protected abstract bool validateAction( PlayerController player );

	// Executes action if ValidateAction is true
	public void actionPlayer( PlayerController player ){
		if (this.validateAction (player)) {
			this.action ();
		}
	}
}
