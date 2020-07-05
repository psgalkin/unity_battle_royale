using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CharacterNavmeshMoving : MonoBehaviour
{
    Animator _animator;
    NavMeshAgent _agent;
    RaycastHit _hit;

    void Start()
    {
        _animator = GetComponent<Animator>();
        _agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!Input.GetMouseButton(0)) { return; }

        var position = Camera.main.ScreenPointToRay(Input.mousePosition);
        Physics.Raycast(position, out _hit);

        _agent.SetDestination(_hit.point);


        //GameObject player = new GameObject();
        //Quaternion q = player.transform.rotation;
        //Vector3 vect = q.eulerAngles;
    }
}
