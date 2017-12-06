using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgeButtonTest : MonoBehaviour {

	[SerializeField]
	BridgeAction button;
	[SerializeField]
	BridgeController bridge;

	// Use this for initialization
	void Start () {
		button.addBridge( bridge );
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
