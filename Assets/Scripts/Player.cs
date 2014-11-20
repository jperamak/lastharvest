using System.Collections;
using Assets.Scripts;
using Assets.Scripts.Helpers;
using UnityEngine;

[RequireComponent(typeof(PlayerInput))]
public class Player : MonoBehaviour
{
    public SoundEffect pickItemSound;
    public SoundEffect dieSound;

	public void Harvest(Harvestable h)
	{
	    pickItemSound.Do(s => s.PlayEffect());
		Destroy(h.gameObject);
	}

    public void Die()
    {
        StartCoroutine(RestartLevel());
    }

    private IEnumerator RestartLevel()
    {
        dieSound.Do(s => s.PlayEffect());
        yield return new WaitForSeconds(1.0f);
        Application.LoadLevel(Application.loadedLevelName);
        yield return null;
    }
}
