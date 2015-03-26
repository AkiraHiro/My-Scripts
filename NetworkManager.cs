using UnityEngine;
using System.Collections.Generic;

public class NetworkManager : MonoBehaviour {
	
	public GameObject standbyCamera;
	SpawnSpot[] spawnSpots;

	public bool offlineMode = false;

	bool connecting = false;

	List<string> chatMessages;
	int MaxChatMessages = 5;

	// Use this for initialization
	void Start () {
		spawnSpots = GameObject.FindObjectsOfType<SpawnSpot>();
		PhotonNetwork.player.name = PlayerPrefs.GetString("Username", "Guest");
		chatMessages = new List<string>();
	}

	void OnDestroy() {
		PlayerPrefs.SetString ("Username", PhotonNetwork.player.name);
	}


	public void AddChatMessage(string m) {
		GetComponent<PhotonView>().RPC ("AddChatMessage_RPC", PhotonTargets.All, m);
	}
	[RPC]
	void AddChatMessage_RPC(string m) {
		while(chatMessages.Count >= MaxChatMessages) {
			chatMessages.RemoveAt(0);
		}
		chatMessages.Add(m);
	}

	void Connect() {
		PhotonNetwork.ConnectUsingSettings( "NanHQHS v2.0.0" );
		}

	void OnGUI() {
		GUILayout.Label( PhotonNetwork.connectionStateDetailed.ToString () );

		if(PhotonNetwork.connected == false && connecting == false ) {
			GUILayout.BeginArea( new Rect(0, 0, Screen.width, Screen.height) );
			GUILayout.BeginHorizontal();
			GUILayout.FlexibleSpace();
			GUILayout.BeginVertical();
			GUILayout.FlexibleSpace();

			GUILayout.BeginHorizontal();
			GUILayout.Label("Username: ");
			PhotonNetwork.player.name = GUILayout.TextField(PhotonNetwork.player.name);
			GUILayout.EndHorizontal();


			if( GUILayout.Button ("Single Player") ) {
				connecting = true;
				PhotonNetwork.offlineMode = true;
				OnJoinedLobby();
            }

			if( GUILayout.Button("Multiplayer") ) {
				connecting = true;
				Connect ();
			}
			GUILayout.FlexibleSpace();
			GUILayout.EndVertical();
			GUILayout.FlexibleSpace();
			GUILayout.EndHorizontal();
			GUILayout.EndArea();
		}

		if(PhotonNetwork.connected == true && connecting == false) {
			GUILayout.BeginArea( new Rect(0, 0, Screen.width, Screen.height) );
			GUILayout.BeginVertical();
			GUILayout.FlexibleSpace();

			foreach(string msg in chatMessages) {
				GUILayout.Label(msg);
			}

			GUILayout.EndVertical();
			GUILayout.EndArea();

    	}

	}

	void OnJoinedLobby() {
		PhotonNetwork.JoinRandomRoom();
		Debug.Log ("OnJoinedLobby");
	}

	void OnPhotonRandomJoinFailed() {
		PhotonNetwork.CreateRoom( null );
		Debug.Log ("OnPhotonRandomJoinFailed");
	}

	void OnJoinedRoom() {
		Debug.Log ("OnJoinedRoom");

		connecting = false;
		SpawnMyPlayer();
		}
		
		void SpawnMyPlayer() {
		AddChatMessage(PhotonNetwork.player.name + " has joined.");

			if (spawnSpots == null) {
			     Debug.LogError ("WTF?!?!?");
				 return;
			}
			
			SpawnSpot mySpawnSpot = spawnSpots [Random.Range (0, spawnSpots.Length)];
			GameObject myPlayerGO = (GameObject)PhotonNetwork.Instantiate ("PlayerController", mySpawnSpot.transform.position, mySpawnSpot.transform.rotation, 0);
			standbyCamera.SetActive (false);
			//((MonoBehaviour)myPlayerGO.GetComponent ("PlayerShooting")).enabled = true;
		    //((MonoBehaviour)myPlayerGO.GetComponent ("Cursor")).enabled = true;
	    	((MonoBehaviour)myPlayerGO.GetComponent ("TPC")).enabled = true;
	        //((MonoBehaviour)myPlayerGO.GetComponent ("CrosshairScript")).enabled = true;
			((MonoBehaviour)myPlayerGO.GetComponent ("PlayerMovement")).enabled = true;
			((MonoBehaviour)myPlayerGO.GetComponent ("MouseLook")).enabled = true;
		    myPlayerGO.transform.FindChild ("Main Camera").gameObject.SetActive (true);
		    myPlayerGO.transform.FindChild ("TP_Camera").gameObject.SetActive (true);
	}
}