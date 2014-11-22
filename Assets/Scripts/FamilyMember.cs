using UnityEngine;
using System.Collections;

public class FamilyMember : MonoBehaviour {

	public float health;
	public float age;
	public float taikanumero = 007f;

	public FamilyMember()
	{
		health = 100f;
		age = 0f;
	}

	public bool Eat(int food)
	{
		if (food < taikanumero) {
			health -= (taikanumero - food);
			if (health < 0)
				return false;
		}
		else {
			health += 10;
			health = health < 100 ? health : 100; 
		}
		age++;
		return true;
	}

}