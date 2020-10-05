using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField]
    private GameObject[] elements = new GameObject[5];
    private AudioSource[] audios = new AudioSource[5];
    private Vector3[] ElementLocations = new Vector3[5];


    [SerializeField]
    private GameObject pacoca;
    private Animator animPacoca;

    [SerializeField]
    private GameObject pacocanegativo;
    private Animator pacocanegativoanim;

    [SerializeField]
    private GameObject tutorialCanvas;
    [SerializeField]
    private GameObject EndScreen;


    private int lvl = 1;
    private int stepRoute = 0;
    private bool pacocaWalk = false;

    public int QuantPlays = 0;

    /*
    private Vector3 igrejaLocal = new Vector3(-0.54f, 0.34f, 0);
    private Vector3 carpinteiroLocal = new Vector3(1.45f, -2.12f, 0);
    private Vector3 ferreiroLocal = new Vector3(3.25f, -4.04f, 0);
    private Vector3 estabuloLocal = new Vector3(-2.98f, -3.22f, 0);
    private Vector3 costuraLocal = new Vector3(-2.03f, -1.18f, 0);
    */

    private Vector3 newposition;


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

    private void Update()
    {

        if (pacocaWalk == true)
        { 
             pacoca.transform.position = Vector3.MoveTowards(pacoca.transform.position, newposition, Time.deltaTime * 3);
        }
    }

    //FECHA TUTORIAL E STARTA PRIMEIRO LVL
    public void CloseTutoAndStart()
    {
        tutorialCanvas.SetActive(false);     
        StartCoroutine(StartRound(lvl));
        pacoca.GetComponent<Animator>().applyRootMotion = true;
    }


    //INICIA O ROUND TOCANDO OS SONS
    IEnumerator StartRound(int currentLvl)
    {
        yield return new WaitForSeconds(1); //TEMPO PARA INICIAR O TURNO

        for (int i = 0; i < currentLvl; i++)
        {
            audios[i].Play();

            yield return new WaitForSeconds(2f);
        }

        EnableColliders();
    }


    public void RepeatAudio()
    {
        StartCoroutine(AudioRepeatInTime());
    }


    IEnumerator AudioRepeatInTime()
    {
        for (int i = 0; i < lvl; i++)
        {
            audios[i].Play();

            yield return new WaitForSeconds(2f);
        }
    }
    
    //CHECA RESPOSTA E ETAPA
    public void CheckAnswer(string currentClicked, Vector3 local)
    {
        QuantPlays++;


        if(stepRoute < lvl)
        {
           if (currentClicked == elements[stepRoute].name) //RESPOSTA CORRETA
            {

                ElementLocations[stepRoute] = local;

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

            DisableColliders();// DESABILITA COLISOR PARA O PACOCA ANDAR
            StartCoroutine(PacocaCaminhando()); 
        }
    }

    //SE RESPOSTA FOR INCORRETA
    IEnumerator Wronganswer()
    {
        pacocanegativoanim.SetTrigger("wrong");
        DisableColliders();
        yield return new WaitForSeconds(2.6f);
        EnableColliders();

    }

    //FINALIZA TURNO
    private void EndRound()
    {
        for (int i = 0; i < elements.Length; i++) // RESETA OS ICONES DE CIMA
        {
            elements[i].GetComponent<MouseHover>().ResetIconPositions();
        }

        lvl++; // ADD 1 A PROX FASE

        if(lvl <= 5) // SE FIZER 5 LVLS ACABA O JOGO
        {
            StartCoroutine(StartRound(lvl));
        }
        else
        {
            Debug.Log("acaba o jogo, e a quant plays foi: " + QuantPlays); // JOGO ACABA
            EndScreen.SetActive(true);

        }
    }

    // PACOCA ANDA
    IEnumerator PacocaCaminhando() 
    {
        for(int i = 0; i < lvl; i++)
        {
            if(newposition.x > ElementLocations[i].x)
            {
                pacoca.GetComponent<SpriteRenderer>().flipX = true;
            }
            else
            {
                pacoca.GetComponent<SpriteRenderer>().flipX = false;
            }

            newposition = ElementLocations[i];


            animPacoca.SetBool("walk", true);

            pacocaWalk = true;
            yield return new WaitForSeconds(2);
            animPacoca.SetBool("walk", false);
        }

 
        yield return new WaitForSeconds(2); // TEMPO DE ESPERA PARA PROX TURNO

        pacocaWalk = false;
        

        EndRound(); // VAI PARA O FIM DO TURNO
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

        animPacoca = pacoca.GetComponent<Animator>();

        pacocanegativoanim = pacocanegativo.GetComponent<Animator>();
    }

}
