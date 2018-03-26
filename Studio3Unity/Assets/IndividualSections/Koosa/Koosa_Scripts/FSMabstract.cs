using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class FSMabstract
{
 abstract public class FSMState  <zombie>
 {
  abstract public void Enter (zombie owner);
  abstract public void Execute (zombie owner);
  abstract public void Exit(zombie owner);
 }
}
 