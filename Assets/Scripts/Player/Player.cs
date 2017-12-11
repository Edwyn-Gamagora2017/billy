using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player {
	int id;
	int score;

	public int Id {
		get {
			return id;
		}
	}

	public int Score {
		get {
			return score;
		}
	}
	public void addScore( int value ){
		this.score += value;
	}

	public Player( int id ){
		this.id = id;
		this.score = 0;
	}
}
