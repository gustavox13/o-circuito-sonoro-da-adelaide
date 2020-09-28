using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField]
    private GameObject[] elements = new GameObject[5];
    private AudioSource[] audios = new AudioSource[5];



    [SerializeField]
    private GameObject pacoca;
    private Animator animPacoca;

    [SerializeField]
    private GameObject tutorialCanvas;

    private int lvl = 5;

    private int stepRoute = 0;


    private void Awake()
    {
        ShuffleElements();
        GetAllComponents();
        DisableColliders();
    }

    //FECHA TUTORIAL E STARTA PRIMEIRO LVL
    public void CloseTutoAndStart()
    {
        tutorialCanvas.SetActive(false);
        

        StartCoroutine(StartRound(lvl));
    }



    IEnumerator StartRound(int currentLvl)
    {
        yield return new WaitForSeconds(1); //TEMPO PARA INICIAR O TURNO

        for (int i = 0; i < currentLvl; i++)
        {
            Debug.Log(elements[i].name);

            audios[i].Play();

            yield return new WaitForSeconds(4);
        }

        EnableColliders();
    }



    public void CheckAnswer(string currentClicked)
    {
        if(stepRoute < lvl)
        {
           if (currentClicked == elements[stepRoute].name)
            {
                Debug.Log("resposta certa");
                elements[stepRoute].GetComponent<MouseHover>().PositionMyIcon(stepRoute);

                stepRoute++;
            }
            else
            {
                Debug.Log("resposta errada");
            }

            
        }

        if(stepRoute == lvl)
        {
            Debug.Log("terminou o lvl");
        }
    }


    //DESATIVA COLIDER
    private void DisableColliders()
    {
        for (int i = 0; i < elements.Length; i++)
        {
            elements[i].gameObject.GetComponent<Collider2D>().enabled = false;
        }
    }

    //ATIVA COLIDER
    private void EnableColliders()
    {
        for (int i = 0; i < elements.Length; i++)
        {
            elements[i].gameObject.GetComponent<Collider2D>().enabled = true;
        }
    }


    // EMBARALHA OS ANIMAIS -------- 1
    private void ShuffleElements()
    {
        for (int i = 0; i < elements.Length; i++)
        {
            GameObject obj = elements[i];
            int randomizeArray = Random.Range(0, i);
            elements[i] = elements[randomizeArray];
            elements[randomizeArray] = obj;
        }
    }


    //PROCURA E PEGA TODOS OS COMPONENTES DOS ELEMENTOS ----------- 2
    private void GetAllComponents()
    {
        for (int i = 0; i < elements.Length; i++)
        {

            audios[i] = elements[i].gameObject.GetComponent<AudioSource>();

        }

    }

}
