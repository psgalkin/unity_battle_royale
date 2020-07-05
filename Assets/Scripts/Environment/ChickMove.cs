using UnityEngine;
using UnityEngine.AI;
public class ChickMove : MonoBehaviour
{
    // Start is called before the first frame update
    private Animator Animator;
    private NavMeshAgent agent;
    public NavMeshAgent Agent => agent;

    private void Awake()
    {
        Animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        var magnitude = Agent.velocity.magnitude;
        if (magnitude > 0.01f) Animator.SetBool("Run", true);
        else Animator.SetBool("Run", false);
    }
}
