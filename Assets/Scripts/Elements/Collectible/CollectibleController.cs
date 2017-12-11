using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleController : MonoBehaviour {

	CollectibleElement collectible;

	[SerializeField]
	Material scoreMaterial;
	[SerializeField]
	Material starMaterial;

	public CollectibleElement Collectible {
		get {
			return collectible;
		}
		set {
			collectible = value;
			setMaterial ();
		}
	}

	void setMaterial(){
		switch( collectible.Collectibletype ){
		case CollectibleElement.CollectibleType.Score:
			this.GetComponent<MeshRenderer> ().material = scoreMaterial;
			break;
		case CollectibleElement.CollectibleType.Star:
		default:
			this.GetComponent<MeshRenderer> ().material = starMaterial;
			break;
		}
	}

	void Awake(){
		this.GetComponent<CollectiblePlayerColliderAction> ().Controller = this;
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
