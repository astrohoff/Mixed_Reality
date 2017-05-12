using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Launcher : Photon.PunBehaviour {
	private string gameVersion = "1";
	private byte maxPlayers = 2;

	private void Awake(){
		PhotonNetwork.autoJoinLobby = false;
		PhotonNetwork.automaticallySyncScene = true;
	}

	// Use this for initialization
	void Start () {
		Connect ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Connect(){
		if (PhotonNetwork.connected) {
			PhotonNetwork.JoinRandomRoom ();
		} else {
			PhotonNetwork.ConnectUsingSettings (gameVersion);
		}
	}

	public override void OnConnectedToMaster ()
	{
		PhotonNetwork.JoinRandomRoom ();
	}

	public override void OnPhotonRandomJoinFailed (object[] codeAndMsg)
	{
		PhotonNetwork.CreateRoom (null, new RoomOptions (){ MaxPlayers = maxPlayers }, null);
	}

	public override void OnJoinedRoom ()
	{
		Debug.Log ("You have joined a room.");
	}

	public override void OnPhotonPlayerConnected (PhotonPlayer newPlayer)
	{
		Debug.Log ("Another player has entered.");
	}
}
