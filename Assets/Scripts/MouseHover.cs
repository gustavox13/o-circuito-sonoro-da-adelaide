using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseHover : MonoBehaviour
{
    private string myName;

    [SerializeField]
    private Vector3 myLocal;

    private Animator hoveranim;

    [SerializeField]
    private GameObject gameController;

    [SerializeField]
    private GameObject mySymbol;

    [SerializeField]
    private GameObject[] localHUD = new GameObject[5];

    [SerializeField]
    private GameObject clickAudioObj;

    private AudioSource clickAudio;

    private void Start()
    {
        clickAudio = clickAudioObj.GetComponent<AudioSource>();

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
        clickAudio.Play();
        gameController.GetComponent<GameController>().CheckAnswer(myName, myLocal); 
    }

    public void ResetIconPositions() // RESETA A POSICAO DOS ICONES
    {
        mySymbol.transform.position = new Vector3(0,9,0);
    }

    public void PositionMyIcon(int localPosition) //POSICIONA O ICONE NO LUGAR CERTO
    {
        
        mySymbol.transform.position = localHUD[localPosition].transform.position;
    }

}
