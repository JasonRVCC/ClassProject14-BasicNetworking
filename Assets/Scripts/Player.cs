using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Player : NetworkBehaviour {

	[SyncVar]
	public Color color;

	public float moveSpeed = 1.87f;

	public override void OnStartClient(){
		gameObject.GetComponent<Renderer> ().material.color = color;
	}

	void Update(){
		GetInput ();
	}

	private void GetInput(){
		float x = Input.GetAxisRaw ("Horizontal") * moveSpeed * Time.deltaTime;
		float y = Input.GetAxisRaw ("Vertical") * moveSpeed * Time.deltaTime;
		MoveIt (x, y);
	}

	private void MoveIt(float x, float y){
		transform.Translate(x,y,0);
	}
}
