using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	private Player player;
	private GameController gameController;

	float moveStep = 4f;
	float rotationStep = 3f;

	float old_y_position;	// stores the position of the player in the last frame (useful to check if the player is jumping)

	Rigidbody rigidyBody;	// RigidyBody useful to apply forces

	[SerializeField]
	PlayerHitController playerHit;

	/**
	 * GETTERS and SETTERS
	 */
	public Player Player {
		get {
			return player;
		}
	}
	public void setPlayerId(int id){
		this.player = new Player(id);
	}

	public GameController GameController {
		set {
			gameController = value;
		}
	}

	void Awake(){
		this.rigidyBody = GetComponent<Rigidbody> ();
	}

	// Use this for initialization
	void Start () {
		if (player == null) {
			player = new Player (-1);
		}
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		// Jump
		if( Input.GetKeyDown( KeyCode.Space ) ){
			if( Mathf.Approximately( this.transform.position.y, old_y_position ) ){
				this.rigidyBody.AddForce( new Vector3( 0,3,0 ), ForceMode.Impulse );
			}
		}
		old_y_position = this.transform.position.y;
		// Move
		if( Input.GetKey( KeyCode.LeftArrow ) || Input.GetKey( KeyCode.A ) ){
			this.transform.Rotate ( new Vector3(0,-moveStep,0) );
		}
		if( Input.GetKey( KeyCode.RightArrow ) || Input.GetKey( KeyCode.D ) ){
			this.transform.Rotate ( new Vector3(0,moveStep,0) );
		}
		if( Input.GetKey( KeyCode.UpArrow ) || Input.GetKey( KeyCode.W ) ){
			//this.rigidyBody.AddForce( this.transform.forward*moveStep*Time.deltaTime, ForceMode.VelocityChange );
			this.rigidyBody.MovePosition( this.rigidyBody.position + this.transform.forward*moveStep*Time.deltaTime );
			//this.transform.position += new Vector3 ( 0, 0, moveStep );
		}

		// Hit
		if( Input.GetKeyDown( KeyCode.H ) ){
			playerHit.hit ();
		}
	}

	// Collisions to Collectible
	public bool collectScore( int value ){
		player.addScore ( value );
		Debug.Log ( "Score" );
		return true;
	}
	public bool collectStar( int value ){
		player.addScore ( value );
		Debug.Log ( "Star" );
		return true;
	}

	// Collisions to Target
	public bool reachTarget(){
		Debug.Log ( "Target" );
		if (this.gameController != null) {
			this.gameController.rebornPlayer( this );
		}
		return true;
	}
}
