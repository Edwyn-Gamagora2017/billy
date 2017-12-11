using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class HitColliderAction : ColliderAction {
	public void hitAction(){
		this.action ();
	}
}
