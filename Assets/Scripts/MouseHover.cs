using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseHover : MonoBehaviour
{
    
    private Animator hoveranim;


    private void Start()
    {
        hoveranim = gameObject.GetComponent<Animator>();
    }


    private void OnMouseEnter()
    {
        hoveranim.SetBool("mousehover", true);
    }

    private void OnMouseExit()
    {
        hoveranim.SetBool("mousehover", false);
    }
}
