using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Creature
{
    private int HealthP1;
    private int HealthP2;
    private int zomDamage;
    public virtual void PlayerBaseHealth()
    {
        HealthP1 = 0;
        HealthP2 = 0;
    }

    public virtual void ZombieDamage()
    {
        zomDamage = 0;
    }
}
