using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FiniteStateMachine : MonoBehaviour {

public class Condition
{
    public virtual bool Test()
    {
        return false;
    }
}

public class Transition
{
    public Condition condition;
	 public State target;    
}

public class State : MonoBehaviour
{
    public List<Transition> transitions; 
}

 public virtual void Awake()
{
   // transitions = new List<Transition>();
    




}























	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
