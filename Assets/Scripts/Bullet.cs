using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Bullet : NetworkBehaviour {

	[SyncVar]
	public Color color;

	public override void OnStartClient(){
		gameObject.GetComponent<Renderer> ().material.color = color;
	}
}
