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
    private float angle;
    private Vector3 axis;

    public void Start()
    {
        this.transform.localRotation.ToAngleAxis(out angle, out axis);    
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag(Tags.Player))
            other.GetComponent<PlayerInput>().windSpeed = Quaternion.AngleAxis(angle, axis) * _windSpeed;
    }
    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag(Tags.Player))
            other.GetComponent<PlayerInput>().windSpeed = Vector3.zero;
    }
}
