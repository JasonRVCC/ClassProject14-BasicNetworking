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
		if (isLocalPlayer && hasAuthority) {
			GetInput ();
		}
	}

	private void GetInput(){
		float x = Input.GetAxisRaw ("Horizontal") * moveSpeed * Time.deltaTime;
		float y = Input.GetAxisRaw ("Vertical") * moveSpeed * Time.deltaTime;

		if (isServer) {
			RpcMoveIt (x, y);
		} else {
			CmdMoveIt (x, y);
		}
	}

	[Command]
	public void CmdMoveIt(float x, float y){
		RpcMoveIt (x, y);
	}

	[ClientRpc]
	public void RpcMoveIt(float x, float y){
		transform.Translate(x,y,0);
	}
}
