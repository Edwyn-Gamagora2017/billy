using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

	List< PlayerController > players;

	[SerializeField]
	MapView map;

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

	void Awake(){
		this.players = new List<PlayerController> ();
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
