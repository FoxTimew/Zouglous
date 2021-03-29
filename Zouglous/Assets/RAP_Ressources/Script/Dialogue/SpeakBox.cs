using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class SpeakBox : MonoBehaviour
{
    [TextArea(1,10)]
    public string[] sentences;  // La liste du dialogue de démarage

    public Text Dialoguetext;  // Attribuer éléments visuelles

    public Text NPCNametext;  // Attribuer éléments visuelles

    public Text Response1;   // Attribuer éléments visuelles

    public Text Response2;  // Attribuer éléments visuelles

    private int index;      // Nombre de lettre dans la liste

    private int index2;      // Nombre de lettre dans la liste

    private int index3;     // Nombre de lettre dans la liste

    public float Letterspeed;    // La vitesse d'affichage lettre

    public GameObject NameBox;   // Attribuer éléments visuelles

    public GameObject Next;   // Attribuer éléments visuelles

    public GameObject Response;   // Attribuer éléments visuelles

    public string npcname;   // Ecrire le nom du personnahe qui parle

     bool finish;    // Juste des conditions pour le fonctionnement

     bool finish2;    // Juste des conditions pour le fonctionnement

     bool finish3;   // Juste des conditions pour le fonctionnement

    public string response_1, response_2;   // Pour écrire le choix de dialogue

    [TextArea(1, 10)]

    public string[] answer_1;  // La Liste du dialogue de la réponse 1

    [TextArea(1, 10)]

    public string[] answer_2;   // La liste du dialogue de la réponse 1


    void Start()

    {     // Cette partie sera modifier plus tard lors de l'integration avec nottament un ontriggerenter
        NameBox.SetActive(true);    // Affiche la boite de dialogue
        NPCNametext.text = npcname;   //Attribue les éléments visuelles
        StartCoroutine(Type());  // Lance l'affichage progressif de la phrase

        Response1.text = response_1;   //Attribue les éléments visuelles
        Response2.text = response_2;  //Attribue les éléments visuelles

    }
    IEnumerator Type ()
    {
        // Permet d'afficher la phrase mots par mots
        foreach ( char letter in sentences[index].ToCharArray())  //Prends les mots de la liste
        {
            finish = false;
            Dialoguetext.text += letter;
            yield return new WaitForSeconds(Letterspeed);
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (Dialoguetext.text == sentences[index])
        {
            // si la phrase est affiché totalement
            Next.SetActive(true);  // Active le next d'affichage pour continuer
            finish = true;   // Indique que la phrase et fini
        }

        if (Input.GetKeyDown(KeyCode.Space) && finish ==true)
        {
            // si on appuye sur espace est que la phrase et fini
            NextSentence();
        }

        // cette partie concerne l'affichage après le choix de dialogue de la réponse 1

        if (Dialoguetext.text == answer_1[index2])
        {
            // si la phrase est affiché totalement
            Next.SetActive(true);  // active le next d'affichage
            finish2 = true;   // indique que la phrase et fini
        }

        if (Input.GetKeyDown(KeyCode.Space) && finish2 == true)
        {
            Answer_1Sentence();
        }




        // cette partie concerne l'affichage après le choix de dialogue de la réponse 2

        if (Dialoguetext.text == answer_2[index3])
        {
            // si la phrase est affiché totalement
            Next.SetActive(true);  // active le next d'affichage
            finish3 = true;   // indique que la phrase et fini
        }

        if (Input.GetKeyDown(KeyCode.Space) && finish3 == true)
        {
            Answer_2Sentence();
        }

    }

    public void NextSentence()
    {
        Next.SetActive(false);  // désactive l'affichage next

        if (index< sentences.Length - 1)
        {
            // affiche l'autre phrase
            index++;
            Dialoguetext.text = "";
            StartCoroutine(Type());
            
        }
        else
        {
            // si tous les dialogues de la liste ont était affiché
            Dialoguetext.text = "";

            Next.SetActive(false);

        Dialoguetext.text  =  sentences[sentences.Length - 1]; // affiche la question ou la dernière phrase de la liste

            Response.SetActive(true);  //Affiche les deux choix de dialogue
        }

    }

    public void Answer1() //Pour la réponse 1
    {
        finish = false;
        Dialoguetext.text = "";  // efface la question
        Response.SetActive(false);  //enlève les choix de dialogue
        StartCoroutine(Type1());
    }

    public void Answer2() //Pour la réponse 2
    {
        finish = false;
        Dialoguetext.text = "";   // efface la question
        Response.SetActive(false);  // efface la question
        StartCoroutine(Type2());

    }
   
    

    IEnumerator Type1() // Affichage de la réponse 1
    {
        // Affiche mot par mot la réponse du choix de dialogue
        foreach (char letter in answer_1[index2].ToCharArray())
        {
            // permet d'afficher la phrase mots par mots
            finish2 = false;
            Dialoguetext.text += letter;
            yield return new WaitForSeconds(Letterspeed);
        }
    }



    IEnumerator Type2()  // Affichage de la réponse 2
    {
        // Affiche mot par mot la réponse du choix de dialogue
        foreach (char letter in answer_2[index3].ToCharArray())
        {
            // permet d'afficher la phrase mots par mots
            finish3 = false;
            Dialoguetext.text += letter;
            yield return new WaitForSeconds(Letterspeed);
        }
    }

    void Answer_1Sentence()  // concerne si on a choisit le choix de dialogue 1
    {
        Next.SetActive(false);  // désactive l'affichage next

        if (index2 < answer_1.Length - 1)
        {
            // affiche l'autre phrase
            index2++;
            Dialoguetext.text = "";
            StartCoroutine(Type1());

        }
        else
        {
            // si tous les dialogues ont était affiché
            Dialoguetext.text = "";
            Next.SetActive(false);
              NameBox.SetActive(false);  // fait disparaitre la boite de dialogue
            finish2 = false;
        }
    }

   void Answer_2Sentence()   // concerne si on a choisit le choix de dialogue 1
    {
        Next.SetActive(false);  // désactive l'affichage next

        if (index3 < answer_2.Length - 1)
        {
            // affiche l'autre phrase
            index3++;
            Dialoguetext.text = "";
            StartCoroutine(Type2());

        }
        else
        {
            // si tous les dialogues ont était affiché
            Dialoguetext.text = "";
            Next.SetActive(false);
            //  NameBox.SetActive(false);  // fait disparaitre la boite de dialogue
            // Reponse.SetActive(true);
            finish3 = false;
            NameBox.SetActive(false);
        }
    }
}
