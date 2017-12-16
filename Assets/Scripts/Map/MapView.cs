using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TargetElement = MapElement;

using BlockElement = MapElement;

public class MapView : MonoBehaviour {

	Map mapModel;			// stores the map information

	List<BridgeController> bridges;

	[SerializeField]
	TextAsset mapFile;	// File describing the map

	Dictionary<KeyValuePair<int,int>, GameObject> tileGameObjects;	// Tile GameObjects

	[SerializeField]
	bool useThinWall = false;

	// Prefabs that are going to be used to show the tiles of the map
	// Walls
	[SerializeField]
	GameObject wallBigPrefab;
	[SerializeField]
	GameObject wallCenterPrefab;
	[SerializeField]
	GameObject wallCornerPrefab;
	[SerializeField]
	GameObject wallDoubleCornerPrefab;
	[SerializeField]
	GameObject wallCrossPrefab;
	// Floor
	[SerializeField]
	GameObject floorPrefab;
	// Carpet
	[SerializeField]
	GameObject carpetPrefab;

	[SerializeField]
	GameObject playerPrefab;
	[SerializeField]
	GameObject bridgePrefab;
	[SerializeField]
	GameObject bridgeButtonPrefab;
	[SerializeField]
	GameObject CollectiblePrefab;
	[SerializeField]
	GameObject TargetPrefab;
	[SerializeField]
	GameObject blockPrefab;

	[SerializeField]
	GameController gameController;	// Controls the game rules
	[SerializeField]
	GameObject tilesContainer;

	[SerializeField]
	GameObject simulationMap;

	/* PROPERTIES */
	public Map MapModel {
		set {
			mapModel = value;
			this.drawMap();
		}
	}

	void Awake(){
		bridges = new List<BridgeController>();
	}

	// Use this for initialization
	void Start () {
		MapModel = Map.read ( mapFile.text );
		GameObject.Destroy (simulationMap);
	}
	
	// Update is called once per frame
	void Update () {}

	// Create map Tiles
	private void drawMap(){
		this.tileGameObjects = new Dictionary<KeyValuePair<int, int>, GameObject>();

		if( this.mapModel != null ){
			// Create tiles
			for( int y=0; y<this.mapModel.Height; y++ ){
				for( int x=0; x<this.mapModel.Width; x++ ){
					Map.MapTileType type = this.mapModel.getTileType( x,y );
					this.tileGameObjects.Add( new KeyValuePair<int, int>( y,x ), this.createMapTile( type, x, y ) );
				}
			}
			// Create Player
			this.createPlayer( 0 );
			// Create Bridges
			foreach( BridgeElement bridge in this.mapModel.Bridges ){
				this.createBridge( bridge );
			}
			foreach( BridgeButton button in this.mapModel.BridgeButtons ){
				this.createBridgeButton( button );
			}
			// Create Collectibles
			foreach( CollectibleElement collectible in this.mapModel.Collectibles ){
				this.createCollectible ( collectible );
			}
			// Create Target
			this.createTarget( this.mapModel.PlayerTarget );

			// Create Blocks
			foreach( BlockElement block in this.mapModel.Blocks ){
				this.createBlock( block );
			}
			// Adjust the camera
			Camera mapCamera = GameObject.FindObjectOfType<Camera>();
			if( mapCamera != null ){
				mapCamera.GetComponent<TrackingCamera> ().Target = this.gameObject.transform;
				mapCamera.transform.position = new Vector3( 0, this.mapModel.Height+1, 0 );	// 0.5 is the size of a half of the tile
				//mapCamera.transform.position = new Vector3( this.mapModel.Width/2f-0.5f, this.mapModel.Height/2f-0.5f, -1 );	// 0.5 is the size of a half of the tile
				//mapCamera.orthographicSize = Mathf.Max( this.mapModel.Width/2f, this.mapModel.Height/2f );
			}
			// Adjust Kill Collider
			KillCollider killCollider = GetComponentInChildren<KillCollider>();
			if (killCollider != null) {
				killCollider.setScale ( this.mapModel.Width, this.mapModel.Height );
				killCollider.setMinHeight ( 0f );
			}
			// Adjust Floor
			/*if(floor != null){
				floor.transform.localScale = new Vector3 ( this.mapModel.Width-(useThinWall?0.9f:0f), 0.2f, this.mapModel.Height-(useThinWall?0.9f:0f));
				floor.transform.position = new Vector3 ( 0, -0.1f, 0 );
			}*/
		}
	}

