using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgeElement : MapElement {

	int index;
	int length;
	bool closed;
	bool verticalOrientation;

	public int Index {
		get {
			return index;
		}
	}

	public int Length {
		get {
			return length;
		}
	}

	public bool VerticalOrientation {
		get {
			return verticalOrientation;
		}
	}

	public bool Closed {
		get {
			return closed;
		}
	}

	public BridgeElement( int index, int length, bool initiallyClosed, bool verticalOrientation, Vector2 pos ):base(pos){
		this.index = index;
		this.length = length;
		this.closed = initiallyClosed;
		this.verticalOrientation = verticalOrientation;
	}
}
