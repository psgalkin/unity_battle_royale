using UnityEngine;
using UnityEngine.AI;

public class Jumper : MonoBehaviour
{
    [SerializeField] private float _jumpForce;

    private TriggerDetector _jumpDetector;
    private Rigidbody _rb;
    private NavMeshAgent _agent;

    private void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _rb = GetComponent<Rigidbody>();
        _jumpDetector = GetComponentInChildren<TriggerDetector>();
    }

    public void Jump() 
    {
        Vector3 rotation = _agent.velocity.normalized;
        _agent.enabled = false;
        _rb.isKinematic = false;

        if (_jumpDetector.IsInTrigger)
        {              
            _rb.AddForce(new Vector3(rotation.x, 1, rotation.z) * _jumpForce, ForceMode.Force);
        }
    }

    public void OnCollisionEnter(Collision collision)
    {
        _agent.enabled = true;
        _rb.isKinematic = true;
    }
}