	private GameObject createMapTile( Map.MapTileType type, int x, int y ){
		GameObject result = null;
		if( type != Map.MapTileType.NotDefined ){
			result = createMapTileView (x, y, type);
		}
		return result;
	}

	public GameObject createMapTileView( float x, float y, Map.MapTileType type ){
		GameObject result = null;

		Vector3 tilePosition = new Vector3(-this.mapModel.Width/2f+x+0.5f,0,-this.mapModel.Height/2f+y+0.5f);

		switch( type ){
		case Map.MapTileType.Wall:
			result = createWall (Mathf.RoundToInt (x), Mathf.RoundToInt (y));
			tilePosition.y = 0.9f;
			createMapTileView (x, y, Map.MapTileType.Floor);
			break;
		case Map.MapTileType.Floor:
			result = createFloor( Mathf.RoundToInt(x), Mathf.RoundToInt(y) );
			tilePosition.y = -0.1f;
			break;
		}

		if( result ){
			result.gameObject.transform.position = tilePosition;
		}

		return result;
	}

	public void createPlayer( int id ){
		Vector2 position = this.mapModel.PlayerSpawnerPosition;

		GameObject player = Instantiate( playerPrefab, this.transform );
		player.gameObject.transform.localPosition = new Vector3(-this.mapModel.Width/2f+ position.x +0.5f, 0, -this.mapModel.Height/2f+ position.y +0.5f);

		PlayerController playerCont = player.GetComponent<PlayerController> ();
		playerCont.setPlayerId (id);
		playerCont.GameController = gameController;
		gameController.addPlayer ( playerCont );
	}

	private void createBridge( BridgeElement bridge ){
		Vector2 position = bridge.position;
		GameObject obj = Instantiate( bridgePrefab, this.transform );
		BridgeController controller = obj.GetComponent<BridgeController>();

		obj.gameObject.transform.position = new Vector3(-this.mapModel.Width/2f+ position.x +0.5f, 0, -this.mapModel.Height/2f+ position.y +0.5f);
		// initial status
		controller.setInitialStatus( bridge.Closed );
		// Length
		if( bridge.Length > 1 ){
			obj.gameObject.transform.localScale = new Vector3( bridge.Length, bridge.Length, obj.gameObject.transform.localScale.z );
			Vector3 old_position = obj.gameObject.transform.position;
			obj.gameObject.transform.position = new Vector3(old_position.x+(bridge.VerticalOrientation?0f:bridge.Length/2f-0.5f), old_position.y, old_position.z-(!bridge.VerticalOrientation?0f:bridge.Length/2f-0.5f));
		}
		// Orientation
		if( bridge.VerticalOrientation ){
			obj.gameObject.transform.rotation = Quaternion.Euler( 0,90,0 );
		}

		bridges.Add( controller );
	}
	private void createBridgeButton( BridgeButton bridgeButton ){
		Vector2 position = bridgeButton.position;
		GameObject obj = Instantiate( bridgeButtonPrefab, this.transform );
		obj.gameObject.transform.position = new Vector3(-this.mapModel.Width/2f+ position.x +0.5f, 0.25f, -this.mapModel.Height/2f+ position.y +0.5f);

		BridgeAction action = obj.GetComponent<BridgeAction>();
		for( int i = 0; i < bridgeButton.Bridges.Count; i++ ){
			action.addBridge( this.bridges[ bridgeButton.Bridges[ i ].Index ] );
		}
	}
	private void createBlock( BlockElement block ){
		Vector2 position = block.position;
		GameObject obj = Instantiate( blockPrefab, this.transform );
		obj.gameObject.transform.position = new Vector3(-this.mapModel.Width/2f+ position.x +0.5f, obj.transform.localScale.y/2f, -this.mapModel.Height/2f+ position.y +0.5f);
	}

