using UnityEngine;
using System.Collections;
using Assets.Scripts;
using Assets.Scripts.Helpers;


public class Wind : MonoBehaviour {

    [SerializeField]
    private Vector3 _windSpeed;
    public Vector3 windSpeed 
    {
        get {return _windSpeed;}
        set {_windSpeed = value;}
    }
    [SerializeField]
    private float _periodLength;
    public float periodLength
    {
        get { return _periodLength; }
        set { _periodLength = value; }
    }

    [SerializeField]
    private float _gustLength;
    public float gustLength
    {
        get { return _gustLength; }
        set { _gustLength = value; }
    }


    private bool _blows = false;
    private bool _waits = false;
    private float angle;
    private Vector3 axis;

    private Animator _animator;

    public void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void Start()
    {
        this.transform.localRotation.ToAngleAxis(out angle, out axis);    
    }

    public void OnTriggerStay2D(Collider2D other)
    {
        if (_blows && other.gameObject.CompareTag(Tags.Player))
            other.GetComponent<PlayerInput>().windSpeed = Quaternion.AngleAxis(angle, axis) * _windSpeed;
        else if (!_blows && other.gameObject.CompareTag(Tags.Player))
            other.GetComponent<PlayerInput>().windSpeed = Vector3.zero;

    }
    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag(Tags.Player))
            other.GetComponent<PlayerInput>().windSpeed = Vector3.zero;
    }

    public void Update()
    {
        if (!_waits)
        {
            _waits = true;
            StartCoroutine(WindBlows());
        }
    }

    IEnumerator WindBlows()
    {
        _blows = true;
        //_animator.Play("windy");

        yield return new WaitForSeconds(_gustLength);
        _blows = false;
        //_aniator.Play("calm");
        yield return new WaitForSeconds(_periodLength);
        _waits = false;       
    }
}
