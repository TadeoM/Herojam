using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour, IDamageable {

    // attribute
    public int startingHealth;
    public GameObject hitParticles;


    private int currentHealth;

	void wake()
    {
        currentHealth = startingHealth;
    }
	
	// Update is called once per frame
	public void Damage (int damage, Vector3 hitPoint) {
        Instantiate(hitParticles, hitPoint, Quaternion.identity);
        currentHealth -= damage;
        Debug.Log("Hit for " + damage + " health, current health: " + currentHealth);
          
        if (currentHealth < 0)
        {
            Defeated(); 
        }
	}

	// deactivate the object
	public void Defeated()
	{
		gameObject.SetActive (false);
	}
}
