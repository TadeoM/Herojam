using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door_Script : MonoBehaviour {

    // Use this for initialization
    public Animator animator;
    private bool opener = true;
	void Awake()
    {
    }
	
	// Update is called once per frame
	void Update () {
        PressedButton();

    }
    void PressedButton()
    {
        if (Input.GetButton("Fire1") == true)
        {
            opener = false;
            animator.SetBool("boolOpen", opener);
        }
    }
}
