using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fire : MonoBehaviour
{
    public Animator animator;

    private bool opener = false;

    // private bool isScoped = false; use this if we want to toggle scoped

    private void Update()
    {
        if (Input.GetButton("Fire2"))
        {
            animator.SetBool("scoped", true);
        }
        else
        {
            animator.SetBool("scoped", false);
        }

        if (Input.GetButtonDown("Fire1"))
        {
            animator.SetTrigger("fire");
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            opener = !opener;
            animator.SetBool("opener", opener);
        }
    }
}
