using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapView : MonoBehaviour {

	Map mapModel;			// stores the map information

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
	// Carpet
	[SerializeField]
	GameObject carpetPrefab;

	[SerializeField]
	GameObject floor;
	[SerializeField]
	GameObject playerPrefab;

	/* PROPERTIES */
	public Map MapModel {
		set {
			mapModel = value;
			this.drawMap();
		}
	}

	// Use this for initialization
	void Start () {
		MapModel = Map.read ( mapFile.text );
	}
	
	// Update is called once per frame
	void Update () {}

	private GameObject createMapTile( Map.MapTileType type, int x, int y ){
		GameObject result = null;
		if( type != Map.MapTileType.NotDefined ){
			result = createMapTileView (x, y, type);
		}
		return result;
	}

	private void createPlayer( Vector2 position ){
		GameObject player = Instantiate( playerPrefab, this.transform );
		player.gameObject.transform.position = new Vector3(-this.mapModel.Width/2f+ position.x +0.5f, 0, -this.mapModel.Height/2f+ position.y +0.5f);
	}

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
			// Adjust Floor
			if(floor != null){
				floor.transform.localScale = new Vector3 ( this.mapModel.Width-(useThinWall?0.9f:0f), 0.2f, this.mapModel.Height-(useThinWall?0.9f:0f));
				floor.transform.position = new Vector3 ( 0, -0.1f, 0 );
			}
			// Create Player
			this.createPlayer( this.mapModel.PlayerSpawnerPosition );
			// Adjust the camera
			Camera mapCamera = GameObject.FindObjectOfType<Camera>();
			if( mapCamera != null ){
				mapCamera.transform.position = new Vector3( 0, this.mapModel.Height+1, 0 );	// 0.5 is the size of a half of the tile
				//mapCamera.transform.position = new Vector3( this.mapModel.Width/2f-0.5f, this.mapModel.Height/2f-0.5f, -1 );	// 0.5 is the size of a half of the tile
				//mapCamera.orthographicSize = Mathf.Max( this.mapModel.Width/2f, this.mapModel.Height/2f );
			}
		}
	}

	public GameObject createMapTileView( float x, float y, Map.MapTileType type ){
		GameObject result = null;

		Vector3 tilePosition = new Vector3(-this.mapModel.Width/2f+x+0.5f,0,-this.mapModel.Height/2f+y+0.5f);

		switch( type ){
		case Map.MapTileType.Wall:
			result = createWall( Mathf.RoundToInt(x), Mathf.RoundToInt(y) );
			tilePosition.y = 0.9f;
			break;
		}

		if( result ){
			result.gameObject.transform.position = tilePosition;
		}

		return result;
	}

	private GameObject createWall( int x, int y ){
		if( !useThinWall ){
			return GameObject.Instantiate (wallBigPrefab, this.transform);
		}
		else{
			// Evaluate Neigbors
			GameObject result;
			switch( amountWallNeighbors( x,y ) ){
			// One Neighbor
			case 1:
				result = GameObject.Instantiate (wallCenterPrefab, this.transform);
				if( mapModel.getTileType(x-1,y) == Map.MapTileType.Wall || mapModel.getTileType(x+1,y) == Map.MapTileType.Wall ){
					result.gameObject.transform.rotation = Quaternion.Euler( 0,90,0 );
				}
				break;
			// Two Neighbors
			case 2:
				// Colinear neigbors
				if( mapModel.getTileType(x,y-1) == Map.MapTileType.Wall && mapModel.getTileType(x,y+1) == Map.MapTileType.Wall ){
					result = GameObject.Instantiate (wallCenterPrefab, this.transform);
				}
				else if( mapModel.getTileType(x-1,y) == Map.MapTileType.Wall && mapModel.getTileType(x+1,y) == Map.MapTileType.Wall ){
					result = GameObject.Instantiate (wallCenterPrefab, this.transform);
					result.gameObject.transform.rotation = Quaternion.Euler( 0,90,0 );
				}
				else{
					// Perpendicular Neighbors
					result = GameObject.Instantiate (wallCornerPrefab, this.transform);
					if( mapModel.getTileType(x,y+1) == Map.MapTileType.Wall && mapModel.getTileType(x+1,y) == Map.MapTileType.Wall ){
						// No rotation
					}
					else if( mapModel.getTileType(x+1,y) == Map.MapTileType.Wall && mapModel.getTileType(x,y-1) == Map.MapTileType.Wall ){
						result.gameObject.transform.rotation = Quaternion.Euler( 0,90,0 );
					}
					else if( mapModel.getTileType(x,y-1) == Map.MapTileType.Wall && mapModel.getTileType(x-1,y) == Map.MapTileType.Wall ){
						result.gameObject.transform.rotation = Quaternion.Euler( 0,180,0 );
					}
					else if( mapModel.getTileType(x-1,y) == Map.MapTileType.Wall && mapModel.getTileType(x,y+1) == Map.MapTileType.Wall ){
						result.gameObject.transform.rotation = Quaternion.Euler( 0,270,0 );
					}
				}
				break;
			// Three Neighbors
			case 3:
				result = GameObject.Instantiate (wallDoubleCornerPrefab, this.transform);
				if( mapModel.getTileType(x-1,y) != Map.MapTileType.Wall ){
					// No rotation
				}
				if( mapModel.getTileType(x,y+1) != Map.MapTileType.Wall ){
					result.gameObject.transform.rotation = Quaternion.Euler( 0,90,0 );
				}
				else if( mapModel.getTileType(x+1,y) != Map.MapTileType.Wall ){
					result.gameObject.transform.rotation = Quaternion.Euler( 0,180,0 );
				}
				else if( mapModel.getTileType(x,y-1) != Map.MapTileType.Wall ){
					result.gameObject.transform.rotation = Quaternion.Euler( 0,270,0 );
				}
				break;
			// No Neigbors or Four Neighbors
			case 4: default:
				result = GameObject.Instantiate (wallCrossPrefab, this.transform);
				break;
			}
			return result;
		}
		return null;
	}

	private int amountWallNeighbors( int x, int y ){
		int result = 0;
		if( mapModel.getTileType(x-1,y) == Map.MapTileType.Wall ){ result++; }
		if( mapModel.getTileType(x+1,y) == Map.MapTileType.Wall ){ result++; }
		if( mapModel.getTileType(x,y-1) == Map.MapTileType.Wall ){ result++; }
		if( mapModel.getTileType(x,y+1) == Map.MapTileType.Wall ){ result++; }
		return result;
	}
}
