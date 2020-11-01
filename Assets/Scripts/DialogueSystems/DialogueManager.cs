using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DialogueManager : MonoBehaviour
{
    public Text nameText;
    public Text dialogueText;

    public GameObject DialogueBox;
    private Queue<string> sentences;
    public float interval = 0.05f;

    private bool startText = false;




    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("start");
        sentences = new Queue<string>();
    }

    public void StartDialogue(Dialogue dialogue, GameObject npc)
    {

        nameText.text = dialogue.name;


        //delete the content of the queue

        sentences.Clear();


        foreach (string sentence in dialogue.sentences)
        {

            sentences.Enqueue(sentence);
        }


        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {


        if (startText == false)
        {

            startText = true;
            if (sentences.Count == 0)
            {
                EndDialogue();
                return;
            }

            string sentence = sentences.Dequeue();

            StopAllCoroutines();
            StartCoroutine(TypeSentence(sentence));
        }

    }

    IEnumerator TypeSentence(string sentence)
    {
        //display 1 word per frame
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(interval);
        }
        startText = false;
    }

    void EndDialogue()
    {

        startText = false;
        DialogueBox.transform.position = new Vector3(0, 100f, 0);
        
    }
}
