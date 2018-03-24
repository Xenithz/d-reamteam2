using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrongZombie : zombie {
public void Awake()
	{
	speed=5;
	attackDamage=2f;
	distanceToAttack=1;
	maxSpeed=10;
	damageDelay=2;
	}
}
