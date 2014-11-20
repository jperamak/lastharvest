using Assets.Scripts;
using Assets.Scripts.Helpers;
using UnityEngine;

public class Hazard : MonoBehaviour 
{
	public void OnTriggerEnter2D(Collider2D other)
	{
	    if (other.gameObject.CompareTag(Tags.Player))
	    {
	        other.GetComponent<Player>().Die();
	    }

	}
}
