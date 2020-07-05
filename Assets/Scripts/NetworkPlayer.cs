using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkPlayer : MonoBehaviour
{
    [SerializeField] private  PhotonView _view;
    private PlayerAnimation _animation;
    private Vector3 _oldTransform;

    void Start()
    {
        _view = GetComponent<PhotonView>();
        _animation = GetComponent<PlayerAnimation>();
        _oldTransform = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (_view.IsMine)
        {
            if ((transform.position - _oldTransform).magnitude > 0.01f)
            {
                _animation.SetMove(transform.position - _oldTransform);
                _oldTransform = transform.position;
            }
        }
    }
}
