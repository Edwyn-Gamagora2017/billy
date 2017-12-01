using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapView : MonoBehaviour {

	Map mapModel;			// stores the map information

	Dictionary<KeyValuePair<int,int>, GameObject> tileGameObjects;	// Tile GameObjects

	// Prefabs that are going to be used to show the tiles of the map
	[SerializeField]
	GameObject wallPrefab;
	[SerializeField]
	GameObject carpetPrefab;

	[SerializeField]
	TextAsset mapFile;	// File describing the map

	[SerializeField]
	GameObject floor;

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

	// Create map Tiles
	private void drawMap(){
		this.tileGameObjects = new Dictionary<KeyValuePair<int, int>, GameObject>();

		if( this.mapModel != null ){
			for( int y=0; y<this.mapModel.Height; y++ ){
				for( int x=0; x<this.mapModel.Width; x++ ){
					Map.MapTileType type = this.mapModel.getTileType( x,y );
					this.tileGameObjects.Add( new KeyValuePair<int, int>( y,x ), this.createMapTile( type, x, y ) );
				}
			}
			// Adjust the camera
			Camera mapCamera = GameObject.FindObjectOfType<Camera>();
			if( mapCamera != null ){
				mapCamera.transform.position = new Vector3( this.mapModel.Width/2f-0.5f, this.mapModel.Height/2f-0.5f, -1 );	// 0.5 is the size of a half of the tile
				mapCamera.orthographicSize = Mathf.Max( this.mapModel.Width/2f, this.mapModel.Height/2f );
			}
			// Adjust Floor
			if(floor != null){
				floor.transform.localScale = new Vector3 ( this.mapModel.Width+0.1f, 0.2f, this.mapModel.Height+0.1f );
			}
		}
	}

	public GameObject createMapTileView( float x, float y, Map.MapTileType type ){
		GameObject result = null;

		switch( type ){
		case Map.MapTileType.Wall:
			result = GameObject.Instantiate (wallPrefab);
			break;
		}

		if (result) {
			result.gameObject.transform.position = new Vector3(-this.mapModel.Width/2f+x,0,-this.mapModel.Height/2f+y);
		}

		return result;
	}
}
