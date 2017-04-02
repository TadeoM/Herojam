using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastShoot : MonoBehaviour {

    // attributes
    public float fireRate = 1f;
    public float range = 50;
    public GameObject shootParticles;
    public GameObject hitParticles;
    public GameObject muzzleFlash;
    public Animator animator;
    public int damage = 1;
    public Transform gunEnd;


    private Camera fpsCam;
    private LineRenderer lineRenderer;
    private WaitForSeconds shotLength = new WaitForSeconds(0.05f);
    private int shootForce = 10;
    //private AudioSource source;
    private float nextFireTime;

    void Awake()
    {
        shootParticles.SetActive(false);
        muzzleFlash.SetActive(false);
        lineRenderer = GetComponent<LineRenderer>();
        //source = GetComponent<AudioSource>();
        fpsCam = GetComponentInParent<Camera>();
    }
	
	/// <summary>
    /// checks for left and right click, and runs code
    /// </summary>
	void Update () {
        RaycastHit hit;
        Vector3 rayOrigin = fpsCam.ViewportToWorldPoint(new Vector3(.5f, .5f, 0));

        if (Input.GetButton("Fire2"))
        {
            animator.SetBool("scoped", true);
        }
        else
        {
            animator.SetBool("scoped", false);
        }

        // if fire1(left click) is pressed, fire
        if (Input.GetButtonDown("Fire1") && Time.time >= nextFireTime)
        {
            shootParticles.SetActive(false);
            nextFireTime = Time.time + fireRate;
            shootParticles.SetActive(true);

            //if the raycast of the weapon firing hits a target
            if (Physics.Raycast(rayOrigin, fpsCam.transform.forward, out hit))
            {

                animator.SetTrigger("fire");
                EnemyHealth dmgScript = hit.collider.gameObject.GetComponent<EnemyHealth>();

                // if it hits an enemy, damage it
                if(dmgScript != null)
                {
                    dmgScript.Damage(damage, hit.point);
                }
                if (hit.rigidbody != null)
                    hit.rigidbody.AddForce(-hit.normal * 100f);


                lineRenderer.SetPosition(0, gunEnd.position);
                lineRenderer.SetPosition(1, hit.point);
                //Instantiate(hitParticles, hit.point, Quaternion.identity);
            }
            StartCoroutine(ShotEffect());
        }
	}

    /// <summary>
    /// activates the line renderer,
    /// activates the muzzle flash animation
    /// </summary>
    /// <returns></returns>
    private IEnumerator ShotEffect()
    {
        lineRenderer.enabled = true;
        //source.Play();
        muzzleFlash.SetActive(true);
        yield return shotLength;
        lineRenderer.enabled = false;
        muzzleFlash.SetActive(false);

    }
}
