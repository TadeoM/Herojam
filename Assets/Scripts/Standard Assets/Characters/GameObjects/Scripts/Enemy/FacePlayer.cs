using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FacePlayer : MonoBehaviour {

    public Transform player;
    public EnemyHealth health;
    

	// Use this for initialization
	void Awake () {
	}
	
	// Update is called once per frame
	void Update () {
        if ((player != null) &&  health.currentHealth >= 0)
        {
            transform.LookAt(player);
        }

	}
}
