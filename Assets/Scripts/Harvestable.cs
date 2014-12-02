using Assets.Scripts.Helpers;
using UnityEngine;

public class Harvestable : MonoBehaviour 
{
    public void Awake()
    {
        tag = Tags.Harvestable;
    }

	public void OnTriggerEnter2D(Collider2D other)
	{
        if (other.gameObject.CompareTag(Tags.Player))
        {
            other.GetComponent<Player>().Harvest(this);
        }
	}
}
