using Assets.Scripts;
using UnityEngine;
using Assets.Scripts.Helpers;

[RequireComponent(typeof(CharacterController2D))]
[RequireComponent(typeof(LineRenderer))]
public class PlayerInput : MonoBehaviour
{
	// movement config
	public float gravity = -25f;
	public float runSpeed = 8f;
	public float groundDamping = 20f; // how fast do we change direction? higher means faster
	public float inAirDamping = 5f;
	public float jumpHeight = 3f;
    public float hookSpeed = 1f;
    public float grapplingSpeed = 1f;
    public bool disableMovementInAir;
    public bool constantSpeedGrappling = true;
    public float grapplingAcceleration = 2.0f;
    public Vector3 windSpeed = new Vector3(0, 0, 0);

    [Range(0.0f, 0.1f)]
    public float grapplingDamping = 0.01f;

    public Vector3 aimLaserOffset = Vector3.zero;

    [SerializeField]
    private GrapplingHook _hookPrefab;
    public GrapplingHook HookPrefab { get { return _hookPrefab; } }

    public Transform grapplingArm;

	[HideInInspector]
	private float normalizedHorizontalSpeed = 0;

	private CharacterController2D _controller;
	private Animator _animator;
	private RaycastHit2D _lastControllerColliderHit;
	private Vector3 _velocity;

    private GrapplingHook _hook;
    private bool _isGrappling;
    private Transform _grapplingPoint;

    private Camera _mainCamera;
    private LineRenderer _lineRenderer;
    private bool left = false;

    private Player _player;


	public void Awake()
	{
        _player = GetComponent<Player>();

		_animator = GetComponent<Animator>();
		_controller = GetComponent<CharacterController2D>();
	    _mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
	    _lineRenderer = GetComponent<LineRenderer>();
        _lineRenderer.SetVertexCount(2);
        _lineRenderer.material = new Material(Shader.Find("Particles/Additive"));
        _lineRenderer.SetColors(Color.black, Color.red);
        _lineRenderer.SetWidth(0.4f, 0.4f);
		// listen to some events for illustration purposes
		_controller.onControllerCollidedEvent += onControllerCollider;
		_controller.onTriggerEnterEvent += onTriggerEnterEvent;
		_controller.onTriggerExitEvent += onTriggerExitEvent;
	}


	#region Event Listeners

	void onControllerCollider( RaycastHit2D hit )
	{
		// bail out on plain old ground hits cause they arent very interesting
		if( hit.normal.y == 1f )
			return;

		// logs any collider hits if uncommented. it gets noisy so it is commented out for the demo
		//Debug.Log( "flags: " + _controller.collisionState + ", hit.normal: " + hit.normal );
	}


	void onTriggerEnterEvent( Collider2D col )
	{
		Debug.Log( "onTriggerEnterEvent: " + col.gameObject.name );
	}


	void onTriggerExitEvent( Collider2D col )
	{
		Debug.Log( "onTriggerExitEvent: " + col.gameObject.name );
	}

	#endregion


