using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class MyNetworkManager : NetworkManager {

	public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId){
		GameObject playerToSpawn = (GameObject)Instantiate (playerPrefab, Vector3.zero, Quaternion.identity);
		playerToSpawn.GetComponent<Player> ().color = new Color (Random.Range (0f, 1f), Random.Range (0f, 1f), Random.Range (0f, 1f));
		NetworkServer.AddPlayerForConnection (conn, playerToSpawn, playerControllerId);
	}
}
