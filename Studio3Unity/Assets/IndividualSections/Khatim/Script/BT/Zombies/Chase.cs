using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chase : Node
{
    #region Functions
    public override void Execute(ZombieBT ownerBT)
    {
        ownerBT.zom.distanceToPlayer = Vector3.Distance(ownerBT.zom.player.position, ownerBT.transform.position);

        if (ownerBT.zom.distanceToPlayer < ownerBT.zom.chaseDistance)
        {
            currCon = Condition.Running;
            ownerBT.transform.LookAt(ownerBT.zom.player.position);
            ownerBT.transform.position = Vector3.MoveTowards(ownerBT.transform.position, ownerBT.zom.player.transform.position,
            ownerBT.zom.speed * Time.deltaTime);
            Debug.Log("Chasing");
        }
        else
        {
            currCon = Condition.Failed;
        }

        if (ownerBT.zom.distanceToPlayer< ownerBT.zom.attackDistance)
        {
            currCon = Condition.Success;
        }
    }
    #endregion
}
