using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(Animator))]
public class PlayerController : MonoBehaviour
{
    [Header("Input Manager")]
    [SerializeField] private InputManager _input;

    [Header("Camera")]
    [SerializeField] private Transform _cameraHolder;
    [SerializeField] private Transform _camera;

    [Header("Move Settings")]
    [SerializeField] private float _walkSpeed;
    [SerializeField] private float _runSpeed;
    [SerializeField] private float _drag;
    [SerializeField] private float _speedMultiplaer;
    [SerializeField] [Range(0f, 5f)] private float _smoothSpeedTime;

    [Header("Mouse Look Settings")]
    [SerializeField] [Range(0f, 10f)] private float _xSensitivity;
    [SerializeField] [Range(0f, 10f)] private float _ySensitivity;
    [SerializeField] private float _clampLookAxisX;
    [SerializeField] private bool _inverseX;
    [SerializeField] private bool _inverseY;

    [Header("Sprinting Settings")]
    [SerializeField] private float _stamina;
    [SerializeField] private float _staminaDrain;
    [SerializeField] private float _staminaRestore;
    [SerializeField] private float _staminaRestoreDelay;

    [Header("Jumping")]
    [SerializeField] private float _jumpForce;

    [Header("Falling")]
    [SerializeField] private float _fallingSpeed;
    [SerializeField] private float _fallingThreshold;

    [Header("Groubd Detection")]
    [SerializeField] private float _sphereRadius;
    private int _groundMask;
    private bool _isGrounded;

    private Rigidbody _rb;
    private RaycastHit _hit;

    private float _horizontalMove;
    private float _verticalMove;
    private float _speed;
    private float _targetSpeed;
    private float _targetSpeedVelocity;

    private Vector3 _moveDirectional;
    private Vector3 _slopeMoveDirection;
    private Vector3 _cameraRotation;
    private Vector3 _playerRotation;

    private float _currentStamina;
    private float _currentStaminaRestoreDelay;

    private float _playerHeight;

    private bool _isSprinting;
    private bool _isJumping;
    private bool _isFalling;


    public float HorizontalMove => _horizontalMove;
    public float VerticalMove => _verticalMove;
    public bool IsSprinting => _isSprinting;
    public bool IsJumping => _isJumping;
    public bool IsFalling => _isFalling;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        _isSprinting = false;
        _isJumping = false;
        _isFalling = false;

        _currentStamina = _stamina;
        _currentStaminaRestoreDelay = _staminaRestoreDelay;

        _playerHeight = GetComponent<CapsuleCollider>().height;

        _groundMask = LayerMask.GetMask("Ground");
    }

    private void Update()
    {
        ChechGround();
        LookUpdate();
        ControlDrag();
        Jump();
        Sprint();
        CalculateSprint();
        ProjectOnPlane();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        _speed = _walkSpeed;

        if (_isSprinting)
        {
            _speed = _runSpeed;
        }

        _horizontalMove = _input.MoveInput.x;
        _verticalMove = _input.MoveInput.y;

        _moveDirectional = _horizontalMove * transform.right + _verticalMove * transform.forward;
        _moveDirectional.Normalize();

        _targetSpeed = Mathf.SmoothDamp(_targetSpeed, _speed, ref _targetSpeedVelocity, _smoothSpeedTime);

        if (!OnSlope())
        {
            _rb.AddForce(_moveDirectional * _targetSpeed * _speedMultiplaer, ForceMode.Acceleration);
        }
        else
        {
            _rb.AddForce(_slopeMoveDirection * _targetSpeed * _speedMultiplaer, ForceMode.Acceleration);
        }
    }

    private void LookUpdate()
    {
        _cameraRotation.x += _ySensitivity * (_inverseY ? _input.ViewInput.y : -_input.ViewInput.y) * Time.deltaTime;
        _cameraRotation.x = Mathf.Clamp(_cameraRotation.x, -_clampLookAxisX, _clampLookAxisX);

        _playerRotation.y += _xSensitivity * (_inverseX ? -_input.ViewInput.x : _input.ViewInput.x) * Time.deltaTime;

        _cameraHolder.localRotation = Quaternion.Euler(_cameraRotation);
        transform.localRotation = Quaternion.Euler(_playerRotation);
    }

    private void Sprint()
    {
        if (_input.Sprint && _currentStamina > _stamina / 4f && CanSprint())
        {
            _isSprinting = true;
        }
        else
        {
            _isSprinting = false;
        }
    }

    private void Jump()
    {
        if (_input.Jump && _isGrounded && !_isJumping)
        {
            _isJumping = true;
        }
    }

    public void ApplyJumpForce()
    {
        _rb.AddForce(transform.up * _jumpForce, ForceMode.Impulse);
    }

    private void CalculateSprint()
    {
        if (_isSprinting)
        {
            if (_currentStamina > 0)
            {
                _currentStamina -= _staminaDrain * Time.deltaTime;
            }
            else
            {
                _isSprinting = false;
            }

            _currentStaminaRestoreDelay = _staminaRestoreDelay;
        }
        else
        {
            if(_currentStamina < _stamina)
            {
                if (_currentStaminaRestoreDelay <= 0)
                {
                    _currentStamina += _staminaRestore * Time.deltaTime;
                }
                else
                {
                    _currentStaminaRestoreDelay -= Time.deltaTime;
                }
            }
            else
            {
                _currentStamina = _stamina;
            }
        }
    }

    private bool CanSprint()
    {
        if (Mathf.Abs(_input.MoveInput.y) > 0.25f)
        {
            return true;
        }

        if(Mathf.Abs(_input.MoveInput.x) > 0.25f)
        {
            return true;
        }

        return false;
    }

    private void CalculateFalling()
    {
        _fallingSpeed = transform.InverseTransformDirection(_rb.velocity).y;
    }


    private void ChechGround()
    {
        _isGrounded = Physics.CheckSphere(transform.position - new Vector3(0f, 1f, 0f), _sphereRadius, _groundMask);
    }

    private void ProjectOnPlane()
    {
        _slopeMoveDirection = Vector3.ProjectOnPlane(_moveDirectional, _hit.normal);
    }

    private bool OnSlope()
    {
        if(Physics.Raycast(transform.position, Vector3.down, out _hit, _playerHeight /2f + 0.5f))
        {
            if(_hit.normal != Vector3.up)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        return false;
    }

  
    private void ControlDrag()
    {
        _rb.drag = _drag;
    }

}
