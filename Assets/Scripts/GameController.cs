using UnityEngine;
using System.Collections;
using System;

public class GameController : MonoBehaviour {

	public static int food;
	private Player _player;

	void Start () {
		//_player.Harvest += OnHarvested;
		DontDestroyOnLoad(this);
	}

	private void OnHarvested(object sender, HarvestEventArgs args)
	{
		Debug.Log ("Harvested: " + args.Harvestable);
	}

}
