using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace StateSystem
{
public class FiniteStateMachine<zombie>
{
    public FSMState<zombie> currentState{get;private set;}
    public zombie owner;



public FiniteStateMachine(zombie T){
    owner=T;
    currentState = null;

} 
 
public void ChangeState(FSMState<zombie> newstate)
{
    if(currentState!=null)
    currentState.ExitState(owner);
    currentState=newstate;
    currentState.EnterState(owner);
    
}
public void Update()
{
    if(currentState!=null)
    currentState.UpdateState(owner);
    
}













abstract public class FSMState  <zombie1>
 {
  abstract public void EnterState (zombie owner);
  abstract public void UpdateState (zombie owner);
  abstract public void ExitState(zombie owner);
 }
}
}


