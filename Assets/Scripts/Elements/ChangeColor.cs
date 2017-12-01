using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeColor : MonoBehaviour {

	MeshRenderer mRenderer;

	Color originalColor;
	public Color hitColor = Color.green;

	// Use this for initialization
	void Start () {
		mRenderer = GetComponent<MeshRenderer> ();
		originalColor = mRenderer.material.color;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void change(){
		mRenderer.material.color = hitColor;
		Invoke ("backToColor", 1);
	}

	void backToColor(){
		mRenderer.material.color = originalColor;
	}
}