	private void createCollectible( CollectibleElement collectible ){
		Vector2 position = collectible.position;
		GameObject obj = Instantiate( CollectiblePrefab, this.transform );
		obj.gameObject.transform.position = new Vector3(-this.mapModel.Width/2f+ position.x +0.5f, obj.gameObject.transform.position.y, -this.mapModel.Height/2f+ position.y +0.5f);

		obj.GetComponent<CollectibleController> ().Collectible = collectible;
	}

	private void createTarget( TargetElement target ){
		Vector2 position = target.position;
		GameObject obj = Instantiate( TargetPrefab, this.transform );
		obj.gameObject.transform.position = new Vector3(-this.mapModel.Width/2f+ position.x +0.5f, obj.gameObject.transform.position.y, -this.mapModel.Height/2f+ position.y +0.5f);
	}

	private GameObject createWall( int x, int y ){
		if( !useThinWall ){
			return GameObject.Instantiate (wallBigPrefab, tilesContainer.transform);
		}
		else{
			// Evaluate Neigbors
			GameObject result;
			switch( amountWallNeighbors( x,y ) ){
			// One Neighbor
			case 1:
				result = GameObject.Instantiate (wallCenterPrefab, tilesContainer.transform);
				if( isWallNeighbor(x-1,y) || isWallNeighbor(x+1,y) ){
					result.gameObject.transform.rotation = Quaternion.Euler( 0,90,0 );
				}
				break;
			// Two Neighbors
			case 2:
				// Colinear neigbors
				if( isWallNeighbor(x,y-1) && isWallNeighbor(x,y+1) ){
					result = GameObject.Instantiate (wallCenterPrefab, tilesContainer.transform);
				}
				else if( isWallNeighbor(x-1,y) && isWallNeighbor(x+1,y) ){
					result = GameObject.Instantiate (wallCenterPrefab, tilesContainer.transform);
					result.gameObject.transform.rotation = Quaternion.Euler( 0,90,0 );
				}
				else{
					// Perpendicular Neighbors
					result = GameObject.Instantiate (wallCornerPrefab, tilesContainer.transform);
					if( isWallNeighbor(x,y+1) && isWallNeighbor(x+1,y) ){
						// No rotation
					}
					else if( isWallNeighbor(x+1,y) && isWallNeighbor(x,y-1) ){
						result.gameObject.transform.rotation = Quaternion.Euler( 0,90,0 );
					}
					else if( isWallNeighbor(x,y-1) && isWallNeighbor(x-1,y) ){
						result.gameObject.transform.rotation = Quaternion.Euler( 0,180,0 );
					}
					else if( isWallNeighbor(x-1,y) && isWallNeighbor(x,y+1) ){
						result.gameObject.transform.rotation = Quaternion.Euler( 0,270,0 );
					}
				}
				break;
			// Three Neighbors
			case 3:
				result = GameObject.Instantiate (wallDoubleCornerPrefab, tilesContainer.transform);
				if( !isWallNeighbor(x-1,y) ){
					// No rotation
				}
				if( !isWallNeighbor(x,y+1) ){
					result.gameObject.transform.rotation = Quaternion.Euler( 0,90,0 );
				}
				else if( !isWallNeighbor(x+1,y) ){
					result.gameObject.transform.rotation = Quaternion.Euler( 0,180,0 );
				}
				else if( !isWallNeighbor(x,y-1) ){
					result.gameObject.transform.rotation = Quaternion.Euler( 0,270,0 );
				}
				break;
			// No Neigbors or Four Neighbors
			case 4: default:
				result = GameObject.Instantiate (wallCrossPrefab, tilesContainer.transform);
				break;
			}
			return result;
		}
		return null;
	}

	private bool isWallNeighbor( int x, int y ){
		return mapModel.getTileType (x, y) == Map.MapTileType.Wall || mapModel.getTileType (x, y) == Map.MapTileType.Hole;
	}
	private int amountWallNeighbors( int x, int y ){
		int result = 0;
		if( isWallNeighbor(x-1,y) ){ result++; }
		if( isWallNeighbor(x+1,y) ){ result++; }
		if( isWallNeighbor(x,y-1) ){ result++; }
		if( isWallNeighbor(x,y+1) ){ result++; }
		return result;
	}

	private GameObject createFloor( int x, int y ){
		return GameObject.Instantiate (floorPrefab, tilesContainer.transform);
	}
}
