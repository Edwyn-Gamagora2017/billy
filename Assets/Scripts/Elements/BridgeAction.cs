using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgeAction : HitColliderAction {

	[SerializeField]
	Bridge[] bridges;

	public override void action(){
		foreach( Bridge bridge in bridges ){
			bridge.activateBridge();
		}
		this.GetComponent<ChangeColor>().change();
	}

	public void addBridge( Bridge[] newBridges ){
		bridges = newBridges;
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
