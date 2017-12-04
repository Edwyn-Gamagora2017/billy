using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillCollider : MonoBehaviour {

	[SerializeField]
	GameController gameController;

	public void setMinHeight( float height ){
		Vector3 old_position = this.transform.position;
		this.transform.position = new Vector3 (old_position.x, height-1f, old_position.z);
	}

	public void setScale( float width, float depth ){
		this.transform.localScale = new Vector3 ( width+5f, this.transform.localScale.y, depth+5f );
	}

	void OnTriggerEnter( Collider col ){
		PlayerController player = col.GetComponent< PlayerController > ();
		if( player != null ){
			gameController.rebornPlayer (player);
		}
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
