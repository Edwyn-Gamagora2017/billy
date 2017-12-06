using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgeButton : MapElement {

	List<BridgeElement> bridges;

	public List<BridgeElement> Bridges {
		get {
			return bridges;
		}
	}

	public BridgeButton( Vector2 pos ):base(pos){
		this.bridges = new List<BridgeElement>();
	}

	public void addBridge( BridgeElement bridge  ){
		this.bridges.Add( bridge );
	}
}
