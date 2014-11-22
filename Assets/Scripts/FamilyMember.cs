using UnityEngine;
using System.Collections;

public class FamilyMember {


	public int health;
	public int age;
	public int taikanumero = 3;
	public string name;

	public FamilyMember(string n, int a = 0)
	{
		name = n;
		health = 100;
		age = a;
	}

	public int Feed(int food)
	{
		if (food < taikanumero) {
			health -= (taikanumero - food);
			return 0;
		}
		else {
			health += 10;
			health = health < 100 ? health : 100; 
		}
		return food - taikanumero;
	}

	public bool DidSurvive()
	{
		if (health <= 0)
			return false;
		age++;
		return true;
	}

}