using UnityEngine;
using System.Collections;

public class Hazard : MonoBehaviour {


	void OnTriggerEnter2D(Collider2D other)
	{
		
		if (other.gameObject.CompareTag ("Player"))
						Application.LoadLevel (Application.loadedLevelName);

	}
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
