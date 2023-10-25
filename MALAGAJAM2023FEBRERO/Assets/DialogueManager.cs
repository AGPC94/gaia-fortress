using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class DialogueManager : MonoBehaviour
{
    public GameObject window;
    //public Text nameText;
    public Text dialogueText;

    Queue<string> sentences;

    [SerializeField] Dialogue[] dialogues;

    public bool isOpen;

    // Start is called before the first frame update
    void Start()
    {
        sentences = new Queue<string>();

        //Test
        //StartDialogue(dialogues[0]);
        StartCoroutine(ShowDialogues());
    }

    void Update()
    {
    }

    public void StartDialogue(Dialogue dialogue)
    {
        isOpen = true;
        window.SetActive(true);
        sentences.Clear();

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }


        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();
        StartCoroutine(TypeSentence(sentence));
    }

    public void EndDialogue()
    {
        isOpen = false;
        window.SetActive(false);
        Debug.Log("EndDialogue()");
    }

    IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {  
            dialogueText.text += letter;
            yield return new WaitForSecondsRealtime(0.01f);
        }
    }


    IEnumerator ShowDialogues()
    {
        for (int i = 0; i < dialogues.Length; i++)
        {
            Dialogue d = dialogues[i];

            yield return new WaitForSeconds(d.timeToAppear);
            StartDialogue(d);

            yield return new WaitForSeconds(d.timeToDisappear);
            EndDialogue();
        }
    }
}
