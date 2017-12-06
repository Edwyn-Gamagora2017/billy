using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgeTest : MonoBehaviour {
	
	[SerializeField]
	BridgeController bridge;
	[SerializeField]
	bool bridgeClosed;

	// Use this for initialization
	void Start () {
		bridge.setInitialStatus( bridgeClosed );
	}
	
	// Update is called once per frame
	void Update () {
	}
}
