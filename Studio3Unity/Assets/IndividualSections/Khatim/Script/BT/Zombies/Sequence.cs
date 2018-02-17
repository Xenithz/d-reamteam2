using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sequence : Node
{
    #region Functions
    public override void Execute(ZombieBT ownerBT)
    {
        for (int i = 0; i < children.Count; i++)
        {
            children[i].Execute(ownerBT);

            //Node Running
            if (children[i].currCon == Condition.Running)
            {
                currCon = Condition.Running;
                return;
            }

            //Node Failed
            if (children[i].currCon == Condition.Failed)
            {
                currCon = Condition.Failed;
                return;
            }
        }

        //Node Succeeded
        currCon = Condition.Success;
        return;

    }
    #endregion
}
