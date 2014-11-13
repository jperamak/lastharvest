using UnityEngine;
using System.Collections;

public class Harvestable : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D other)
	{

		if (other.gameObject.CompareTag("Player"))
			GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().Harvest(this);
	}


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
