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
    private GameObject pacocanegativo;

    private Animator pacocanegativoanim;

    [SerializeField]
    private GameObject tutorialCanvas;

    private int lvl = 1;

    private int stepRoute = 0;


    private void Awake()
    {
        ShuffleElements();
        GetAllComponents();
        DisableColliders();
    }

    private void Start() // apenas mostra o percurso
    {
        for (int i = 0; i < elements.Length; i++)
        {
            Debug.Log(elements[i].name);
        }
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
            audios[i].Play();

            yield return new WaitForSeconds(4f);
        }

        EnableColliders();
    }



    public void CheckAnswer(string currentClicked)
    {

        if(stepRoute < lvl)
        {
           if (currentClicked == elements[stepRoute].name) //RESPOSTA CORRETA
            {
                elements[stepRoute].GetComponent<MouseHover>().PositionMyIcon(stepRoute);
                stepRoute++;
            }
            else // RESPOSTA ERRADA
            {
                StartCoroutine(Wronganswer());
                
            }
 
        }

        if(stepRoute == lvl) // FIM DO TURNO
        {
            stepRoute = 0;
            StartCoroutine(EndRound());
        }
    }

    IEnumerator Wronganswer()
    {
        pacocanegativoanim.SetTrigger("wrong");
        DisableColliders();
        yield return new WaitForSeconds(2.6f);
        EnableColliders();

    }

    IEnumerator EndRound()
    {
        DisableColliders();

        yield return new WaitForSeconds(1);

        Debug.Log("o pacoca anda aqui"); // PACOCA ANDA AQUI

        yield return new WaitForSeconds(1); // TEMPO DO PACOCA ANDAR

        for (int i = 0; i < elements.Length; i++)
        {
            elements[i].GetComponent<MouseHover>().ResetIconPositions();
        }

        lvl++;

        if(lvl <= 5)
        {
            StartCoroutine(StartRound(lvl));
        }
        else
        {
            Debug.Log("acaba o jogo"); // JOGO ACABA
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


        pacocanegativoanim = pacocanegativo.GetComponent<Animator>();
    }

}
