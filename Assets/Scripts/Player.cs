using System.Collections;
using Assets.Scripts;
using Assets.Scripts.Helpers;
using UnityEngine;
using System;

[RequireComponent(typeof(PlayerInput))]
public class Player : MonoBehaviour
{
    public SoundEffect pickItemSound;
    public SoundEffect dieSound;

	public void Harvest(Harvestable h)
	{
	    pickItemSound.Do(s => s.PlayEffect());
		Harvested.RaiseEvent (this, new HarvestEventArgs (h));
	}


	public event EventHandler<HarvestEventArgs> Harvested;

    public void Die()
    {
        StartCoroutine(RestartLevel());
    }

    private IEnumerator RestartLevel()
    {
        dieSound.Do(s => s.PlayEffect());
        
		//yield return new WaitForSeconds(1.0f);
        Application.LoadLevel(Application.loadedLevelName);
        yield return null;
    }
}

public class HarvestEventArgs : EventArgs
{
	public Harvestable Harvestable { get; private set; }

	public HarvestEventArgs(Harvestable harvestable)
	{
		Harvestable = harvestable;
	}
}
