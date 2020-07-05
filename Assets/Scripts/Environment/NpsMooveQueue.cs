using System;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class NpsMooveQueue : MonoBehaviour
{
    NavMeshAgent _agent;
    Vector3 newPos;
    RaycastHit _hit;
    [SerializeField] Transform[] positions;
    int currentPositionNum = 0;
    bool moveForvard = true;

    void Start()
    {
        SetNewPos();
        _agent = GetComponent<NavMeshAgent>();
        _agent.SetDestination(newPos);
    }

    void SetNewPos()
    {
        if (moveForvard && currentPositionNum < positions.Length)
        {
            currentPositionNum++;
            if (currentPositionNum == positions.Length - 1)
            { moveForvard = false; }
        }
        else if (!moveForvard && currentPositionNum >= 0)
        {
            currentPositionNum--;
            if (currentPositionNum == 0)
            { moveForvard = true; }
        }


        newPos = positions[currentPositionNum].position;
        
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
