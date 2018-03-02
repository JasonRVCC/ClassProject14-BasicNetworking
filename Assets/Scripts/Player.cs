﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class Player : NetworkBehaviour {

	[SyncVar]
	public Color color;
	[SyncVar]
	public int score;

	public float moveSpeed = 1.87f;
	public GameObject bulletPrefab;

	private Text scoreText;

	public override void OnStartClient(){
		gameObject.GetComponent<Renderer> ().material.color = color;
		scoreText = GameObject.Find ("ScoreText").GetComponent<Text> ();
	}

	void Update(){
		if (isLocalPlayer && hasAuthority) {
			GetInput ();
			scoreText.text = "Score: " + score;
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

		if (Input.GetButtonUp ("Fire1")) {
			CmdDoFire ();
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

	[Command]
	public void CmdDoFire(){
		GameObject bullet = (GameObject)Instantiate (bulletPrefab, this.transform.position + this.transform.right, Quaternion.identity);
		bullet.GetComponent<Rigidbody> ().velocity = Vector3.forward * 17.5f;
		bullet.GetComponent<Bullet> ().color = color;
		bullet.GetComponent<Bullet> ().parentNetId = this.netId;
		Destroy (bullet, 0.875f);
		NetworkServer.Spawn (bullet);
	}
}