	// the Update loop contains a very simple example of moving the character around and controlling the animation
	public void Update()
	{
        if (Input.GetMouseButtonDown(0))
	    {
	        if (!_isGrappling && _hook == null)
	        {
	            var distance = (_mainCamera.ScreenToWorldPoint(Input.mousePosition) - transform.position);
	            distance.z = 0;
                ThrowGrapplingHook(distance.normalized);
	        }
	    }
	    else if (Input.GetMouseButtonUp(0))
	    {
	        if(_isGrappling)
                DetachGrappling();
            else if (_hook != null)
                _hook.Do(h => Destroy(h.gameObject));
	    }

		if (_isGrappling)
	    {
            Vector2 direction = _grapplingPoint.position - transform.position;
            _animator.Play("Fly");
	        if (constantSpeedGrappling  && Mathf.Abs(direction.x) < 2f && Mathf.Abs(direction.y) < 2f)
	        {
                _controller.velocity = Vector3.zero;
                return;
	        }

            direction.Normalize();

	        direction *= grapplingSpeed * Time.deltaTime;
	        if (constantSpeedGrappling)
	        {
                _controller.move(direction);
	        }
	        else
	        {
                _controller.velocity += (Vector3)direction * grapplingAcceleration;
                _controller.move(_controller.velocity * Time.deltaTime * (1-grapplingDamping));
	        }
            return;	        
	    }

		// grab our current _velocity to use as a base for all calculations
		_velocity = _controller.velocity + windSpeed;

		if( _controller.isGrounded )
			_velocity.y = 0;

        if (InputHelpers.IsAnyKey(KeyCode.RightArrow, KeyCode.D) && (!disableMovementInAir || (disableMovementInAir && _controller.isGrounded)))
		{
			normalizedHorizontalSpeed = 1;
            if (left)
            {
                transform.RotateAround(transform.position, Vector3.up, -180);
                left = !left;
            }

            if( _controller.isGrounded )
                _animator.Play("Run");
		}
        else if (InputHelpers.IsAnyKey(KeyCode.LeftArrow, KeyCode.A) && (!disableMovementInAir || (disableMovementInAir && _controller.isGrounded)))
		{
			normalizedHorizontalSpeed = -1;
            if (!left)
            {
                transform.RotateAround(transform.position, Vector3.up, 180);
                left = !left;
            }

            if( _controller.isGrounded )
                _animator.Play("Run");
		}
		else
		{
			normalizedHorizontalSpeed = 0;

            if( _controller.isGrounded )
                _animator.Play("Idle");
		}


		// we can only jump whilst grounded
        if (_controller.isGrounded && InputHelpers.IsAnyKey(KeyCode.UpArrow, KeyCode.W, KeyCode.Space))
		{
			_velocity.y = Mathf.Sqrt( 2f * jumpHeight * -gravity );
            _animator.Play("Jump");
            _player.jumpSound.Do(s => s.PlayEffect());
        }


		// apply horizontal speed smoothing it
		var smoothedMovementFactor = _controller.isGrounded ? groundDamping : inAirDamping; // how fast do we change direction?
		_velocity.x = Mathf.Lerp( _velocity.x, normalizedHorizontalSpeed * runSpeed, Time.deltaTime * smoothedMovementFactor );

		// apply gravity before moving
		_velocity.y += gravity * Time.deltaTime;

		_controller.move( _velocity * Time.deltaTime );
	}

    public void LateUpdate()
    {
        Vector2 aimPoint = _mainCamera.ScreenToWorldPoint(Input.mousePosition);
        DrawHookAimLine(aimPoint);
        var direction = (aimPoint - (Vector2)grapplingArm.position).normalized;
        grapplingArm.RotateAround(grapplingArm.position, Vector3.back, direction.AngleAtan() - transform.rotation.eulerAngles.y);
    }

    private void DrawHookAimLine(Vector2 mousePosition)
    {
        var direction = (mousePosition - (Vector2)transform.position).normalized;
        _lineRenderer.SetPosition(0, transform.position + aimLaserOffset);
        _lineRenderer.SetPosition(1, transform.position + ((Vector3)direction * _hookPrefab.MaxLength));
    }

    private void ThrowGrapplingHook(Vector3 direction)
    {
        _hook = (GrapplingHook)Instantiate(_hookPrefab);
        _hook.transform.position = new Vector3(transform.position.x, transform.position.y, _hook.transform.position.z);
        _hook.Velocity = new Vector3(direction.x, direction.y) * hookSpeed;
    }

    public void Grapple(Transform grapplingPoint)
    {
        _grapplingPoint = grapplingPoint;
        _isGrappling = true;
    }

    public void DetachGrappling()
    {
        if (_isGrappling && _hook != null && _hook.enabled)
        {
            _isGrappling = false;
            _hook.Do(h => Destroy(h.gameObject));
            _hook = null;
        }
    }
}

