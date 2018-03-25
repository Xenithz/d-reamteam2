using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class NormalZombie : zombie
 {
	public void Awake()
	{
    speed=10;
	attackDamage=0.5f;
	distanceToAttack=1;
	maxSpeed=20;
	damageDelay=2;

	}
}

