using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CharacterController2D))]
public class PlayerInput : MonoBehaviour
{

    private CharacterController2D _characterController2D;

	public void Start ()
	{
	    _characterController2D = GetComponent<CharacterController2D>();
	}
	
	public void FixedUpdate () 
    {
	    if (Input.GetKey(KeyCode.LeftArrow))
	    {
	        _characterController2D.move(new Vector3(-0.4f, 0));
	    }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            _characterController2D.move(new Vector3(0, 0.4f));
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            _characterController2D.move(new Vector3(0.4f, 0));
        }
	}
}
