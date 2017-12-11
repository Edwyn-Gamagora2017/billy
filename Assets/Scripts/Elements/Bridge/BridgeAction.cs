using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgeAction : HitColliderAction {

	List<BridgeController> bridges;

	public void addBridge( BridgeController action ){
		this.bridges.Add( action );
	}

	protected override bool action(){
		foreach( BridgeController bridge in bridges ){
			bridge.activateBridge();
		}
		this.GetComponent<ChangeColor>().change();
		return true;
	}

	void Awake(){
		this.bridges = new List<BridgeController>();
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
