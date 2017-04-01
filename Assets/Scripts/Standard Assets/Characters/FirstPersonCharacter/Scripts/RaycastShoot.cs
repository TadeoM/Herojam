﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastShoot : MonoBehaviour {

    // attributes
    public float fireRate = .25f;
    public float range = 50;
    public GameObject hitParticles;
    public GameObject shootFlare;
    public int damage = 1;
    public Transform gunEnd;

    private Camera fpsCam;
    private LineRenderer lineRenderer;
    private WaitForSeconds shotLength = new WaitForSeconds(0.7f);
    private AudioSource source;
    private float nextFireTime;

    void Awake()
    {
        
        lineRenderer = GetComponent<LineRenderer>();
        source = GetComponent<AudioSource>();
        fpsCam = GetComponentInParent<Camera>();
    }
	
	// Update is called once per frame
	void Update () {
        RaycastHit hit;
        Vector3 rayOrigin = fpsCam.ViewportToWorldPoint(new Vector3(.5f, .5f, 0));

        if (Input.GetButtonDown("Fire1") && Time.time > nextFireTime)
        {
            nextFireTime = Time.time + fireRate;

            if (Physics.Raycast(rayOrigin, fpsCam.transform.forward, out hit))
            {
                EnemyHealth dmgScript = hit.collider.gameObject.GetComponent<EnemyHealth>();

                if(dmgScript != null)
                {
                    dmgScript.Damage(damage, hit.point);
                }
                if (hit.rigidbody != null)
                    hit.rigidbody.AddForce(-hit.normal * 100f);

                lineRenderer.SetPosition(0, gunEnd.position);
                lineRenderer.SetPosition(1, hit.point);
                Instantiate(hitParticles, hit.point, Quaternion.identity);
            } 
        }
	}

    private IEnumerator ShotEffect()
    {
        lineRenderer.enabled = true;
        source.Play();
        //smokeParticles.Play();
        shootFlare.SetActive(true);
        yield return shotLength;
        lineRenderer.enabled = false;
        shootFlare.SetActive(false);
    }
}
