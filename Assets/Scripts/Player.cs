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
    public SoundEffect jumpSound;

	public void Harvest(Harvestable h)
	{
	    if (h.pickUpSound == null)
	    {
            pickItemSound.Do(s => s.PlayEffect());
	    }
	    else
	    {
	        h.pickUpSound.PlayEffect();
	    }
	    
		Harvested.RaiseEvent (this, new HarvestEventArgs (h));
		//Destroy (h.gameObject);
	}

    public void Awake()
    {
        jumpSound = (SoundEffect)Instantiate(jumpSound);
        jumpSound.transform.SetParent(this.transform);
    
        pickItemSound = (SoundEffect)Instantiate(pickItemSound);
        pickItemSound.transform.SetParent(this.transform);
        dieSound = (SoundEffect)Instantiate(dieSound);
       // dieSound.transform.SetParent(this.transform);
    }


	public event EventHandler<HarvestEventArgs> Harvested;
    public event EventHandler Died;

    public void Die()
    {
        GetComponent<PlayerInput>().DetachGrappling();
        RestartLevel();
    }

    private void RestartLevel()
    {
        dieSound.Do(s => s.PlayEffect());
        
        Died.RaiseEvent(this, new EventArgs());
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
