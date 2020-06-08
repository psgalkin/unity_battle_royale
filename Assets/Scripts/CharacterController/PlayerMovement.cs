using UnityEngine;
using UnityEngine.AI;

public sealed class PlayerMovement : MonoBehaviour
{
    private const string HORIZONTAL = "Horizontal";
    private const string VERTICAL = "Vertical";
    [Range(0.0f, 1.0f), SerializeField] private float _moveSpeed;
    private Vector3 _movement;
    private PlayerAnimation _playerAnimation;
    private NavMeshAgent _agent;
    private Camera _camera;
    private Jumper _jumper;
    private Rigidbody _rb;
    
    private bool siting = false;
    private bool lying = false;
    private bool rotating = false;
    private bool jumping = false;

    private float _standingSpeed;
    private float _sittingSpeed;
    private float _lyingSpeed;

    private Vector3 currentFacing;
    private Vector3 lastFacing;
    private float currentAngularVelocity;


    void Start()
    {
        _camera = Camera.main;
        _agent = GetComponent<NavMeshAgent>();
        _jumper = GetComponent<Jumper>();
        _rb = GetComponent<Rigidbody>();
        _playerAnimation = GetComponent<PlayerAnimation>();
        FindObjectOfType<CameraController>().SetTarget(transform);
        _standingSpeed = _agent.speed;
        _sittingSpeed = _standingSpeed - 1.0f;
        _lyingSpeed = _standingSpeed - 2.5f;
    }

    void Update()
    {
        var timeDelta = Time.deltaTime;
        var horizontal = Input.GetAxis(HORIZONTAL);
        var vertical = Input.GetAxis(VERTICAL);

        Jumping();
        Sitting();
        Lying();
        

        if (Input.GetMouseButtonDown(0))
        {
            _playerAnimation.OnFireEnable();
        }

        if (Input.GetMouseButtonUp(0))
        {
            _playerAnimation.OnFireDisable();
        }

        _movement.Set(horizontal, 0.0f, vertical);

        if (Mathf.Abs(_movement.magnitude) > 1.0f)
        {
            _movement.Normalize();
        }

        Vector3 targetDirection = _camera.transform.TransformDirection(_movement);
        targetDirection.y = 0.0f;

        transform.localEulerAngles = new Vector3(transform.localEulerAngles.x,
            _camera.transform.localEulerAngles.y, transform.localEulerAngles.z);

        _agent.Move(targetDirection * (timeDelta * _moveSpeed));
        _agent.SetDestination(transform.position + targetDirection);

        _playerAnimation.SetMove(_movement);

        Rotation();
    }

    //private void OnAnimatorMove()
    //{
    //    if (_agent.velocity.magnitude > 0)
    //    {
    //        //_playerAnimation.Animator.speed = _agent.velocity.magnitude;
    //    }
    //}

    private void Jumping()
    {

        if(Input.GetKeyDown(KeyCode.Space))
        {
             jumping = true;
            rotating = false;
            _jumper.Jump();
            _playerAnimation.OnJumpUpEnable();
        }


        else if (_rb.velocity.y < 0 && jumping)
        {
            _playerAnimation.OnJumpUpDisable();
            _playerAnimation.OnJumpDownEnable();
        }


        else if (_rb.velocity.y == 0 && jumping)
        {
            jumping = false;
            if (siting)
            {
                _playerAnimation.OnJumpDownDisable();
                _playerAnimation.SitEnable();
            }
            else if (lying)
            {
                _playerAnimation.OnJumpDownDisable();
                _playerAnimation.OnLieEnable();
            }
            else
            {
                _playerAnimation.OnJumpDownDisable();
            }
            
        }
    }

    private void Sitting()
        {
        if (Input.GetKeyDown(KeyCode.C))
        {
            lying = false;
            rotating = false;
            _playerAnimation.SetLieBool(false);
            if (siting)
            {
                _agent.speed = _standingSpeed;
                _playerAnimation.SitDisable();
            }
            else
            {
                _agent.speed = _sittingSpeed;
                _playerAnimation.SitEnable();
            }
            siting = !siting;
        }
    }

    private void Lying()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            siting = false;
            rotating = false;

            if (lying)
            {
                _agent.speed = _standingSpeed;
                _playerAnimation.OnLieDisable();
                _playerAnimation.SetLieBool(false);
            }
            else
            {
                _agent.speed = _lyingSpeed;
                _playerAnimation.OnLieEnable();
                _playerAnimation.SetLieBool(true);
            }
            lying = !lying;
        }
    }

    private void Rotation()
    {
        currentFacing = transform.forward;
        currentAngularVelocity = Vector3.Angle(currentFacing, lastFacing); //degrees per second
        lastFacing = currentFacing;

        if (currentAngularVelocity > 1 && _agent.velocity.magnitude < 0.1f)
        {
            if (!rotating)
            {
                _playerAnimation.OnRotationEnable(currentAngularVelocity);
            }
            rotating = true;
        }
        else if (rotating)
        {
            _playerAnimation.OnRotationDisable();
            rotating = false;
        }
    }
}
