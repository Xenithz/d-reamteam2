using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class settting : MonoBehaviour 
{ 
 
 public GameObject[] hearts;

 public int health;
  public int maxHealth = 100;
 public void Awake()
{
    health = maxHealth;
}
 
 public void Update()
{
	if(Input.GetKeyDown(KeyCode.P))
	    Hurt(1);
    if(Input.GetKeyDown(KeyCode.L))
        Hurt(-1);
}
 
 public void Hurt( int damage )
 {
    health = (health - damage);
    float healthPercentage = (float) health / maxHealth;
    for( int i = 0 ; i < hearts.Length ; i++ )
    {
        float ratio = (float) i / hearts.Length;
        hearts[i].SetActive( healthPercentage > ratio );
    }
 }
 }
/* 
 // Decrease the health, but make sure it doesn't fall under 0
     health = (health - damage);
     // Compute the remaining health as a percentage, 50% for example
     float healthPercentage = (float) health / maxHealth ;
     // Loop through all the gameObjects (hearts)
     for( int i = 0 ; i < hearts.Length ; i++ )
     {
         // Supposing you have 10 hearts
         // If i = 1, then ratio = 1/10 = 0.1
         // If i = 9, then ratio = 9/10 = 0.9
         float ratio = (float) i / hearts.Length;
         // Supposing i = 1, ratio = 0.1
         // Then, you enable the gameObject only if the percentage of health is greater than the ratio 
         // (So only if health percentage > 0.1 )
         hearts[i].SetActive( healthPercentage > ratio ) ;
         // OR, IN A MORE EXPLICIT WAY:
         
         /* 
         if( healthPercentage < ratio )
             hearts[i].SetActive( true ) ;
         else
             hearts[i].SetActive( false ) ;
             */
             

