using UnityEngine;

public class GameController : MonoBehaviour {

	public static int food;
	private Player _player;

	void Start ()
	{
	    _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
	    _player.Harvested += OnHarvested;
		DontDestroyOnLoad(this);
	}

	private void OnHarvested(object sender, HarvestEventArgs args)
	{
		Debug.Log ("Harvested: " + args.Harvestable);
	}

}
