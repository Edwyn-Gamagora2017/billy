using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// a map is a grid graph composed of tiles with different values
public class Map {
	
	// Type of a map tile : it is associated to a value for the tile
	public enum MapTileType{
		Wall, Floor, Carpet, NotDefined
	};

	// Class to be used to store a vertex in the graph
	public class TileInfo{
		// Map
		public MapTileType type;	// type of the tile
		public int x;				// position of the tile in the map : X component
		public int y;				// position of the tile in the map : Y component

		public TileInfo( int x, int y, MapTileType type ){
			this.x = x;
			this.y = y;
			this.type = type;
		}
	};

	int height;							// Map height
	int width;							// Map width
	private TileInfo[][] mapTiles;		// the grid of tile

	public Map( int height, int width ){
		this.height = height;
		this.width = width;

		// Creating empty matrix
		this.mapTiles = new TileInfo[height][];
		for(int y = 0; y < height; y++){
			this.mapTiles[y] = new TileInfo[width];
			for(int x = 0; x < width; x++){
				this.mapTiles[y][x] = new TileInfo(x,y,MapTileType.NotDefined);
			}
		}
	}

	/* PROPERTIES */
	public int Height {
		get {
			return height;
		}
	}
	public int Width {
		get {
			return width;
		}
	}

	/*GETTERS*/
	public MapTileType getTileType(int x, int y){
		if( this.isValidTilePosition(x,y) ){
			return this.mapTiles[y][x].type;
		}
		else{
			return MapTileType.NotDefined;
		}
	}

	/*
	 * SETTERS
	 */
	// insert the tile in the map matrix and into the graph
	public void addTile( int x, int y, MapTileType type ){
		// Check map bounds
		if( this.isValidTilePosition(x,y) ){
			TileInfo info = new TileInfo( x, y, type);
			this.mapTiles[y][x] = info;
		}
	}

	public bool isValidTilePosition( int x, int y ){
		return( y >= 0 && y < this.height && x >= 0 && x < this.width );
	}
	public bool isDefinedTile( int x, int y ){
		return this.isValidTilePosition( x,y ) && this.mapTiles[y][x].type != MapTileType.NotDefined;
	}
	public bool isUsefulPosition( int x, int y ){
		return this.isDefinedTile( x,y ) && this.mapTiles[y][x].type != MapTileType.Wall;
	}

	public static MapTileType typeIndexToType( int typeIndex )
	{
		string[] names = System.Enum.GetNames(typeof(MapTileType));
		if( typeIndex < names.Length ){
			return (MapTileType)typeIndex;
		}
		else{
			return MapTileType.NotDefined;
		}
	}
	public static int typeToTypeIndex( MapTileType type ){
		return (int)type;
	}

	/*
	 * FILE READING
	 */
	public static Map read( string content )
	{
		/*
		 * File format :
		 * Height Width
		 * Height *
		 * 	[Width * MapTileType (index)]
		*/

		// Reading Information
		try{
			string[] linesWithComments = content.Split('\n');
			List<string[]> lines = new List<string[]>();
			// filtering comments and empty lines
			for ( int i = 0; i < linesWithComments.Length; i++ ){
				string[] lineSplitSpace = linesWithComments[i].Trim().Split(' ');
				if( lineSplitSpace.Length > 0 && lineSplitSpace[0].Length > 0 && lineSplitSpace[0][0] != '#' ){
					lines.Add( lineSplitSpace );
				}
			}

			int lineIt = 0;

			// initial info
			string[] infoline = lines[lineIt];
			lineIt++;
			int height = int.Parse(infoline [0]);
			int width = int.Parse(infoline [1]);

			Map m = new Map(height, width);

			// Vertices
			for (int y = 0; y < height; y++) {
				string[] mapLine = lines[lineIt];
				lineIt++;
				for( int x = 0; x < width; x++ ){
					m.addTile( x, height-1-y, Map.typeIndexToType( int.Parse(mapLine[x]) ) );
				}
			}

			return m;
		}catch( System.Exception ex ){
			Debug.LogError ( "Error while opening the Map file : wrong format" );
			Debug.LogError( ex.Message );
			return null;
		}
	}
	public static Map readFromFile( string fileName )
	{
		string path = fileName;

		// UNCOMMENT FOR FIXED MAP FILES : WEB BUILD
		//return Map.read( Resources.Load<TextAsset>( fileName ).text );

		// Starting Reader 
		System.IO.StreamReader reader; 
		try{ 
			reader = new System.IO.StreamReader( path ); 
		} 
		catch( System.Exception ex ){ 
			Debug.LogError ( "Error while opening the Map file" ); 
			Debug.LogError( ex.Message ); 
			return null; 
		} 

		// Reading Information 
		return Map.read( reader.ReadToEnd() ); 
	}
}
