using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class settting : MonoBehaviour 
{ // Drag & drop the gameObjects in the inspector
 // Here, I suppose you want to have a fixed number of hearts
 // Representing the remaining health of your character
 // Make sure the gameObjects are referenced in the correct order
 // The 1st gameObject must represent the lowest value of remaining health
 // The last gameObject must represent the highest value of remaining health
 [SerializeField]
 private GameObject[] hearts;
 // The remaining amount of health
 [SerializeField]
 private int health;
 // The maximum health of the player
 [SerializeField]
 private int maxHealth = 100;
 private void Awake()
 {
     // Initializes the health to its max value
     health = maxHealth;
 }
 
 public void Update()
 {
	if(Input.GetKeyDown(KeyCode.P))
	    Hurt(10);
    if(Input.GetKeyDown(KeyCode.L))
        health=maxHealth;


	 
 }
 
 public void Hurt( int damages )
 {
     // Decrease the health, but make sure it does fall under 0
     health = Mathf.Max( health - damages, 0 );
     // Compute the remaining health as a percentage, 50% for example
     float healthPercentage = (float) health / maxHealth ;
     // Loop through all the gameObjects (hearts)
     for( int i = 0 ; i < hearts.Length ; ++i )
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
         
         if( healthPercentage < ratio )
             hearts[i].SetActive( true ) ;
         else
             hearts[i].SetActive( false ) ;
     }
 }
}
