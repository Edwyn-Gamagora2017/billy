using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleElement : MapElement {

	public enum CollectibleType{ Score, Star };
	protected static int[] scoreType = { 1, 5 };	// Score related to each type (based on index)

	CollectibleElement.CollectibleType collectibleType;
	int score;

	public CollectibleElement.CollectibleType Collectibletype {
		get {
			return collectibleType;
		}
	}

	public int Score {
		get {
			return score;
		}
	}

	public CollectibleElement( CollectibleElement.CollectibleType type, Vector2 pos ):base(pos){
		this.collectibleType = type;
		this.score = CollectibleElement.scoreType[ (int)type ];
	}
}