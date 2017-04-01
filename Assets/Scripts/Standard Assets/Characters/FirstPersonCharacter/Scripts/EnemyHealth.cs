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
	
	// Update is called once per frame
	public void Damage (int damage, Vector3 hitPoint) {
        Instantiate(hitParticles, hitPoint, Quaternion.identity);
        Debug.Log(currentHealth);
        currentHealth = currentHealth - 1;
        Debug.Log("Hit for " + damage + " health, went from: " + currentHealth+damage + " to " + currentHealth);
          
        if (currentHealth < 1)
        {
            rb.isKinematic = false;
            if (currentHealth <= 0)
            {
                StartCoroutine(ShowAndHide(this.gameObject));
            }

        }
	}

    IEnumerator ShowAndHide(GameObject go)
    {
        go.SetActive(true);
        yield return new WaitForSeconds(deathTime);
        go.SetActive(false);
    }
}
