using Assets.Scripts;
using Assets.Scripts.Helpers;
using UnityEngine;

public class Harvestable : MonoBehaviour {

	public void OnTriggerEnter2D(Collider2D other)
	{
        if (other.gameObject.CompareTag(Tags.Player))
        {
            other.GetComponent<Player>().Harvest(this);
        }
	}
}
