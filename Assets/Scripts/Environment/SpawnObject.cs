using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObject : MonoBehaviour, IOnEventCallback, IPunInstantiateMagicCallback
{
    [SerializeField] private SpawnObjType _type;
    [SerializeField] private int _value;
    [SerializeField] private float _spawnTime;
    private BoxCollider _collider;
    private MeshRenderer _meshRenderer;

    const int EventCode = 100;
    public int uniqueId;

    private void Start()
    {
        _collider = GetComponent<BoxCollider>();
        _meshRenderer = GetComponentInChildren<MeshRenderer>();
    }

    public void OnPhotonInstantiate(Photon.Pun.PhotonMessageInfo info)
    {
        uniqueId = (int)info.photonView.InstantiationData[0];
        //Debug.Log("instantiated");
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
        if (photonEvent.Code == EventCode)
        {
            object[] data = (object[])photonEvent.CustomData;
            int id = (int)data[0];
            if (id == uniqueId)
            {
                //Debug.Log("Event");
                TakeLogic();
            }
        }
    }

    public SpawnObjType GetType()
    {
        return _type;
    }

    public int GetValue()
    {
        return _value;
    }

    private void TakeLogic()
    {
        _meshRenderer = GetComponentInChildren<MeshRenderer>();
        //Debug.Log($"On take, {_meshRenderer.gameObject.name}");
        _collider.enabled = false;
        _meshRenderer.enabled = false;
        StartCoroutine(Wait());
    }

    public void OnTake()
    {
        //Debug.Log("Collision");
        byte evCode = EventCode;
        object[] content = new object[] { uniqueId };
        RaiseEventOptions raiseEventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.Others };
        SendOptions sendOptions = new SendOptions { Reliability = true };
        PhotonNetwork.RaiseEvent(evCode, content, raiseEventOptions, sendOptions);

        TakeLogic();
    }

    IEnumerator Wait()
    {
        //Debug.Log("Wait coroutine");
        yield return new WaitForSeconds(_spawnTime);
        _collider.enabled = true;
        _meshRenderer.enabled = true;
    }
}
