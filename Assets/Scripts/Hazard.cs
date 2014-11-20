using Assets.Scripts;
using UnityEngine;

public class Hazard : MonoBehaviour 
{
	public void OnTriggerEnter2D(Collider2D other)
	{
	    if (other.gameObject.CompareTag(Tags.Player))
	    {
	        Application.LoadLevel (Application.loadedLevelName);
	    }

	}
}
