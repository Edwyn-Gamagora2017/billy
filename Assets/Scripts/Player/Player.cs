using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player {
	int id;

	public int Id {
		get {
			return id;
		}
	}

	public Player( int id ){
		this.id = id;
	}
}
