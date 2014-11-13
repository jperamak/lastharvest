using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Harvest(Harvestable h)
	{
		GameObject.Destroy (h.gameObject);
	}

}
