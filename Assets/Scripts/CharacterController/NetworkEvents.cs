using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using System;
using UnityEngine;

class NetworkEvents : MonoBehaviour, IOnEventCallback
{
    private string _id;
    private CharacterParticleSystem _particleSystem;

    private void Start()
    {
        _id = GetComponent<Character>().uniqueId;
        _particleSystem = GetComponent<CharacterParticleSystem>();
    }

    void OnEnable()
    {
        PhotonNetwork.AddCallbackTarget(this);
    }

    void OnDisable()
    {
        PhotonNetwork.RemoveCallbackTarget(this);
    }

    public void OnEvent(EventData photonEvent)
    {
        if (photonEvent.Code == (int)EventCode.ParticleGunFire)
        {
            object[] data = (object[])photonEvent.CustomData;
            string id = Convert.ToString(data[0]);
            if (id == _id)
            {
                _particleSystem.PlayGunFire();
            }
        }
        else if (photonEvent.Code == (int)EventCode.ParticleBlood)
        {
            object[] data = (object[])photonEvent.CustomData;
            string id = Convert.ToString(data[0]);
            if (id == _id)
            {
                _particleSystem.PlayBlood();
            }
        }
    }

    public void SendEvent(string id, EventCode code)
    {
        byte eventCode = (byte)code;
        object[] content = new object[] { id };
        RaiseEventOptions raiseEventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.Others };
        SendOptions sendOptions = new SendOptions { Reliability = true };
        PhotonNetwork.RaiseEvent(eventCode, content, raiseEventOptions, sendOptions);
    }
}

