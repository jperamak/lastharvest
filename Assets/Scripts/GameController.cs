using UnityEngine;
using System.Linq;
using System.Collections.Generic;


public class GameController : MonoBehaviour {

	public static int food;
	private Player _player;
	public int currenLevel = 1;
	public List<FamilyMember> family;

	void Start ()
	{
	    _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
	    _player.Harvested += OnHarvested;
		DontDestroyOnLoad(this);
		StartFamily ();
	}

	private void OnHarvested(object sender, HarvestEventArgs args)
	{
		Debug.Log ("Harvested: " + args.Harvestable);
		if (Done ())
			NextLevel ();
	}

	private bool Done()
	{
		return ( GameObject.FindGameObjectWithTag("Harvestable") == null );
	}

	private void NextLevel()
	{
		//loading screen
		//feed family


		Application.LoadLevel (++currenLevel);
	}

	private void StartFamily()
	{	
		family = new List<FamilyMember> ();
		family.Add (new FamilyMember("Billy-Bob", 23));
		family.Add (new FamilyMember("Wifey", 19));
		family.Add (new FamilyMember("Babby1", 3));
		family.Add (new FamilyMember("Babby2", 1));
	}

	private void Feed()
	{
		foreach (FamilyMember fm in family.ToList())
		{
			food = fm.Feed(food);
			if (!fm.DidSurvive())
				family.Remove(fm);
		}
	}

}
