using UnityEngine;

[RequireComponent(typeof(PlayerInput))]
public class Player : MonoBehaviour 
{
	public void Harvest(Harvestable h)
	{
		Destroy(h.gameObject);
	}
}
