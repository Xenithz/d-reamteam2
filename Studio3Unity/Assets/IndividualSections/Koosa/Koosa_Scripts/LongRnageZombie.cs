using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LongRnageZombie : zombie 
{
	public void Awake()
{
    speed=8;
	attackDamage=1f;
	distanceToAttack=2;
	maxSpeed=17;
	damageDelay=2;
}
}
