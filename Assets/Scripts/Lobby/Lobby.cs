using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class Lobby : MonoBehaviourPunCallbacks
{
    public TMP_InputField InputField;
    public GameObject buttonPrefab;
    public Transform scrollListContents;
    List<GameObject> buttons = new List<GameObject>();

    void Start()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
        PhotonNetwork.ConnectUsingSettings();
        PhotonNetwork.GameVersion = "1.0";
    }

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby();
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.LogError($"Disconnected: {cause}");
    }

    public override void OnJoinedRoom()
    {
        if (PhotonNetwork.CurrentRoom.PlayerCount == 1)
        {
            PhotonNetwork.LoadLevel("GameScene");
        }
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        foreach (GameObject button in buttons) {
            Destroy(button);
        }

        buttons.Clear();
        foreach (RoomInfo room in roomList)
        {
            GameObject button = Instantiate(buttonPrefab, scrollListContents);
            button.GetComponent<LobbyButton>().Init(room);
            buttons.Add(button);
        }
    }

    public void CreateRoom()
    {
        RoomOptions options = new RoomOptions { MaxPlayers = 10 };
        PhotonNetwork.CreateRoom(InputField.text, options);
    }
}
