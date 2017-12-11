using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleTest : MonoBehaviour {

	public CollectibleController collectible;
	public CollectibleElement.CollectibleType collectibleType;

	// Use this for initialization
	void Start () {
		collectible.Collectible = new CollectibleElement ( this.collectibleType, new Vector2(0,0) );;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
