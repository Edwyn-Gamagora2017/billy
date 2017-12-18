using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

	List< PlayerController > players;

	[SerializeField]
	MapView map;

	float moveBoardStep = 70f;
	float maxMoveBoard = 15f;

	// Map Rotation
	float mapXrotation;
	float mapZrotation;

	private PlayerController getPlayerById( int id ){
		foreach (PlayerController p in players) {
			if (p.Player.Id == id) {
				return p;
			}
		}
		return null;
	}

	public void addPlayer ( PlayerController player ){
		if( this.getPlayerById( player.Player.Id ) == null ){
			this.players.Add ( player );
		}
	}

	public void rebornPlayer( PlayerController player ){
		GameObject.Destroy (player.gameObject);
		map.createPlayer ( player.Player.Id );
	}

	private void moveBoard( float moveVertical, float moveHorizontal ){
		Vector3 new_rotation = new Vector3( mapXrotation+moveHorizontal, 0, mapZrotation+moveVertical );
		if( new_rotation.x >= -maxMoveBoard && new_rotation.x <= maxMoveBoard && new_rotation.z >= -maxMoveBoard && new_rotation.z <= maxMoveBoard){
			map.transform.rotation = Quaternion.Euler( new_rotation );
			mapXrotation = new_rotation.x;
			mapZrotation = new_rotation.z;
		}
	}

	void Awake(){
		this.players = new List<PlayerController> ();
	}

	// Use this for initialization
	void Start () {
		mapXrotation = map.transform.rotation.eulerAngles.x;
		mapZrotation = map.transform.rotation.eulerAngles.z;
	}
	
	// Update is called once per frame
	void Update () {
		if( Input.GetKey(KeyCode.Keypad2) ){
			moveBoard( 0, -moveBoardStep*Time.deltaTime );
		}
		else if( Input.GetKey(KeyCode.Keypad8) ){
			moveBoard( 0, moveBoardStep*Time.deltaTime );
		}
		else{
			if( mapXrotation < 0 ){
				moveBoard( 0, Mathf.Min( moveBoardStep*Time.deltaTime, -mapXrotation ) );
			}
			else if( mapXrotation > 0 ){
				moveBoard( 0, Mathf.Max( -moveBoardStep*Time.deltaTime, -mapXrotation ) );
			}
		}
		if( Input.GetKey(KeyCode.Keypad4) ){
			moveBoard( moveBoardStep*Time.deltaTime, 0 );
		}
		else if( Input.GetKey(KeyCode.Keypad6) ){
			moveBoard( -moveBoardStep*Time.deltaTime, 0 );
		}
		else{
			if( mapZrotation < 0 ){
				moveBoard( Mathf.Min( moveBoardStep*Time.deltaTime, -mapZrotation ), 0 );
			}
			else if( mapZrotation > 0 ){
				moveBoard( Mathf.Max( -moveBoardStep*Time.deltaTime, -mapZrotation ), 0 );
			}
		}
	}
}
