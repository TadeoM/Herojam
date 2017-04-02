using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour, IDamageable {

    // attribute
    public int startingHealth;
    public GameObject hitParticles;
    public Rigidbody rb;
    public float deathTime;


    private int currentHealth;

	void Awake()
    {
        currentHealth = startingHealth;
        Debug.Log(currentHealth);
    }
	
	/// <summary>
    /// if activated, tells you how much health the object had, removes one health, and tell you the new health
    /// </summary>
    /// <param name="damage"></param>
    /// <param name="hitPoint"></param>
	public void Damage (int damage, Vector3 hitPoint) {
        Instantiate(hitParticles, hitPoint, Quaternion.identity);
        Debug.Log(currentHealth);
        currentHealth = currentHealth - 1;
        Debug.Log("Hit for " + damage + " health, went from: " + currentHealth+damage + " to " + currentHealth);
          
        // if the health is less than zero, then it loses its kinematic property
        if (currentHealth < 1)
        {
            rb.isKinematic = false;
            // if the object has zero health
            if (currentHealth <= 0)
            {
                StartCoroutine(ShowAndHide(this.gameObject));
            }

        }
	}

    /// <summary>
    /// waits for death timer, and then deactivates the object
    /// </summary>
    /// <param name="go"></param>
    /// <returns></returns>
    IEnumerator ShowAndHide(GameObject go)
    {
        yield return new WaitForSeconds(deathTime);
        go.SetActive(false);
    }
}
