using Assets.Scripts;
using UnityEngine;
using System.Collections;
using Assets.Scripts.Helpers;

[RequireComponent(typeof(CharacterController2D))]
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

    [SerializeField]
    private GrapplingHook _hookPrefab;
    public GrapplingHook HookPrefab { get { return _hookPrefab; } }

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

	public void Awake()
	{
		//_animator = GetComponent<Animator>();
		_controller = GetComponent<CharacterController2D>();
	    _mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
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

		if (Input.GetKey(KeyCode.Space) && _isGrappling )
	    {
			DetachGrappling();				
	    }		

		if (_isGrappling)
	    {
            var distance = _grapplingPoint.position - transform.position;
	        if (Mathf.Abs(distance.x) < 2f && Mathf.Abs(distance.y) < 2f)
	        {
                _controller.velocity = Vector3.zero;
                return;
	        }
	        distance.z = 0;
            distance.Normalize();

	        distance *= grapplingSpeed * Time.deltaTime;
            _controller.move(new Vector3(distance.x, distance.y));
	        return;
	    }

		// grab our current _velocity to use as a base for all calculations
		_velocity = _controller.velocity;

		if( _controller.isGrounded )
			_velocity.y = 0;

        if (InputHelpers.IsAnyKey(KeyCode.RightArrow, KeyCode.D) && (!disableMovementInAir || (disableMovementInAir && _controller.isGrounded)))
		{

			normalizedHorizontalSpeed = 1;
			if( transform.localScale.x < 0f )
				transform.localScale = new Vector3( -transform.localScale.x, transform.localScale.y, transform.localScale.z );

            //if( _controller.isGrounded )
            //    _animator.Play( Animator.StringToHash( "Run" ) );
		}
        else if (InputHelpers.IsAnyKey(KeyCode.LeftArrow, KeyCode.A) && (!disableMovementInAir || (disableMovementInAir && _controller.isGrounded)))
		{
			normalizedHorizontalSpeed = -1;
			if( transform.localScale.x > 0f )
				transform.localScale = new Vector3( -transform.localScale.x, transform.localScale.y, transform.localScale.z );

            //if( _controller.isGrounded )
            //    _animator.Play( Animator.StringToHash( "Run" ) );
		}
		else
		{
			normalizedHorizontalSpeed = 0;

            //if( _controller.isGrounded )
            //    _animator.Play( Animator.StringToHash( "Idle" ) );
		}


		// we can only jump whilst grounded
        if (_controller.isGrounded && InputHelpers.IsAnyKey(KeyCode.UpArrow, KeyCode.W, KeyCode.Space))
		{
			_velocity.y = Mathf.Sqrt( 2f * jumpHeight * -gravity );
            //_animator.Play( Animator.StringToHash( "Jump" ) );
		}


		// apply horizontal speed smoothing it
		var smoothedMovementFactor = _controller.isGrounded ? groundDamping : inAirDamping; // how fast do we change direction?
		_velocity.x = Mathf.Lerp( _velocity.x, normalizedHorizontalSpeed * runSpeed, Time.deltaTime * smoothedMovementFactor );

		// apply gravity before moving
		_velocity.y += gravity * Time.deltaTime;

		_controller.move( _velocity * Time.deltaTime );
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
        Destroy(_hook.gameObject);
        _isGrappling = false;
        _hook = null;
    }
}

