using Assets.Scripts;
using UnityEngine;

[RequireComponent(typeof(PlayerInput))]
public class Player : MonoBehaviour
{
    public SoundEffect pickItemSound;
    public SoundEffect hurtSound;

	public void Harvest(Harvestable h)
	{
	    pickItemSound.Do(s => s.PlayEffect());
		Destroy(h.gameObject);
	}
}
