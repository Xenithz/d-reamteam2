using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : Node
{
    #region Functions
    public override void Execute(ZombieBT ownerBT)
    {
        if (ownerBT.zom.distanceToPlayer < ownerBT.zom.attackDistance)
        {
            currCon = Condition.Success;
            Debug.Log("Attacking");
        }
        else
        {
            currCon = Condition.Failed;
            return;
        }
    }
    #endregion
}
