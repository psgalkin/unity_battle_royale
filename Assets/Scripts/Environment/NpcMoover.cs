using System;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class NpcMoover : MonoBehaviour
{
    NavMeshAgent _agent;
    Vector3 newPos;
    RaycastHit _hit;
    [SerializeField] Transform[] positions;

    void Start()
    {
        SetNewPos();
        _agent = GetComponent<NavMeshAgent>();
        _agent.SetDestination(newPos);
    }

    void SetNewPos()
    {
        newPos = positions[Random.Range(0, positions.Length)].position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Math.Abs(transform.position.magnitude - newPos.magnitude) < 0.3f)
        {
            SetNewPos();
            _agent.SetDestination(newPos);
        }
            
    }
}
