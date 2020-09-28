using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseHover : MonoBehaviour
{
    private string myName;

    private Animator hoveranim;

    [SerializeField]
    private GameObject gameController;

    [SerializeField]
    private GameObject mySymbol;

    [SerializeField]
    private GameObject[] localHUD = new GameObject[5];


    private void Start()
    {
        myName = gameObject.name;
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

    private void OnMouseUpAsButton()
    {
        gameController.GetComponent<GameController>().CheckAnswer(myName); 
    }

    public void PositionMyIcon(int localPosition)
    {
        Debug.Log("entrou no position my icon");
        mySymbol.transform.position = localHUD[localPosition].transform.position;
    }

}
