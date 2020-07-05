using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;


public class LobbyButton : MonoBehaviour
{
    RoomInfo Info;
    public void Init(RoomInfo info)
    {
        Info = info;
        GetComponentInChildren<TextMeshProUGUI>().text = info.Name;
    }

    public void Join()
    {
        PhotonNetwork.JoinRoom(Info.Name);
    }
}
